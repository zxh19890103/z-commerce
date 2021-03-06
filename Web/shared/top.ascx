﻿<%@ Control Language="C#" ClassName="top" Inherits="Nt.Framework.BaseAscx" %>

<!DOCTYPE HTML>
<html>
<head>
    <title><%=WebsiteInfo.SeoTitle %></title>
    <link href="/content/css/bootstrap.css" rel='stylesheet' type='text/css' />
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script type="text/javascript" src="/js/jquery.min.js"></script>
    <!-- Custom Theme files -->
    <link href="/content/css/style.css" rel='stylesheet' type='text/css' />

    <link href="/content/css/style-additional.css" rel='stylesheet' type='text/css' />
    <link href="/content/css/customer.css" rel="stylesheet" />

    <!-- Custom Theme files -->
    <!---- start-smoth-scrolling---->
    <script type="text/javascript" src="/js/move-top.js"></script>
    <script type="text/javascript" src="/js/easing.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $(".scroll").click(function (event) {
                event.preventDefault();
                $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 1000);
            });
        });
    </script>
    <!---- start-smoth-scrolling---->
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script type="application/x-javascript"> 
    addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <!----webfonts--->
    <!--<link href='http://fonts.googleapis.com/css?family=Lato:100,300,400,700,900,100italic,300italic,400italic,700italic,900italic' rel='stylesheet' type='text/css'>-->
    <!---//webfonts--->
    <!----start-top-nav-script---->
    <script type="text/javascript">
        $(function () {
            var pull = $('#pull');
            menu = $('nav ul');
            menuHeight = menu.height();
            $(pull).on('click', function (e) {
                e.preventDefault();
                menu.slideToggle();
            });
            $(window).resize(function () {
                var w = $(window).width();
                if (w > 320 && menu.is(':hidden')) {
                    menu.removeAttr('style');
                }
            });
        });
    </script>
    <!----//End-top-nav-script---->
    <script src="/script/naite.utility.js" type="text/javascript"></script>
    <script src="/script/jquery.form.js" type="text/javascript"></script>
    <link rel="icon" href="/favicon.gif" type="image/x-icon" />
    <link rel="shortcut icon" href="/favicon.gif" type="image/x-icon" />
    
    <%
        PageBasedOn.RenderCssAndJsContent();
    %>
</head>
<body>
    <div class="container">
        <!----top-header---->
        <div class="top-header">
            <div class="logo">
                <a href="/index.aspx">
                    <img alt="" src="/content/images/logo.png" title="barndlogo" /></a>
            </div>
            <div class="top-header-info">
                <div class="top-contact-info">
                    <ul class="unstyled-list list-inline">
                        <li><span class="phone"></span>0411 - 82527872</li>
                        <li><span class="mail"></span><a href="mailto:zhangxinghai@naite.com.cn">zhangxinghai@naite.com.cn</a></li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="cart-details">
                    <div class="add-to-cart">
                        <ul class="unstyled-list list-inline">
                            <li><a href="/customer/myshoppingcart.aspx"><span class="cart"></span></a>
                                <ul class="cart-sub">
                                    <li>
                                        <p>
                                            <span id="shoppingcartItemTotal">
                                                <%=(NtContext.Current.CustomerLogined)?NtContext.Current.CurrentCustomer.TotalOfShoppingCartItem:0 %></span>
                                        </p>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                    <div class="login-rigister">
                        <ul class="unstyled-list list-inline">
                            <li>
                                <%
                                    if (NtContext.Current.CustomerLogined)
                                    {
                                        Response.Write("<label>您好:</label><a href=\"/customer/myaccount.aspx\">");
                                        Response.Write(NtContext.Current.CurrentCustomer.Name);
                                        Response.Write("</a>|<a class=\"login\" onclick=\"logout();\" href=\"javascript:;\">退出</a>");
                                    }
                                    else
                                    {
                                        Response.Write("<a class=\"login\" href=\"/customer/login.aspx\">登录</a>");
                                    }
                                %>
                            </li>
                            <li><a class="rigister" href="/customer/register.aspx">注册 <span></span></a></li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="clearfix"></div>
        </div>
        <!----//top-header---->
        <!---top-header-nav---->
        <div class="top-header-nav">
            <!----start-top-nav---->
            <div class="top-nav main-menu" id="nav">
                <ul class="top-nav">
                    <li><a href="/list.aspx">产品 </a><span></span></li>
                    <li><a href="/campaigns.aspx">活动</a><span> </span></li>
                    <li><a href="/service.aspx">服务</a><span> </span></li>
                    <li><a href="/brands.aspx">品牌</a><span> </span></li>
                    <li><a href="/aboutus.aspx">关于</a></li>
                </ul>
                <div class="clearfix"></div>
                <a href="#" id="pull">
                    <img alt="" src="/content/images/nav-icon.png" title="menu" /></a>
            </div>
            <!----End-top-nav---->
            <!---top-header-search-box--->
            <div class="top-header-search-box">
                <form action="/list.aspx">
                    <input type="text" maxlength="22" />
                    <input type="submit" value=" " />
                </form>
            </div>
            <!---top-header-search-box--->
            <div class="clearfix"></div>
        </div>
    </div>
    <!--//top-header-nav---->
