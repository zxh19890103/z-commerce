<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="list.aspx.cs" Inherits="Netin_Customer_list" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        /*重置密码为000000*/
        function resetPassword(id) {
            nt.ajax({
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
                action: 'ResetPassword',
                data: { id: $('#CID').val(), pass: $('#NewPassword', '#passSettingForm').val() },
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
            //注意顺序
            $('#passSettingForm').hide();
            nt.removeMask();            
        }

        /*设置密码*/
        function setPassword(id) {
            nt.showMask();
            $('#CustomerId').val(id);
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
                <b>新密码:</b>
                <input type="text" id="NewPassword" name="NewPassword" />
            </p>
        </div>
        <div class="submit">
            <input name="Id" id="CustomerId" type="hidden" />
            <a class="a-button" href="javascript:;" onclick="pass_save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="pass_cancel();">取消</a>
        </div>
    </div>
</asp:Content>
