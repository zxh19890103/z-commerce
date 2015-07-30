<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="articles.aspx.cs" Inherits="netin_article_articles" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <div class="list-tool-head">

        <form method="get" action="articles.aspx" id="searchForm">
            <table class="adminContent">
                <tr>
                    <td class="adminTitle">文章分类:</td>
                    <td class="adminData">
                        <%
                            NtUtility.ListItemSelect(Categories, Default("se.article.category",0).ToString());
                            HtmlRenderer.DropDownList(Categories, "se.article.category"); %>
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">文章ID号:</td>
                    <td class="adminData">
                        <input type="text" class="input-short" name="se.article.id" value="<%=Default("se.article.id",string.Empty) %>" />
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">文章标题:</td>
                    <td class="adminData">
                        <input type="text" class="input-long" name="se.article.title" value="<%=Default("se.article.title",string.Empty) %>" />
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">文章添加日期开始于:</td>
                    <td class="adminData">
                        <input type="text" class="input-datetime" name="se.article.createddate.start" value="<%=Default("se.article.createddate.start",NtConfig.MinDate.ToString("yyyy-MM-dd"))%>" />
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">文章添加日期结束于:</td>
                    <td class="adminData">
                        <input type="text" class="input-datetime" name="se.article.createddate.end" value="<%=Default("se.article.createddate.end",NtConfig.MaxDate.ToString("yyyy-MM-dd"))%>" />
                    </td>
                </tr>
            </table>
            <div>
                <input type="hidden" name="page" value="<%=Default("page",1) %>" />
                <a class="a-button" href="javascript:;" onclick="searchForm.submit();">搜索</a>
                <a class="a-button" href="javascript:;" onclick="clearSeach();">清除搜索</a>
            </div>
        </form>
    </div>
    <div class="list" id="list">
        <%List(); %>
    </div>
    <hr />
    <%
        Html("<a href=\"javascript:;\" onclick=\"batchMigrate(1);\">迁移当前文章至:</a>");
        HtmlRenderer.DropDownList(Categories, "ToCategoryId", 120);
    %>
</asp:Content>
