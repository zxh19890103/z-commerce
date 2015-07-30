<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false"
    CodeFile="index.aspx.cs" Inherits="netin_index" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <link href="/netin/content/css/index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://api.map.baidu.com/api?key=&v=1.1&services=true"></script>
    <script src="script/map.js" type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="body">
    <div class="layer-1">
        <div class="layer-1-left fl">
            <div class="layer-1-left-up">
                <img alt="welcome" src="/netin/content/images/welcome.png" />
            </div>
            <div class="layer-1-left-down">
                <div class="admin-profile fl">
                    <img alt="profile01" width="73" height="67" src="<%=MediaService.GetThumbnailUrl(NtContext.Current.CurrentUser.Profile,80,80,ThumbnailGenerationMode.HW)%>" />
                </div>
                <div class="admin-name fl">
                    NAME:<%=NtContext.Current.CurrentUser.UserName %>
                    <div class="logout-control" onclick="logout();">
                        退出登录
                    </div>
                </div>
            </div>
        </div>
        <div class="layer-1-right fr">
            <div class="layer-1-right-up">
                <span>您有: <a href="sale/orders.aspx?se.pending.order=true">
                    <%=TotalPendingOrder %></a>个未处理订单; <a href="guestbook/list.aspx?se.not.view=true">
                        <%=TotalUnViewedMessage %></a>个未查看留言; <a href="product/list.aspx">
                            <%=TotalProduct %></a>个产品; <a href="article/articles.aspx">
                                <%=TotalArticle %></a>篇文章; <a href="customer/list.aspx">
                                    <%=TotalCustomer %></a>个会员; </span>
            </div>
            <div class="layer-1-right-down">
                <div class="layer-1-right-down-inner">
                    <table>
                        <tr>
                            <td>文件空间使用大小:
                            </td>
                            <td>
                                <%=fileTotalSize/(1024*1024) %>Mb
                            </td>
                        </tr>
                        <tr>
                            <td>数据库空间使用大小:
                            </td>
                            <td>
                                <%=dbSize%>
                            </td>
                        </tr>
                        <tr>
                            <td>服务器环境:
                            </td>
                            <td>
                                <%=Environment.OSVersion.VersionString %>
                            </td>
                        </tr>
                        <tr>
                            <td>当前您的IP:
                            </td>
                            <td>
                                <%=WebHelper.GetIP() %>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="layer-2">
        <ul>
            <li>
                <a href="/netin/single/pages.aspx">
                    <img alt="" src="/netin/content/images/btn01.png" />
                    <span>页面管理</span>
                </a>
            </li>
            <li>
                <a href="/netin/article/articles.aspx">
                    <img alt="" src="/netin/content/images/btn02.png" />
                    <span>文章管理</span>
                </a>
            </li>
            <li>
                <a href="/netin/product/list.aspx">
                    <img alt="" src="/netin/content/images/btn03.png" />
                    <span>产品管理</span>
                </a>
            </li>
            <li>
                <a href="/netin/guestbook/list.aspx">
                    <img alt="" src="/netin/content/images/btn04.png" />
                    <span>留言管理</span>
                </a>
            </li>
            <li>
                <a href="/netin/goods/list.aspx">
                    <img alt="" src="/netin/content/images/btn05.png" />
                    <span>商品管理</span>
                </a>
            </li>
            <li>
                <a href="/netin/sale/orders.aspx">
                    <img alt="" src="/netin/content/images/btn06.png" />
                    <span>销售管理</span>
                </a>
            </li>
        </ul>
    </div>
    <hr class="index-line" />
    <div class="layer-3">

        <div class="ads-of-naite-text">
            <img alt="naite power" src="/netin/content/images/naite-power.png" />
        </div>

        <div class="ads-of-naite fl">
            <div class="ads-of-naite-page">
                <iframe width="100%" height="398" frameborder="0" scrolling="no" src="http://www.naite.com.cn/naiteguanggao"></iframe>
            </div>
        </div>

        <div class="contact-us fr">
            <div>
                <img alt="contact us" src="/netin/content/images/contact-us.png" />
            </div>
            <div class="contact-us-way">
                客户服务信箱：kefu1@naite.com.cn
                <br />
                客户咨询热线：0411-82528287 82527885
                <a title="449238915" href="http://wpa.qq.com/msgrd?v=3&uin=449238915&site=qq&menu=yes" target="_blank">
                    <img class="qq qq0" alt="" src="/netin/content/images/qq.png" /></a>
                <a title="707496807" href="http://wpa.qq.com/msgrd?v=3&uin=707496807&site=qq&menu=yes" target="_blank">
                    <img class="qq qq1" alt="" src="/netin/content/images/qq.png" /></a>
            </div>

            <div class="contact-us-map" id="dituContent">
            </div>
            <br />
            <div class="contact-us-lookforward">
                <img alt="lookforward.png" src="/netin/content/images/lookforward.png" />
                <hr class="clear index-line" />
                <address>地址：大连中山区人民路锦联大厦1505室</address>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        new nt.map();
    </script>

</asp:Content>
