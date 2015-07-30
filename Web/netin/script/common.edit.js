/// <reference path="jquery-1.7.2.min.js" />
/// <reference path="common.js" />

/*
提交数据并刷新父页面列表，关闭当前子页面
*/
function modalDialogWinPostForm() {
    $('#editForm').ajaxSubmit2({
        action: 'Post',
        success: function (json) {
            if (json.error) error(json.message);
            else {
                GetOpener().refreshList('list', 'List', false);
                window.close();
            }
        }
    });
}

/*
获取对模态对话框的父窗口引用
*/
function GetOpener() {
    if (window.opener === undefined) {
        return window.dialogArguments;//弹出的是模态窗口
    } else {
        return window.opener;
    }
}

/*
ajax载入图片
*/
function loadImg(url, imgId) {
    if (typeof url != 'string' || url === '') return;//must be string and not empty
    nt.ajax({
        url: '/netin/handlers/ajaxhandler.aspx',
        data: { url: escape(url) },
        action: 'ImgLoad',
        success: function (j) {
            if (j.error) error(j.message);
            else {
                var img = G(imgId);
                if (img != null && img != undefined) {
                    img.src = j.message;
                }
            }
        }
    });
}

/*缓存*/
window.ntCache = window.ntCache || {};

/*
根据Key获取缓存值,
funcOrValue:如果没有缓存，则以此设置
*/
window.ntCache.get = function (key, funcOrValue) {
    if (window.ntCache[key]) return window.ntCache[key];
    window.ntCache.set(key, funcOrValue);
    return window.ntCache[key];
};

/*
设置缓存
*/
window.ntCache.set = function (key, funcOrValue) {
    if (isFunc(funcOrValue))
        window.ntCache[key] = funcOrValue();
    else if (typeof funcOrValue === 'string')
        window.ntCache[key] = funcOrValue;
    else
        return;
}

/*是否存在缓存*/
window.ntCache.has = function (key) {
    return window.ntCache[key] != undefined;
}

/*移除*/
window.ntCache.remove = function (key) {
    if (window.ntCache.has(key)) {
        window.ntCache[key] = undefined;
    }
}

/*
选择模板
*/
function selectTemplate(sender) {
    var c = function () {
        var html = window.ntCache.get(key);
        nt.createHtmlWindow({
            title: '模板选择',
            html: html,
            created: function () {
                var self = this;
                $('a.nt-templates-item-folder', '#ntTemplatesFiles').click(function () {
                    var tar = $('#nt-templates-folder-' + $(this).attr('data-target-folder-id'));
                    if (this.open) {
                        //close folder
                        tar.slideUp();
                        $(this).removeClass('nt-templates-item-folder-open');
                        this.open = false;
                    } else {
                        //open folder
                        tar.slideDown();
                        $(this).addClass('nt-templates-item-folder-open');
                        this.open = true;
                    }
                });

                $('a.nt-templates-item-file', '#ntTemplatesFiles').dblclick(function () {
                    $(sender).val($(this).text());
                    self.close();
                });

            },
            width: 800,
            height: 600
        });
    };

    var key = 'nt.cache.templates.html';
    if (!window.ntCache.has(key)) {
        var src = '/netin/handlers/aspxTemplatesBrowser.ashx';
        $.get(src, null, function (t) {
            window.ntCache.set(key, t);
            c();
        }, 'text');
    } else {
        c();
    }
}