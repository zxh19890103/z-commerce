<%@ Page Language="C#" AutoEventWireup="false" CodeFile="info_view.aspx.cs" Inherits="Netin_Customer_info_view" %>

<%@ Register Src="~/netin/shared/head.ascx" TagPrefix="uc1" TagName="head" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>会员信息</title>
    <uc1:head runat="server" ID="head" />
</head>
<body>
    <table class="adminContent adminContentView">
        <tr>
            <td class="adminTitle">会员组：</td>
            <td class="adminData"><%=Model.RoleName %></td>
        </tr>
        <tr>
            <td class="adminTitle">会员名：</td>
            <td class="adminData"><%=Model.Name %></td>
        </tr>
        <tr>
            <td class="adminTitle">真实姓名：</td>
            <td class="adminData"><%=Model.RealName %></td>
        </tr>
        <tr>
            <td class="adminTitle">生日：</td>
            <td class="adminData"><%=Model.BirthDay.ToString("yyyy/MM/dd") %></td>
        </tr>
        <tr>
            <td class="adminTitle">性别：</td>
            <td class="adminData"><%=Model.Gender?"男":"女" %></td>
        </tr>
        <tr>
            <td class="adminTitle">积分值：</td>
            <td class="adminData"><%=Model.Points %></td>
        </tr>
        <tr>
            <td class="adminTitle">启用状态：</td>
            <td class="adminData"><%=Model.Active?"启用":"未启用" %></td>
        </tr>
        <tr>
            <td class="adminTitle">登录次数：</td>
            <td class="adminData"><%=Model.LoginTimes %></td>
        </tr>
        <tr>
            <td class="adminTitle">上次登录IP：</td>
            <td class="adminData"><%=Model.LastLoginIP %></td>
        </tr>
        <tr>
            <td class="adminTitle">上次登录日期：</td>
            <td class="adminData"><%=Model.LastLoginDate %></td>
        </tr>
        <tr class="adminSeparator">
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td class="adminTitle">邮箱：</td>
            <td class="adminData"><%=Model.Email %></td>
        </tr>
        <tr>
            <td class="adminTitle">固话：</td>
            <td class="adminData"><%=Model.Phone %></td>
        </tr>
        <tr>
            <td class="adminTitle">手机：</td>
            <td class="adminData"><%=Model.Mobile %></td>
        </tr>
        <tr>
            <td class="adminTitle">住址：</td>
            <td class="adminData"><%=Model.Address %></td>
        </tr>
        <tr>
            <td class="adminTitle">邮编：</td>
            <td class="adminData"><%=Model.Zip %></td>
        </tr>
        <tr>
            <td class="adminTitle">注册日期：</td>
            <td class="adminData"><%=Model.CreatedDate %></td>
        </tr>
    </table>
    <div class="submit">
        <a href="javascript:;" class="a-button" onclick="window.close();">关闭</a>
    </div>
</body>
</html>
