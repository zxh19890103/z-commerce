<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false"
    CodeFile="settings.aspx.cs" Inherits="netin_guestbook_settings" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">

        /*保存*/
        function save() {
            editForm.submit();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <form action="settings.aspx" method="post" id="editForm">
    <table class="adminContent">
        <tr>
            <td class="adminTitle">
                开启审核功能：
            </td>
            <td class="adminData">
                <%HtmlRenderer.CheckBox(Model.EnableVerification, "EnableVerification"); %>
            </td>
        </tr>
        <tr>
            <td class="adminTitle">
                使用验证码：
            </td>
            <td class="adminData">
                <%HtmlRenderer.CheckBox(Model.EnableCheckCode, "EnableCheckCode"); %>
            </td>
        </tr>
        <tr>
            <td class="adminTitle">
                使用飘浮咨询框：
            </td>
            <td class="adminData">
                <%HtmlRenderer.CheckBox(Model.EnableFloatQueryBox, "EnableFloatQueryBox"); %>
            </td>
        </tr>
        <tr>
            <td class="adminTitle">
                飘浮咨询框JS代码：
            </td>
            <td class="adminData">
                <textarea name="ScriptOfFloatQueryBox"><%= Model.ScriptOfFloatQueryBox%></textarea>
            </td>
        </tr>
        <tr>
            <td class="adminTitle">
                使用邮件通知：
            </td>
            <td class="adminData">
                <%HtmlRenderer.CheckBox(Model.EnableSendEmail, "EnableSendEmail"); %>
            </td>
        </tr>
        <tr>
            <td class="adminTitle">
                接收邮件的邮箱地址：
            </td>
            <td class="adminData">
                <input type="text" class="input-long" name="EmailAddressToReceiveEmail" value="<%=Model.EmailAddressToReceiveEmail%>" />
            </td>
        </tr>
        <tr>
            <td class="adminTitle">
                收件人名称：
            </td>
            <td class="adminData">
                <input type="text" class="input-short" name="EmailToName" value="<%=Model.EmailToName%>" />
            </td>
        </tr>
        <tr class="adminSeparator">
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td class="adminTitle">
                过滤内容中含有网址的留言：
            </td>
            <td class="adminData">
                <%HtmlRenderer.CheckBox(Model.FiltrateUrl, "FiltrateUrl"); %>
            </td>
        </tr>
        <tr>
            <td class="adminTitle">
                过滤内容中的敏感词汇：
            </td>
            <td class="adminData">
                <%HtmlRenderer.CheckBox(Model.FiltrateSensitiveWords, "FiltrateSensitiveWords"); %>
            </td>
        </tr>
        <tr>
            <td class="adminTitle">
                敏感词汇：
            </td>
            <td class="adminData">
                <div style="color: #f00;">
                    如果留言中有如下词语，则视为无效留言 ，每个过滤字符用回车分割开。</div>
                <textarea cols="2" rows="3" name="SensitiveWords" class="textarea-normal"><%=Model.SensitiveWords %></textarea>
            </td>
        </tr>
    </table>
    <div class="submit">
        <a class="a-button" href="javascript:;" onclick="save();">保存</a> <a class="a-button"
            href="javascript:;" onclick="editForm.reset();">重置</a>
    </div>
    </form>
</asp:Content>
