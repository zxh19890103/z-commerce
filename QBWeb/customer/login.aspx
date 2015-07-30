<%@ Page Language="C#" AutoEventWireup="false" %>

<%@ Register Src="~/shared/qb_top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/qb_foot.ascx" TagPrefix="uc1" TagName="foot" %>


<uc1:top runat="server" ID="top" />

<div style="height: 48px;" class="clear"></div>
<div class="nt-login-box">
    <div style="height: 20px;" class="clear"></div>
    <img class="loginpic" src="/images/loginpic.png" alt="" />
    <div style="height: 25px;" class="clear"></div>
    <form action="../handlers/ajaxHandler.aspx" method="post" id="LoginForm">
        <div class="inputbox">
            <input class="login_input smart-input" name="LoginName" />
            <label>用户名:</label>
        </div>
        <div style="height: 15px;" class="clear"></div>
        <div class="inputbox">
            <input class="login_input smart-input" name="Password" />
            <label>密码:</label>
        </div>
        <div style="height: 15px;" class="clear"></div>
        <div class="verification inputbox">
            <input name="CheckCode" class="smart-input" style="float: left" />
            <label>验证码:</label>
            <a href="javascript:;" onclick="refreshCode(checkCode);" style="float: left">
                <img style="margin-top: 7px; margin-left: 5px;" id="checkCode" src="/handlers/CheckCodeGenerator.aspx" alt="验证码" />
            </a>
            <a class="retry" style="float: left">忘记密码</a>
        </div>
        <div class="clear" style="padding-top: 15px; margin-left: 120px;">
            <input class="login_bt" type="button" style="float: left" onclick="return login('LoginForm');" />
            <a class="register_botton" href="register.aspx" style="float: left; margin-left: 20px;"></a>
        </div>
    </form>
    <div style="height: 23px;" class="clear"></div>
</div>

<div style="height: 60px;" class="clear"></div>


<uc1:foot runat="server" ID="foot" />
