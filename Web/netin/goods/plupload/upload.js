/// <reference path="../../script/jquery-1.7.2.min.js" />
/// <reference path="plupload.dev.js" />
/// <reference path="../../script/common.js" />

var mutiUploader = function (o) {

    var uploaderConfig = {
        maxNum: 20,
        max_file_size: '800kb',
        url2upload: '/netin/goods/handlers/pluploadHandler.aspx',
        uploadedPicsDiv: 'uploadedPics',
        AjaxAction2RenderPictures: 'Ajax_RenderPictures',
        keyOfForeignId: 'GoodsId',
    };

    $.extend(uploaderConfig, o);

    uploaderConfig.tipsMsg = '请确保每张上传图片大小不超过' + uploaderConfig.max_file_size + ';每次选取的图片数量不超过' + uploaderConfig.maxNum;

    //实例化一个plupload上传对象
    window.uploader = new plupload.Uploader({
        browse_button: 'browse', //触发文件选择对话框的按钮，为那个元素id
        url: uploaderConfig.url2upload, //服务器端的上传页面地址
        flash_swf_url: '/netin/goods/plupload/Moxie.swf', //swf文件，当需要使用swf方式进行上传时需要配置该参数
        silverlight_xap_url: '/netin/goods/plupload/Moxie.xap', //silverlight文件，当需要使用silverlight方式进行上传时需要配置该参数
        chunk_size: 0,
        file_data_name: 'file',
        multipart_params: {},
        filters: {
            mime_types: [ //只允许上传图片和zip文件
              { title: "Image files", extensions: "jpg,gif,png,bmp,jpeg" }
            ],
            max_file_size: uploaderConfig.max_file_size, //最大只能上传800kb的文件
            prevent_duplicates: true //不允许选取重复文件
        }
    });

    //在实例对象上调用init()方法进行初始化
    uploader.init();

    uploader.myConfig = uploaderConfig;

    //绑定各种事件，并在事件监听函数中做你想做的事
    uploader.bind('FilesAdded', function (uploader, files) {
        //每个事件监听函数都会传入一些很有用的参数，
        //我们可以利用这些参数提供的信息来做比如更新UI，提示上传进度等操作
        if (files.length > uploaderConfig.maxNum) {
            tips('注意：最多只能上传' + uploaderConfig.maxNum + '个文件.且文件大小不允许超过' + uploaderConfig.max_file_size);
            uploader.removeAll();
            return;
        }
        for (var i = 0, len = files.length; i < len; i++) {
            var file_name = files[i].name; //文件名
            //构造html来更新UI
            var html = '<li id="file-' + files[i].id + '"><p class="file-name">' + file_name + '<a href="javascript:;" onclick="uploader.del(\'' + files[i].id + '\');" title="删除">删除</a></p><p class="progress"></p></li>';
            $(html).appendTo('#file-list');
        }
    });

    uploader.bind('UploadProgress', function (uploader, file) {
        //每个事件监听函数都会传入一些很有用的参数，
        //我们可以利用这些参数提供的信息来做比如更新UI，提示上传进度等操作
        $('#file-' + file.id + ' .progress').css('width', file.percent + '%');//控制进度条
    });

    uploader.bind('ChunkUploaded', function (uploader, file, responseObject) {
        //当使用文件小片上传功能时，每一个小片上传完成后触发
    });

    uploader.bind('FileUploaded', function (uploader, file, responseObject) {
        //当队列中的某一个文件上传完成后触发
        var json = $.parseJSON(responseObject.response);
        if (json.error) { alert(json.message); }
        else
            refreshList(uploaderConfig.uploadedPicsDiv, uploaderConfig.AjaxAction2RenderPictures, false);//refresh the pictures ui
    });

    uploader.bind('UploadComplete', function (uploader, files) {
        //当上传队列中所有文件都上传完成后触发 
        $('#remove_files').click();
    });

    /*移除所有载入的文件*/
    uploader.removeAll = function () {
        var c = uploader.files.length;
        uploader.splice(0, c);
        $('#file-list').empty();
    }

    /*删除已上传的图片*/
    uploader.del = function (id, url) {
        confirm('您确定要删除?', function () {
            //这是从服务器删除商品的图片
            if (/^\d+$/.test(id)) {
                var data = {};
                data.ids = id;
                data.urlArr = url;
                nt.ajax({
                    url: '/netin/goods/handlers/ajaxHandler.aspx',
                    data: data,
                    action: 'DelPictures',
                    table: 'picture',
                    success: function (json) {
                        if (json.error) { alert(json.message); }
                        else {
                            $('#img-' + id).remove();
                        }
                    }
                });
                return;
            }

            /*仅删除浏览器从本地加载的图片*/
            $('#file-' + id).remove();
            uploader.removeFile(id);
        });
    }

    /*显示*/
    uploader.setDisplay = function (sender, id) {
        var data = {};
        data.ids = id;
        data.table = 'picture';
        data.field = 'display';
        nt.ajax({
            url: '/netin/handlers/ajaxHandler.aspx',
            data: data,
            action: 'setboolean',
            success: function (json) {
                if (json.error) { alert(json.message); }
                else {
                    var jqO = $(sender);
                    if (jqO.attr('data-display') == '1') {
                        jqO.addClass('nt-image-eye-close');
                        jqO.attr('data-display', '0');
                    } else {
                        jqO.removeClass('nt-image-eye-close');
                        jqO.attr('data-display', '1');
                    }
                }
            }
        });
    }

    /*编辑图片的详细信息*/
    uploader.editImageInfo = function (id) {
        var url = '/netin/goods/picture_edit.aspx?id=' + id;
        openWindow({ url: url });
    }

    /*添加参数*/
    uploader.addParam = function (k, v) {
        var o = this.getOption('multipart_params');
        o[k] = v;
    }

    uploader.setForeignId = function (id) {
        uploader.addParam(uploaderConfig.keyOfForeignId, id);
    }

    /*添加网络图片*/
    uploader.addRemoteFile = function () {
        nt.createHtmlWindow({
            title: '添加网络图片',
            html: '<label>请填写网络地址:</label>&nbsp;&nbsp;<input type="text" class="input-long" name="remoteUrl"/>',
            ok: function () {
                var url = $('input[name="remoteUrl"]', this).val();
                if (url == '') {
                    error('请填写网络地址');
                    return;
                }
                var data = {};
                data['isremote'] = true;
                data['remoteUrl'] = url;
                data[uploaderConfig.keyOfForeignId] = uploader.getOption('multipart_params')[uploaderConfig.keyOfForeignId];

                $.post(
                    uploaderConfig.url2upload,
                    data,
                    function (j) {
                        if (j.error) error(j.message);
                        else {
                            refreshList(uploaderConfig.uploadedPicsDiv, uploaderConfig.AjaxAction2RenderPictures, false);//refresh the pictures ui
                        }
                    },
                   'json');
            }
        });
    }

}