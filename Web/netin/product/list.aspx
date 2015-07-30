<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="list.aspx.cs" Inherits="netin_product_list" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <div class="list-tool-head">
        <form method="get" action="list.aspx" id="searchForm">
            <table class="adminContent">
                <tr>
                    <td class="adminTitle">产品类别:</td>
                    <td class="adminData">
                        <%
                            NtUtility.ListItemSelect(Categories, Default("se.product.category", 0).ToString());
                            HtmlRenderer.DropDownList(Categories, "se.product.category"); %>
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">产品ID号:</td>
                    <td class="adminData">
                        <input type="text" class="input-short" name="se.product.id" value="<%=Default("se.product.id",string.Empty) %>" />
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">产品名称:</td>
                    <td class="adminData">
                        <input type="text" class="input-long" name="se.product.name" value="<%=Default("se.product.name",string.Empty) %>" />
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">产品添加日期开始于:</td>
                    <td class="adminData">
                        <input type="text" class="input-datetime" name="se.product.createddate.start" value="<%=Default("se.product.createddate.start",NtConfig.MinDate.ToString("yyyy-MM-dd"))%>" />
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">产品添加日期结束于:</td>
                    <td class="adminData">
                        <input type="text" class="input-datetime" name="se.product.createddate.end" value="<%=Default("se.product.createddate.end",NtConfig.MaxDate.ToString("yyyy-MM-dd"))%>" />
                    </td>
                </tr>
            </table>
            <div>
                <input type="hidden" name="page" value="<%=Default("page",1)   %>" />
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
        Html("<a href=\"javascript:;\" onclick=\"batchMigrate(2);\">迁移当前产品至:</a>");
        HtmlRenderer.DropDownList(Categories, "ToCategoryId", 120);
    %>
</asp:Content>
