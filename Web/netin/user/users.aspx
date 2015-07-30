<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="users.aspx.cs" Inherits="netin_user_users" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        /*重置密码为000000*/
        function resetPassword(id) {
            nt.ajax({
                url: 'users.aspx',
                action: 'ResetPassword',
                data: { id: id, pass: '000000' },
                success: function (json) {
                    success('密码已经重置为000000');
                }
            });
        }

        /*保存密码*/
        function pass_save() {
            nt.ajax({
                url: 'users.aspx',
                action: 'ResetPassword',
                data: { id: $('#UserId').val(), pass: $('#NewPassword', '#passSettingForm').val() },
                success: function (json) {
                    $('#passSettingForm').hide();
                    if (json.error) { alert(json.message); }
                    else {
                        success("修改成功!");
                    }
                }
            });
        }

        /*取消密码修改*/
        function pass_cancel() {
            nt.removeMask();
            $('#passSettingForm').hide();
        }

        /*设置密码*/
        function setPassword(id) {
            nt.showMask();
            $('#UserId').val(id);
            $('#passSettingForm').center();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <div id="list" class="list">
        <%List(); %>
    </div>

    <div id="passSettingForm" class="html-content" style="width: 300px; height: auto;">
        <div class="html-content-top">
            <span>修改密码</span>
            <a href="javascript:;" onclick="pass_cancel();">x</a>
        </div>
        <div class="html-content-body">
            <p>
                <span>新密码:</span>
                <input type="text" id="NewPassword" name="NewPassword" />
            </p>
        </div>
        <div class="submit">
            <input name="Id" id="UserId" type="hidden" />
            <a class="a-button" href="javascript:;" onclick="pass_save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="pass_cancel();">取消</a>
        </div>
    </div>
</asp:Content>
