<%@ Page Language="C#" %>

<!DOCTYPE html>

<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        var _sql = new StringBuilder();
        string uid = string.Format("nt_login_{0}", "qqq"),
                pwd = NtUtility.GetRandomPwd();
        Response.Write(pwd);
        Response.Write("\r\n");
        _sql.AppendFormat("EXEC sp_addlogin N'{0}','{1}';\r\n", uid, pwd);
        _sql.AppendFormat("EXEC sp_grantdbaccess N'{0}';\r\n", uid);
        _sql.AppendFormat("EXEC sp_addrolemember N'db_owner',N'{0}';\r\n", uid);
        _sql.AppendFormat("EXEC sp_revokedbaccess N'{0}';\r\n", uid);
        Response.Write(_sql);
        var installer = new InstallService("qqq", @"michael-pc\SQLEXPRESS");
        installer.Install();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
</body>
</html>
