<%@ Page Language="C#" AutoEventWireup="false"%>

<%@ Register Src="~/shared/qb_top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/qb_foot.ascx" TagPrefix="uc1" TagName="foot" %>


<uc1:top runat="server" id="top" />

<div style="height: 57px;" class="clear"></div>

<p class="register_title">欢迎注册</p>
<div style="height: 32px;" class="clear"></div>
<div class="inputbox">
    <form action="/handlers/ajaxHandler.aspx" method="post" id="registerForm">
        <input type="text" name="LoginName" class="smart-input" value="用户名" />
        <div style="height: 24px;" class="clear"></div>
        <input type="text" name="Password" value="密码" class="smart-input" />
        <div style="height: 24px;" class="clear"></div>
        <input type="text" name="Password.Again" value="确认密码" class="smart-input" />
        <div style="height: 24px;" class="clear"></div>
        <input type="text" name="Email" value="邮箱" class="smart-input" />
        <div style="height: 24px;" class="clear"></div>
        <div class="input_verification">
            <input type="text" name="CheckCode" value="验证码" class="smart-input" />
            <a href="javascript:;" onclick="refreshCode(checkCode);">
                <img alt="" id="checkCode" src="/handlers/CheckCodeGenerator.aspx" />
            </a>
        </div>
    </form>
</div>

<div style="height: 41px;" class="clear"></div>
<a href="javascript:;" onclick="return register('registerForm');" class="register_bt"></a>
<div style="height: 11px;" class="clear"></div>
<a class="register_problem" href="login.aspx">已经注册？</a>

<div style="height: 58px;" class="clear"></div>
<uc1:foot runat="server" id="foot" />
