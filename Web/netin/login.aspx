<%@ Page Language="C#" AutoEventWireup="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登录</title>
    <link href="content/css/common.css" rel="stylesheet" />
    <link href="content/css/login.css" rel="stylesheet" />
    <script src="script/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="script/jquery.easing.min.js" type="text/javascript"></script>
    <script src="script/common.js" type="text/javascript"></script>

    <script type="text/javascript">
        /*
        登录
        */
        function submitLogin() {
            $('#login-form').ajaxSubmit2({
                url: 'handlers/ajaxHandler.aspx',
                action: 'login',
                mutiform_data: {},
                success: function (json) {
                    if (!json.error)
                        window.location.href = 'index.aspx';
                    else
                        error(json.message);
                }
            });
            return false;
        }

        $(function () {
            registerSmartInput('.form-control');
        });

    </script>

</head>
<body>
    <div class="login-content">
        <form method="post" id="login-form" action="login.aspx">
            <h2 class="text-center">奈特原动力 网站管理系统<span class="opacity-80">v7.1</span></h2>
            <div class="content-box">
                <div class="content-box-wrapper">
                    <div class="form-group">
                        <label for="exampleInputUser">用户名:</label>
                        <div class="form-input">
                            <span class="login-icon"><i class="login-icon-user"></i></span>
                            <input type="text" id="exampleInputUser" name="userName" value="用户名" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputPassword">密码:</label>
                        <div class="form-input">
                            <span class="login-icon"><i class="login-icon-password"></i></span>
                            <input type="password" id="exampleInputPassword" name="password" value="123" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputCheckcode">验证码:</label>
                        <div class="form-input">
                            <span class="login-icon"><i class="login-icon-checkcode"></i></span>
                            <input type="text" id="exampleInputCheckcode" name="checkCode" value="验证码" class="form-control" />
                            <div class="checkcode-wrapper">
                                <img class="checkcode-image" id="checkCode" alt="checkcode生成无效" src="handlers/checkCodeGenerator.aspx" />
                                <a href="javascript:;" style="margin: 0; padding-left: 5px;" onclick="refreshCode('checkCode');">换一张</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="button-panel">
                    <a href="javascript:;" onclick="submitLogin();" class="a-button" id="submit-button">登录</a>
                </div>
            </div>
        </form>
        <div class="corpyRight">
            CORPYRIGHT© 2013-<%=DateTime.Now.Year %>
            <a href="http://naite.com.cn">NAITE.COM.CN</a> ALL RIGHT RESERVED
        </div>
    </div>
</body>
</html>
