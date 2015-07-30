<%@ Control Language="C#" ClassName="qb_top" Inherits="Nt.Framework.BaseAscx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>秋白</title>
    <link rel="stylesheet" href="/css/qiubai.css" />
    <link rel="stylesheet" href="/css/style-additional.css" />
    <script src="/js/jquery-1.7.2.min.js"></script>
    <script src="/js/jquery.easing.min.js"></script>
    <script src="/script/naite.utility.js" type="text/javascript"></script>
    <script src="/script/jquery.form.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            registerSmartInput();
        });
    </script>
</head>

<body>
    <div class="headbox">
        <div class="headcontent">
            <a href="/" class="logo"></a>
            <div class="navbutton">
                <a class="a1" href="/customer/login.aspx"></a>
                <a class="a2" href="/customer/login.aspx"></a>
                <a class="a3" href="/customer/shoppingcart.aspx"></a>
                <a class="a4" href="#"></a>
            </div>
            <p class="headtext">
                <%
                    if (NtContext.Current.CustomerLogined)
                    {
                        Response.Write("<a href=\"/customer/shoppingcart.aspx\">");
                        Response.Write("您好:");
                        Response.Write(NtContext.Current.CurrentCustomer.Name);
                        Response.Write("</a> 丨");
                    }
                    else {
                        Response.Write("<a href=\"/customer/login.aspx\">未登录</a> 丨");
                    }
                     %><a href="/about.aspx">关于我们</a> 丨<a href="/contact.aspx"> 联系我们</a> 丨<a href="/emap.aspx"> 电子地图</a></p>
        </div>
        <div class="headline"></div>
    </div>
    <div class="contentbox">
        <div class="content">
            <div class="searchbox">
                <input type="text" class="input01 smart-input" value="搜索您感兴趣的内容" name="myinput">
                <a href="#" class="searchbutton"></a>
            </div>
            <div style="height: 80px;" class="clear"></div>
