<%@ Page Language="C#" MasterPageFile="~/netin/layout.master"
    AutoEventWireup="false" CodeFile="settings.aspx.cs" Inherits="netin_common_settings" %>

<%@ Register Src="~/netin/shared/uploader.ascx" TagPrefix="uc1" TagName="uploader" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        //保存
        function save() {
            editForm.submit();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <form action="settings.aspx" method="post" id="editForm">
        <table class="adminContent">
            <tr>
                <td class="adminTitle">网站名称：</td>
                <td class="adminData">
                    <input type="text" class="input-long" name="WebsiteName" value="<%=Model.WebsiteName%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">网站标题：</td>
                <td class="adminData">
                    <input type="text" class="input-long" name="SeoTitle" value="<%=Model.SeoTitle%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">网站Logo：</td>
                <td class="adminData">
                    <uc1:uploader runat="server" ID="uploader" FieldName="Logo" PostUrl="/netin/handlers/uploadHandler.aspx" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">网站关键词：</td>
                <td class="adminData">
                    <textarea cols="30" rows="5" class="textarea-small" name="SeoKeywords"><%=Model.SeoKeywords%></textarea>
                    <span class="tips">请勿超过1024个字符</span>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">网站描述：</td>
                <td class="adminData">
                    <textarea cols="30" rows="5" class="textarea-small" name="SeoDescription"><%=Model.SeoDescription %></textarea>
                    <span class="tips">请勿超过1024个字符</span>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">公司名称：</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="256" name="CompanyName" value="<%=Model.CompanyName%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">公司地点：</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="CompanyAddress" value="<%=Model.CompanyAddress%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">网站地址：</td>
                <td class="adminData">
                    <input type="text" class="input-long" id="WebsiteUrl" name="WebsiteUrl" value="<%=Model.WebsiteUrl%>" />
                    <span class="tips">请输入完整的网站地址,如
                                <a href="javascript:;" title="复制默认网址" onclick="copyLocalUrl();">http://www.naite.com.cn</a></span>
                    <script type="text/javascript">
                        function copyLocalUrl() {
                            var url = '<%=WebHelper.CurrentHost%>';
                            document.getElementById('WebsiteUrl').value = url;
                        }
                    </script>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">Email：</td>
                <td class="adminData">
                    <input type="text" class="input-long" name="Email" maxlength="256" value="<%=Model.Email%>" />
                    <span class="tips">多个Email请用英文逗号隔开</span>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">座机：</td>
                <td class="adminData">
                    <input type="text" class="input-long" name="Phone" maxlength="256" value="<%=Model.Phone%>" />
                    <span class="tips">多个号码请用英文逗号隔开</span>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">手机：</td>
                <td class="adminData">
                    <input type="text" class="input-long" name="Mobile" maxlength="256" value="<%=Model.Mobile%>" />
                    <span class="tips">多个号码请用英文逗号隔开</span>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">QQ：</td>
                <td class="adminData">
                    <input type="text" class="input-long" name="QQ" maxlength="256" value="<%=Model.QQ%>" />
                    <span class="tips">多个QQ号码请用英文逗号隔开</span>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">传真：</td>
                <td class="adminData">
                    <input type="text" class="input-long" name="Fax" maxlength="256" value="<%=Model.Fax%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">联系人：</td>
                <td class="adminData">
                    <input type="text" class="input-long" name="Linkman" value="<%=Model.Linkman%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">邮编：</td>
                <td class="adminData">
                    <input type="text" class="input-long" name="ZipCode" value="<%=Model.ZipCode%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">ICP：</td>
                <td class="adminData">
                    <input type="text" class="input-long" name="ICP" value="<%=Model.ICP%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">开启静态化：</td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.SetHtmlOn, "SetHtmlOn");%>
                    
                </td>
            </tr>

            <tr>
                <td class="adminTitle">静态化模式：</td>
                <td class="adminData">
                    <%HtmlRenderer.XY(Model.HtmlMode, "HtmlMode", "ID", "GUID"); %>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">关闭网站：</td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.SetWebsiteOff, "SetWebsiteOff");%>
                </td>
            </tr>

        </table>
        <div class="submit">
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
        </div>
    </form>
</asp:Content>
