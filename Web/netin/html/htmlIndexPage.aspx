<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/netin/layout.master" CodeFile="htmlIndexPage.aspx.cs" Inherits="netin_html_htmlIndexPage" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #configCxt { width: 46.2%; height: 400px; }
    </style>
    <script type="text/javascript">

        $(function () {
            //get
            $.get(
                'htmlIndexPage.aspx?r=' + Math.random(),
                { action: 'GetConfig' },
                function (t) { $('#configCxt').val(t); },
                'text');
        });

        //save
        function postCxt() {
            var j = $('#configCxt');
            //var pattern=/^([\w\/]+\.aspx\s+[\w\/]+\.html\n)*$/;
            //if (!pattern.test(j.val()))
            //    alert('配置格式不正确!');
            nt.ajax({
                url: 'htmlindexpage.aspx',
                action: 'SaveConfig',
                data: { cxt: j.val() },
                success: function (j) {
                    success(j.message);
                }
            });
        }

        //htmlize
        function htmlize() {
            $.post(
                'htmlIndexPage.aspx?r=' + Math.random(),
                { action: 'htmlize' },
                function (j) {
                    if (j.error) error(j.message);
                    else {
                        $('#realtimer').text(j.message);
                        setTimeout(htmlRunMsgCaller, 1000, null);
                    }
                },
                'json');
        }

        function htmlRunMsgCaller() {
            $.get('htmlRunMsger.ashx?r=' + Math.random(),
                '',
                function (t) {
                    if (t != 'stopped') {
                        $('#realtimer').text(t);
                        setTimeout(htmlRunMsgCaller, 1000, null);
                    } else {
                        $('#realtimer').text('静态化已经完成!');
                    }
                },
            'text');
        }

        setTimeout(htmlRunMsgCaller, 1000, null);

    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">

    <div>
        <h4>编辑首页静态化配置文件</h4>
        <div class="tips-box">
            格式：<br />
            aspx文件绝对路径+空格+html文件绝对路径；如：/index.aspx&nbsp;&nbsp;/index.html；
            有多个项可以换行
        </div>
        <div>
            <textarea cols="5" rows="2" id="configCxt"></textarea>
        </div>
        <a class="a-button" href="javascript:;" onclick="postCxt();">保存Config</a>
        <a class="a-button" href="javascript:;" onclick="htmlize();">开始生成静态页</a>
        <span id="realtimer"></span>
    </div>

</asp:Content>
