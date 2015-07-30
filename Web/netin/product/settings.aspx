<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/netin/layout.master" CodeFile="settings.aspx.cs" Inherits="netin_product_settings" %>

<%@ Register Src="~/netin/shared/uploader.ascx" TagPrefix="uc1" TagName="uploader" %>


<asp:Content runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .interval-refresh-text-box { font-size: 18px; font-style: italic; }
    </style>
    <script type="text/javascript">
        //保存
        function save() {
            editForm.submit();
        }

        /*
        刷新进度消息
        */
        function refreshMsgBox() {
            window.int = setInterval(function () {
                $.get('../handlers/WatermarkerHandler.ashx',
                function (text) {
                    var json = $.parseJSON(text);
                    if (!json.isRunning)
                        clearInterval(window.int);
                    $('#intervalRefreshTextBox').text(json.message);
                });
            }, 100);
        }

        /*按设置生成图片*/
        function handleMedias(code, thumbon) {
            var data = {};
            data.code = code;
            if (thumbon != undefined) data.thumbon = thumbon;
            nt.ajax({
                url: 'settings.aspx',
                action: 'HandleMedias',
                data: data,
                success: function (json) {
                    if (json.error) {
                        error(json.message);
                    }
                    else {
                        refreshMsgBox();
                    }
                }
            });
        }


    </script>

</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="body">

    <div class="submit-top">

        <a class="a-button" href="javascript:;" onclick="save();">保存设置</a>

        <a class="a-button" href="javascript:;" onclick="handleMedias(<%=WaterMarker.DEL_PRODUCT_THUMBNAIL %>);">删除缩略图</a>

        <a class="a-button" href="javascript:;" onclick="handleMedias(<%=WaterMarker.DEL_PRODUCT_WATER %>);">删除水印图</a>

        <br />

        <a href="javascript:;" onclick="handleMedias(<%=WaterMarker.GENERATE_PRODUCT_THUMBNAIL %>,<%=(int)ThumbOn.List %>);">按设置生成列表页缩略图</a>

        <a href="javascript:;" onclick="handleMedias(<%=WaterMarker.GENERATE_PRODUCT_THUMBNAIL %>,<%=(int)ThumbOn.Detail %>);">按设置生成详细页缩略图</a>

        <a href="javascript:;" onclick="handleMedias(<%=WaterMarker.GENERATE_PRODUCT_THUMBNAIL %>,<%=(int)ThumbOn.Home %>);">按设置生成首页缩略图</a>

        <a href="javascript:;" onclick="handleMedias(<%=WaterMarker.GENERATE_PRODUCT_TEXT_WATER %>);">按设置生成文字水印图</a>

        <a href="javascript:;" onclick="handleMedias(<%=WaterMarker.GENERATE_PRODUCT_IMG_WATER %>);">按设置生成图片水印图</a>
        <br />

        <span class="interval-refresh-text-box" id="intervalRefreshTextBox"><%=WaterMarker.Instance.Message %></span>
        <br />
        <br />
    </div>

    <form action="settings.aspx" method="post" id="editForm">
        <table class="adminContent">

            <tr>
                <td class="adminTitle">列表页显示记录的个数：</td>
                <td class="adminData">
                    <input type="text" class="input-number" name="PageSize" value="<%=Model.PageSize%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">列表页页码的个数：</td>
                <td class="adminData">
                    <input type="text" class="input-number" name="PagerItemCount" value="<%=Model.PagerItemCount%>" />
                </td>
            </tr>

            <tr class="adminSeparator">
                <td colspan="2">
                    <hr />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">开启列表页缩略图功能：</td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.EnableThumbOnList, "EnableThumbOnList"); %>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">缩略图宽(px)：</td>
                <td class="adminData">
                    <input type="text" class="input-number" name="ThumbWidthOnList" value="<%=Model.ThumbWidthOnList%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">缩略图高(px)：</td>
                <td class="adminData">
                    <input type="text" class="input-number" name="ThumbHeightOnList" value="<%=Model.ThumbHeightOnList%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">缩略图生成模式：</td>
                <td class="adminData">
                    <%
                        NtUtility.ListItemSelect(StaticDataProvider.Instance.ThumbGenerationModeProvider, Model.ThumbModeOnList);
                        HtmlRenderer.DropDownList(StaticDataProvider.Instance.ThumbGenerationModeProvider, "ThumbModeOnList");
                    %>
                </td>
            </tr>

            <tr class="adminSeparator">
                <td colspan="2">
                    <hr />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">开启详细页缩略图功能：</td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.EnableThumbOnDetail, "EnableThumbOnDetail"); %>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">缩略图宽(px)：</td>
                <td class="adminData">
                    <input type="text" class="input-number" name="ThumbWidthOnDetail" value="<%=Model.ThumbWidthOnDetail%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">缩略图高(px)：</td>
                <td class="adminData">
                    <input type="text" class="input-number" name="ThumbHeightOnDetail" value="<%=Model.ThumbHeightOnDetail%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">缩略图生成模式：</td>
                <td class="adminData">
                    <%
                        NtUtility.ListItemSelect(StaticDataProvider.Instance.ThumbGenerationModeProvider, Model.ThumbModeOnDetail);
                        HtmlRenderer.DropDownList(StaticDataProvider.Instance.ThumbGenerationModeProvider, "ThumbModeOnDetail");
                    %>
                </td>
            </tr>

            <tr class="adminSeparator">
                <td colspan="2">
                    <hr />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">开启首页缩略图功能：</td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.EnableThumbOnHome, "EnableThumbOnHome"); %>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">缩略图宽(px)：</td>
                <td class="adminData">
                    <input type="text" class="input-number" name="ThumbWidthOnHome" value="<%=Model.ThumbWidthOnHome%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">缩略图高(px)：</td>
                <td class="adminData">
                    <input type="text" class="input-number" name="ThumbHeightOnHome" value="<%=Model.ThumbHeightOnHome%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">缩略图生成模式：</td>
                <td class="adminData">
                    <%
                        NtUtility.ListItemSelect(StaticDataProvider.Instance.ThumbGenerationModeProvider, Model.ThumbModeOnHome);
                        HtmlRenderer.DropDownList(StaticDataProvider.Instance.ThumbGenerationModeProvider, "ThumbModeOnHome");
                    %>
                </td>
            </tr>

            <tr class="adminSeparator">
                <td colspan="2">
                    <hr />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">开启文字水印功能：</td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.EnableTextMark, "EnableTextMark"); %>
                   
                </td>
            </tr>

            <tr>
                <td class="adminTitle">水印文字内容：</td>
                <td class="adminData">
                    <input type="text" class="input-long" name="TextMark" value="<%=Model.TextMark%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">水印所处位置：</td>
                <td class="adminData">
                    <%
                        NtUtility.ListItemSelect(StaticDataProvider.Instance.MarkPositionProvider, Model.TextMarkPosition);
                        HtmlRenderer.DropDownList(StaticDataProvider.Instance.MarkPositionProvider, "TextMarkPosition");
                    %>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">字体：</td>
                <td class="adminData">
                    <%
                        NtUtility.ListItemSelect(StaticDataProvider.Instance.FontFamilyProvider, Model.FontFamily);
                        HtmlRenderer.DropDownList(StaticDataProvider.Instance.FontFamilyProvider, "FontFamily");
                    %>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">水印文字占位宽(px)：</td>
                <td class="adminData">
                    <input type="text" class="input-number" name="WidthOfTextBox" value="<%=Model.WidthOfTextBox%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">水印文字占位高(px)：</td>
                <td class="adminData">
                    <input type="text" class="input-number" name="HeightOfTextBox" value="<%=Model.HeightOfTextBox%>" />
                </td>
            </tr>

            <tr class="adminSeparator">
                <td colspan="2">
                    <hr />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">开启图片水印功能：</td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.EnableImgMark, "EnableImgMark"); %>
                   
                </td>
            </tr>

            <tr>
                <td class="adminTitle">水印图不透明度(0.00)：</td>
                <td class="adminData">
                    <input type="text" class="input-decimal" name="ImgMarkAlpha" value="<%=Model.ImgMarkAlpha%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">水印图所处位置：</td>
                <td class="adminData">
                    <%
                        NtUtility.ListItemSelect(StaticDataProvider.Instance.MarkPositionProvider, Model.ImgMarkPosition);
                        HtmlRenderer.DropDownList(StaticDataProvider.Instance.MarkPositionProvider, "ImgMarkPosition");
                    %>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">上传水印图：</td>
                <td class="adminData">
                    <uc1:uploader runat="server" FieldName="MarkImgUrl" PostUrl="/netin/handlers/uploadHandler.aspx" ID="uploader" />
                </td>
            </tr>

        </table>
        <div class="submit">
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
        </div>
    </form>

</asp:Content>
