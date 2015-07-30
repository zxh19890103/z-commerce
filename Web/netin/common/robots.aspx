<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/netin/layout.master" CodeFile="robots.aspx.cs" Inherits="netin_common_robots" %>

<asp:Content ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function save() {
            editForm.submit();
        }
    </script>

</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">

    <form action="robots.aspx" method="post" id="editForm">

        <div class="submit-top">
            关于Robots的相关知识，请参考
            <a href="http://baike.baidu.com/link?url=Icprvsqks9fBC_rqL_sCbjPcdNybJIP-h9sN6VgXKmScz-pFCebg2Wkvdzp4s7zBRJOxZv58AUxub-u70zL2sKlkth1TmXkhUX942iZzhUfFJhD4TDEEp4c0QbIaegS1" target="_blank">百度百科</a>
        </div>
        <table class="adminContent">
            <tr>
                <td class="adminTitle">User-agent</td>
                <td class="adminData">
                    <input type="text" name="User-agent" value="<%=rh.UserAgent %>" />
                    <span class="tips">搜索引擎种类，* 代表所有搜索引擎种类</span>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">编辑Robots.txt内容
                </td>
                <td class="adminData">
                    <textarea name="RobotsBlock" cols="1" rows="2" class="textarea-big"><%=rh.RobotsBlock %></textarea>
                    <span class="tips">请严格按照Robots的格式编辑</span>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">Sitemap文件
                </td>
                <td class="adminData">
                    <input type="text" name="Sitemap" value="<%=rh.Sitemap %>" />
                    <span class="tips">指定sitema.xml的路径，便于搜索引擎快速抓取</span>
                </td>
            </tr>
        </table>
        <div class="submit">
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
        </div>

    </form>

</asp:Content>
