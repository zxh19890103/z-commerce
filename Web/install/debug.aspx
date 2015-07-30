<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<!DOCTYPE html>

<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);



        //var _sql = new StringBuilder();
        //string uid = string.Format("nt_login_{0}", "qqq"),
        //        pwd = NtUtility.GetRandomPwd();
        //Response.Write("\r\n");
        //_sql.AppendFormat("EXEC sp_addlogin N'{0}','{1}';\r\n", uid, pwd);
        //_sql.AppendFormat("EXEC sp_grantdbaccess N'{0}';\r\n", uid);
        //_sql.AppendFormat("EXEC sp_addrolemember N'db_owner',N'{0}';\r\n", uid);
        //_sql.AppendFormat("EXEC sp_revokedbaccess N'{0}';\r\n", uid);
        //Response.Write(_sql);
        //var installer = new InstallService("qqq", @"michael-pc\SQLEXPRESS");
        //installer.Install();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <%
        ChannelNo = 1;

        SetCatalogDataSource(1);

        //RenderCatalog(0, 3, "<a href=\"kkk.asp?id={value}\">{text}</a>");

         RenderMenu("ul", "li", "haha", "<a href=\"{url}\" target=\"{target}\">{text}</a>", 3, "id=\"sd\"");

        //RenderCrumbs(2, 5, "<a href=\"http://www.baidu.com?sid={value}\">{text}</a>", ">>>>>>");
    
    //RenderLeftMenu(3);
    
    %>
</body>
</html>
