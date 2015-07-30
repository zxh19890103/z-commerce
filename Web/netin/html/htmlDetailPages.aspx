<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/netin/layout.master" CodeFile="htmlDetailPages.aspx.cs" Inherits="netin_html_htmlDetailPages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #configCxt { width: 46.2%; height: 400px; }
        #realtimer { width: 300px; height: 50px; padding: 10px; border: 1px solid #eee; text-align: center; margin: 100px auto; }
    </style>
    <script type="text/javascript">
        //htmlize
        function htmlize(flag) {
            $.post(
                'htmlIndexPage.aspx?r=' + Math.random(),
                { action: 'htmlize', flag: flag },
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

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div>
        <a class="a-button" href="javascript:;" onclick="htmlize(1);">开始生成文章静态页</a>
        <a class="a-button" href="javascript:;" onclick="htmlize(2);">开始生成产品静态页</a>
        <a class="a-button" href="javascript:;" onclick="htmlize(3);">开始生成商品静态页</a>
        <a class="a-button" href="javascript:;" onclick="htmlize(4);">开始生成二级页静态页</a>
        <div id="realtimer"></div>
    </div>

</asp:Content>
