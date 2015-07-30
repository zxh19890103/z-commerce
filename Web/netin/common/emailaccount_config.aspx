<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="emailaccount_config.aspx.cs" Inherits="netin_common_emailaccount_config" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //保存
        function save() {
            editForm.submit();
        }

        /*发送邮件测试*/
        function trySendMail(id) {
            nt.showMask();
            nt.ajax({
                action: 'TrySendMail',
                data: { accountId: id, mailAddress: $('#testMailAddress').val() },
                success: function (json) {
                    if (json.error) error(json.message);
                    else {
                        success(json.message);
                    }
                    nt.removeMask();
                }
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <form action="emailaccount_config.aspx" method="post" id="editForm">
        <table class="adminContent">
            <tr>
                <td class="adminTitle">发送方邮箱地址:</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="Email" value="<%=Model.Email %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">发送者显示名:</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="DisplayName" value="<%=Model.DisplayName %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">主机:</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="Host" value="<%=Model.Host %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">端口:</td>
                <td class="adminData">
                    <input type="text" class="input-number" maxlength="9" name="Port" value="<%=Model.Port==0?25: Model.Port%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">用户名:</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="UserName" value="<%=Model.UserName %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">密码:</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="Password" value="<%=Model.Password %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">使用Ssl协议:</td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.EnableSsl, "EnableSsl"); %>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">使用默认Credentials:</td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.UseDefaultCredentials, "UseDefaultCredentials"); %>
                </td>
            </tr>

        </table>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=NtID %>" />
            <input type="hidden" name="IsDefault" value="<%=Model.IsDefault %>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
            <a class="a-button" href="<%BackScript(); %>">返回</a>
        </div>
    </form>

    <%if (IsEdit)
      { %>

    <hr />
    <div class="try-send-mail">
        <h3>发送邮件测试</h3>
        <div class="mail-address">
            发送至:&nbsp;&nbsp;<input type="text" name="MailAddress" id="testMailAddress" class="input-long" />
            <br />
            <br />
            <a class="a-button" href="javascript:;" onclick="trySendMail(<%=Model.Id%>);">发送</a>
        </div>
    </div>
    <%} %>
</asp:Content>
