<%@ Page Language="C#" AutoEventWireup="false" CodeFile="shoppingcart_info.aspx.cs" Inherits="netin_sale_shoppingcart_info" %>

<%@ Register Src="~/netin/shared/head.ascx" TagPrefix="uc1" TagName="head" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=Title %></title>
    <uc1:head runat="server" ID="head" />
    <style type="text/css">
        attributeXml {
            display: block;
            font-size: 12px;
            border: 1px solid #ddd;
            width: 300px;
        }

            attributeXml add {
                display: block;
                padding: 1em 0 1em 2em;
                border-bottom: 1px dashed #ddd;
            }

                attributeXml add label {
                    padding-right: 10px;
                    font-weight:600;
                }
    </style>
</head>
<body>
    <table class="adminContent adminContentView">
        <tr>
            <td class="adminTitle">货物：</td>
            <td class="adminData"><a href="../goods/goods_edit.aspx?id=<%=Model.GoodsId %>"><%=Model.GoodsName %></a></td>
        </tr>
        <tr>
            <td class="adminTitle">货物照片：</td>
            <td class="adminData">
                <img src="<%=MediaService.MakeThumbnail(Model.PictureUrl,200,200)%>" alt="<%=Model.PictureUrl%>" /></td>
        </tr>
        <tr>
            <td class="adminTitle">会员：</td>
            <td class="adminData"><a href="javascript:;" onclick="openWindow('../customer/info_view.aspx?id=<%=Model.CustomerId %>');"><%=Model.CustomerName %></a></td>
        </tr>
        <tr>
            <td class="adminTitle">数量：</td>
            <td class="adminData"><%=Model.Quantity %></td>
        </tr>
        <tr>
            <td class="adminTitle">属性Xml：</td>
            <td class="adminData"><%=Model.AttributesXml %></td>
        </tr>
    </table>
    <div class="submit">
        <a href="javascript:;" class="a-button" onclick="window.close();">关闭</a>
    </div>
</body>
</html>
