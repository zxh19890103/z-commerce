<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>

<uc1:top runat="server" ID="top1" />

<form action="/handlers/ajaxHandler.aspx" method="post" id="registerForm">
    <div class="nt-register-box">
        <h3>会员注册/REGISTER</h3>
        <label>
            用户名</label><input type="text" name="loginname" class="nt-input" value="" /><span class="nt-tips-oneline">以字母开头，4-20位字母或数字!</span>
        <br />
        <label>密码</label><input type="password" name="password" class="nt-input" value="" /><span class="nt-tips-oneline">密码只能输入6-20个字母、数字、下划线</span>
        <br />
        <label>重新输入密码</label><input type="password" name="password.again" class="nt-input" value="" />
        <br />
        <label>邮箱</label><input type="text" name="email" class="nt-input" value="" />
        <br />
        <label>验证码</label><input type="text" name="checkcode" class="nt-input nt-input-checkcode" value="" />
        <img id="checkcode" title="checkcode" src="/handlers/checkCodeGenerator.aspx" class="nt-checkcode" alt="load failed" /><a href="javascript:;" onclick="refreshCode('checkcode')">重新获取</a>
        <br />
        <input type="submit" class="nt-submit" value="注册" onclick="return register('registerForm');" />
    </div>
</form>

<uc1:foot runat="server" ID="foot" />
