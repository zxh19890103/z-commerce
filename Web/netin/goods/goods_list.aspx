<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="goods_list.aspx.cs" Inherits="Netin_Goods_goods_list" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script src="../script/goods.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <div class="list-tool-head">
        <form method="get" action="goods_list.aspx" id="searchForm">
            <table class="adminContent">
                <tr>
                    <td class="adminTitle">商品分类:</td>
                    <td class="adminData">
                        <%
                            NtUtility.ListItemSelect(Categories, Default("se.goods.class", 0).ToString());
                            HtmlRenderer.DropDownList(Categories, "se.goods.class"); %>
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">商品货号:</td>
                    <td class="adminData">
                        <input type="text" class="input-short" name="se.goods.goodsguid" value="<%=Default("se.goods.goodsguid",string.Empty) %>" />
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">商品ID号:</td>
                    <td class="adminData">
                        <input type="text" class="input-short" name="se.goods.id" value="<%=Default("se.goods.id",string.Empty) %>" />
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">商品名称:</td>
                    <td class="adminData">
                        <input type="text" class="input-long" name="se.goods.name" value="<%=Default("se.goods.name",string.Empty) %>" />
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">商品添加日期开始于:</td>
                    <td class="adminData">
                        <input type="text" class="input-datetime" name="se.goods.createddate.start" value="<%=Default("se.goods.createddate.start",NtConfig.MinDate.ToString("yyyy-MM-dd"))%>" />
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">商品添加日期结束于:</td>
                    <td class="adminData">
                        <input type="text" class="input-datetime" name="se.goods.createddate.end" value="<%=Default("se.goods.createddate.end",NtConfig.MaxDate.ToString("yyyy-MM-dd"))%>" />
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
        Html("<a href=\"javascript:;\" onclick=\"batchMigrate(3);\">迁移当前商品至:</a>");
        HtmlRenderer.DropDownList(Categories, "ToCategoryId", 120);
    %>
</asp:Content>
