<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>
<%
    InCustomerArea = true;
%>

<!----container---->
<uc1:top runat="server" ID="top1" />

<%Include("/html.inc/customer_top.html"); %>
<h3>修改密码</h3>

<form action="/handlers/ajaxhandler.aspx" invoke="changePwd" method="post" id="changePwdForm">
    <div class="nt-customer-info-field">
        <label>原密码：</label><input type="password" name="OldPwd" onchange="return validateField(this,'text');" value="" />
        <span class="nt-validation-message"></span>
    </div>
    <div class="clear"></div>
    <div class="nt-customer-info-field">
        <label>新密码：</label><input type="password" name="NewPwd" onchange="return validateField(this,'text');" value="" />
        <span class="nt-validation-message"></span>
    </div>
    <div class="clear"></div>
    <div class="nt-customer-info-field">
        <label>确认密码：</label><input type="password" name="NewPwd.Again" onchange="return validateField(this,'text');" value="" />
        <span class="nt-validation-message"></span>
    </div>
    <div class="clear"></div>
    <div class="nt-customer-save-button">
        <input type="button" class="nt-submit" onclick="changePwdForm();" value="保存" />
        <span class="nt-validation-message"></span>
    </div>
</form>

<script type="text/javascript">
    function changePwdForm() {
        nt.ajaxSubmit('#changePwdForm', {
            ok: function (j) {
                alert(j.message, function () {
                    go('myaccount.aspx');
                });
            }
        });
    }
</script>


<%Include("/html.inc/customer_bottom.html"); %>

<uc1:foot runat="server" ID="foot" />
