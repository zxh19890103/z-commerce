<%@ Control Language="C#" AutoEventWireup="false" CodeFile="uploader.ascx.cs" Inherits="Netin_shared_uploader" %>
<%
    
    Response.Write("<div class=\"file-uploader\" id=\"");
    Response.Write(ID);
    Response.Write("\"");
    if (!IsFile)
        Response.Write(" style=\"width: 200px; height: 300px;\"");
    else
        Response.Write(" style=\"width: 600px;\"");
    Response.Write(">");
    if (!IsFile)
    {
        Response.Write("<div style=\"height: 200px; width: 200px; margin-bottom: 5px;\">");
        Response.Write("<img alt=\"暂无图片\" id=\"img_");
        Response.Write(ID);
        Response.Write("\" src=\"");
        Response.Write(ImgUrl);
        Response.Write("\" height=\"200\" width=\"200\" />");
        Response.Write("</div>");
    }
    else
    {
        Response.Write("<div style=\"margin-bottom: 5px;padding-left:5px;\">");
        Response.Write("<label style=\"width:120px;display:inline-block;\">下载资料路径(Url):</label><input type=\"text\" class=\"input-long\"  name=\"");
        Response.Write(FieldName);
        Response.Write("\" value=\"");
        Response.Write(FileUrl);
        Response.Write("\"/>");
        Response.Write("<br/><br/>");
        Response.Write("<label style=\"width:120px;display:inline-block;\">下载资料名:</label><input type=\"text\" class=\"input-long\"  name=\"FileName\" value=\"");
        Response.Write(FileName);
        Response.Write("\"/>");
        Response.Write("<br/><br/>");
        Response.Write("<label style=\"width:120px;display:inline-block;\">下载资料大小(Byte):</label><input type=\"text\" class=\"input-long\"  name=\"FileSize\" value=\"");
        Response.Write(FileSize);
        Response.Write("\"/>");
        Response.Write("</div>");
    }
    Response.Write("    <div class=\"buttons\" style=\"height: 45px; width: 100%; text-align: left;\">");
    Response.Write("        <a href=\"javascript:;\" id=\"");
    Response.Write(ID);
    Response.Write("BrowserHolder\" style=\"display: inline-block; width: 70px;");
    Response.Write("            height: 25px; overflow: hidden; border: 1px solid #808080; margin: auto 5px;");
    Response.Write("            color: #767C88;background:url('/netin/content/images/browser-button-bg.png') no-repeat;\"></a> <a href=\"javascript:;\" onclick=\"");
    Response.Write(ID);
    Response.Write("Upload();\" style=\"display: inline-block;");
    Response.Write("                width: 70px; height: 25px; overflow: hidden; border: 1px solid #808080; margin: auto 5px;");
    Response.Write("                color: #767C88;background:url('/netin/content/images/upload-button-bg.png') no-repeat;\"></a>");
    Response.Write("    </div>");
    if (!IsFile)
    {
        Response.Write("    <input type=\"text\" onchange=\"loadImg(this.value,'img_");
        Response.Write(ID);
        Response.Write("');\" class=\"input-long\" id=\"");
        Response.Write(FieldName);
        Response.Write("\" name=\"");
        Response.Write(FieldName);
        Response.Write("\" value=\"");
        Response.Write(FieldValue);
        Response.Write("\" />");
    }
    Response.Write("</div>");
%>
<script type="text/javascript">
    $(function () {
        var html = '';
        html += '<div id="<%=ID %>Uploader" style="display:none;position:absolute;z-index:1989;width: 140px;height:50px;overflow:hidden;">';
        html += '	<form id="<%=ID %>Form" method="post" action="<%=PostUrl %>" target="_blank" enctype="multipart/form-data">';
        html += '		<input type="file" name="file" id="<%=ID%>File" style="left:-140px;padding:0;border:none;background-color:transparent;color:#fff;cursor:pointer;height:50px;width:280px;position:absolute;" />';
        html += '		<input type="hidden" name="oldfile" value="<%=FieldValue%>" />';
        html += '		<input type="hidden" name="isfile" value="<%=IsFile%>" />';
        html += '	</form>';
        html += '</div>';

        $(document.body).append(html);//将文件上传表单追加到body元素中

        /*兼容性调整*/
        var file = $('#<%=ID %>File');
        var bi = getBrowserInfo();
        if (bi.ie) {
            var ver = bi.ie;
            switch (ver) {
                case '11.0':
                case '10.0':
                case '9.0':
                    file.css({ 'left': -10 });
                    break;
                case '8.0':
                case '7.0':
                    file.css('left', -10);
                    break;
                default:
                    error('浏览器版本太低，建议升级!');
                    break;
            }
            file.attr('title', '请双击浏览按钮');
        }
        
        file.change(function () {
            $('#<%=ID %>Uploader').hide();
        });

        $('#<%=ID %>BrowserHolder').mouseover(function (e) {
            var self = this;
            var tx = e.pageX;
            var ty = e.pageY;
            $('#<%=ID %>Uploader').css({ left: tx - 70, top: ty - 25 }).show();
        });
    });

    /*
    上传开始
    */
    function <%=ID %>Upload() {

        var form = $('#<%=ID %>Form');

        if ($('input[type="file"]', form).val() == '') {
            error('没有载入文件');
            return;
        }

        //提交之前为oldfile赋值
        $('input[name="oldfile"]', form).val($('input[name="<%=FieldName%>"]','#<%=ID %>').val());

        form.ajaxSubmit({
            url: '<%=PostUrl%>',
            forceSync: true,
            dataType: 'json',
            beforeSubmit: function () {
                nt.showMask();
                nt.showLoading();
            },
            success: function (json, statusText) {
                if (json.error) {
                    error(json.message);
                } else {
                    var isfile=<%=IsFile?"true":"false" %>;
                        $('input[name="<%=FieldName%>"]','#<%=ID %>').val(json.fileUrl);
                        if(isfile){
                            $('input[name="FileSize"]','#<%=ID %>').val(json.fileSize);
                        $('input[name="FileName"]','#<%=ID %>').val(json.fileName);
                    }else{
                        $('img','#<%=ID%>').attr('src', json.thumbnail);
                    }
                    G('<%=ID%>File').value='';
                    }
                    nt.removeMask();
                    nt.removeLoading();
                },
            error: function () {
                error('操作错误!');
                nt.removeMask();
                nt.removeLoading();
            }
        });
        }
</script>
