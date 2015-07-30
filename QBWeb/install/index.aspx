<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="install_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Netin7.0系统安装</title>
    <link href="install.css" rel="stylesheet" />
    <script src="jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function install() {
            if (installForm.DataSource.value === '') { installForm.DataSource.focus(); message('服务器不能为空！'); return false; }
            if (installForm.DbName.value === '') { installForm.DbName.focus(); message('数据库名不能为空！'); return false; }
            if (!installForm.IntegratedSecurity.checked) {
                if (installForm.UserID.value === '') { installForm.UserID.focus(); message('用户名不能为空！'); return false; }
                if (installForm.Password.value === '') { installForm.Password.focus(); message('密码不能为空！'); return false; }
            }
            var data = {};
            data.DataSource = installForm.DataSource.value;
            data.DbName = installForm.DbName.value;
            data.UserID = installForm.UserID.value;
            data.Password = installForm.Password.value;
            data.DbExisting = installForm.DbExisting.value;
            data.IntegratedSecurity = installForm.IntegratedSecurity.value;
            $('#mask').fadeIn('fast');
            $.post(
            'index.aspx',
            data,
            function (j) {
                if (j.error) {
                    message(j.message);
                }
                else {
                    window.location.href = '/netin/login.aspx';
                }
                $('#mask').fadeOut('fast');
            },
             'json');
            return false;
        }

        function setCheck(sender) {
            if (sender.checked) {
                sender.value = 'true';
            }
            else {
                sender.value = 'false';
            }
            return sender.checked;
        }

        function message(m) {
            msg.innerHTML = m;
            msg.style.display = 'block';
        }
    </script>
</head>
<body>
    <div id="msg">
    </div>
    <div class="loading" id="loading">
    </div>
    <form id="installForm" action="index.aspx" method="post">
        <div class="install-box">
            <table>
                <tr>
                    <td class="cellTitle">服务器:
                    </td>
                    <td class="cellData">
                        <input type="text" name="DataSource" />
                    </td>
                </tr>
                <tr>
                    <td class="cellTitle">数据库名称:
                    </td>
                    <td class="cellData">
                        <input type="text" name="DbName" />
                    </td>
                </tr>
                <tr>
                    <td class="cellTitle">是否Windows集成验证:
                    </td>
                    <td class="cellData">
                        <input type="checkbox" name="IntegratedSecurity" onclick="var ops = $('tr.options', '#installForm'); if (setCheck(this)) ops.hide(); else ops.show();" />
                    </td>
                </tr>
                <tr class="options">
                    <td class="cellTitle">用户名:
                    </td>
                    <td class="cellData">
                        <input type="text" name="UserID" />
                    </td>
                </tr>
                <tr class="options">
                    <td class="cellTitle">密码:
                    </td>
                    <td class="cellData">
                        <input type="password" name="Password" />
                    </td>
                </tr>
                <tr>
                    <td class="cellTitle">数据库是否已存在:
                    </td>
                    <td class="cellData">
                        <input type="checkbox" value="false" onclick="setCheck(this);" name="DbExisting" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td align="center">
                        <input type="button" class="install-button" onclick="install();" value="安装" />
                        <input type="button" class="install-button" onclick="installForm.reset();" value="重置" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <div class="mask" id="mask">
    </div>
</body>
</html>
