<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>


<!----container---->
<uc1:top runat="server" ID="top1" />

<form action="/handlers/ajaxHandler.aspx" method="post" id="loginForm">
    <div class="nt-login-box">
        <h3>会员登录/LOGIN</h3>
        <label>用户名</label><input type="text" name="LoginName" class="nt-input" value="" />
        <br />
        <label>密码</label><input type="password" name="Password" class="nt-input" value="" />
        <br />
        <label>验证码</label><input type="text" name="CheckCode" class="nt-input nt-input-checkcode" value="" />
        <img id="checkcode" title="checkcode" src="/handlers/checkCodeGenerator.aspx" class="nt-login-checkcde" alt="load failed" /><a href="javascript:;" onclick="refreshCode('checkcode')">重新获取</a>
        <br />
        <input type="submit" class="nt-submit" onclick="return login('loginForm');" value="登录" />
    </div>
</form>

<uc1:foot runat="server" ID="foot" />


