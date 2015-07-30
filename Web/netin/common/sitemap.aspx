<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false"
    CodeFile="sitemap.aspx.cs" Inherits="netin_common_sitemap" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #content_container { background-color: #f6f6f6; border-left: 1px solid #cccccc; }
        #content { overflow: hidden; }

        table#sitemap_table > tbody > tr:hover { background-color: #f6f6f6; }
    </style>
    <script type="text/javascript">

        var BAIDU_SITEMAP=<%=(int)SitemapType.Baidu%>;
        var GOOGLE_SITEMAP=<%=(int)SitemapType.Google%>;

        function GenSitemap(t,t2) {
            nt.ajax({
                action: 'GenSitemap',
                data: { type: t, isHtml: false,byMenu:t2 },
                success: function (json) {
                    if (json.error) {
                        error(json.message);
                    } else {
                        alert(json.message, function () {
                            var bog = t == BAIDU_SITEMAP ? '百度' : '谷歌';
                            var html = [
                            '<div>' + bog + '网站地图生成成功,共<font color="red">' + json.countOfFound + '</font>条记录被检索.</div>',
                            '<div><a target="_blank" href="' + json.sitemapPath + '"><font color="red">查看Sitemap.xml</font></a>,',
                            '<a target="_blank"  href="' + json.postUrl + '">提交至' + bog + '</a>',
                            '</div>'
                            ].join('');
                            $('#sitemap-generated-message').html(html);
                        });
                    }
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="server">
    <div class="tips">
        请勿重复提交您的网站，否则容易被搜索引擎视为作弊行为。
    </div>
    <div id="sitemap-generated-message">
    </div>
    <table class="adminListView" id="sitemap_table">
        <tr>
            <td height="150" align="center">
                <img alt="" src="/Netin/Content/Images/logo.baidu.jpg" width="215" vspace="15" />
            </td>
            <td align="center">
                <img alt="" src="/Netin/Content/Images/logo.google.jpg" width="215" vspace="15" />
            </td>
            <td align="center">
                <img alt="" src="/Netin/Content/Images/logo.sogou.gif" width="215" vspace="15" />
            </td>
        </tr>
        <tr>
            <td height="26" align="center">
                <a href="http://zhanzhang.baidu.com/sitesubmit/index/" class="a-button" target="_blank">提交网站入口</a>
                <br />
                <a href="javascript:;" class="admin-a-button" onclick="GenSitemap(<%=(int)SitemapType.Baidu %>,false);">根据相关数据表生成baidu地图</a>
                <a href="javascript:;" class="admin-a-button" onclick="GenSitemap(<%=(int)SitemapType.Baidu %>,true);">根据导航生成baidu地图</a>
            </td>
            <td align="center">
                <a href="http://www.google.com/submit_content.html" class="a-button" target="_blank">提交网站入口</a>
                 <br />
                <a href="javascript:;" class="admin-a-button" onclick="GenSitemap(<%=(int)SitemapType.Google %>,false);">根据相关数据表生成google地图</a>
                <a href="javascript:;" class="admin-a-button" onclick="GenSitemap(<%=(int)SitemapType.Google %>,true);">根据导航生成google地图</a>
            </td>
            <td align="center">
                <a href="http://www.sogou.com/feedback/urlfeedback.php" class="a-button" target="_blank">提交网站入口</a>
            </td>
        </tr>
        <tr>
            <td height="150" align="center">
                <img alt="" src="/Netin/Content/Images/logo.bing.jpg" width="222" vspace="15" />
            </td>
            <td align="center">
                <img alt="" src="/Netin/Content/Images/logo.360.png" width="222" vspace="15" />
            </td>
            <td align="center">
                <img alt="" src="/Netin/Content/Images/logo.soso.jpg" width="222" vspace="15" />
            </td>
        </tr>
        <tr>
            <td height="26" align="center">
                <a href="http://www.bing.com/toolbox/submit-site-url/" class="a-button" target="_blank">提交网站入口</a>
            </td>
            <td align="center">
                <a href="http://hao.360.cn/url.html" class="a-button" target="_blank">提交网站入口</a>
            </td>
            <td align="center">
                <a href="http://www.sousuoyinqingtijiao.com/soso/" class="a-button" target="_blank">提交网站入口</a>
            </td>
        </tr>
    </table>
</asp:Content>
