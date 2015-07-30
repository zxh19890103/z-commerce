<%@ Page Language="C#" AutoEventWireup="false" Title="缓存文件管理" MasterPageFile="~/netin/layout.master" CodeFile="cachefileMgr.aspx.cs" Inherits="netin_common_cachefileMgr" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        #file-list { }
        #editCtrl { width: 600px; height: 400px; }
        #htmlPreView { border: 1px solid #ddd; width: 600px; min-height: 20px; margin-bottom: 20px; }
    </style>
    <script type="text/javascript">
        //加载缓存文件内容
        function loadFileCxt(sender) {
            var f = $(sender).text();
            $.get(
                'cachefilemgr.aspx?r=' + Math.random(),
                { action: 'FetchCacheFileCxt', file: f },
                function (j) {
                    $('#editCtrl').val(j.cache);
                    $('#htmlPreView').html(j.cache);
                    $('#file').val(f);
                },
            'json');
        }

        //保存修改
        function postEdit() {
            var j0 = $('#file'), j1 = $('#editCtrl');
            $.post(
               'cachefilemgr.aspx?r=' + Math.random(),
               { action: 'PostCacheFileCxt', file: j0.val(), cache: j1.val() },
               function (j) {
                   success(j.message);
                   $('#htmlPreView').html('');
                   j0.val('');
                   j1.val('');
               },
           'json');
        }
        
        //清除全部文件缓存
        function clearCacheFiles() {
            $.post(
                'cachefilemgr.aspx?r=' + Math.random(),
                { action: 'clearCacheFiles' },
                function (j) {
                    if (j.error)
                        error(j.message);
                    else {
                        success(j.message);
                        $('#file-list').empty();
                    }
                },
                'json');
        }

    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="server">

    <div>
        <a class="a-button" href="javascript:;" onclick="clearCacheFiles();">清除全部文件缓存</a>
    </div>

    <div id="file-list">
        <%OutAllCacheFile(); %>
    </div>

    <div>
        <div id="htmlPreView">
        </div>
        <textarea id="editCtrl" cols="2" rows="1" onkeyup="$('#htmlPreView').html($(this).val());"></textarea>
        <input type="hidden" name="file" id="file" value="" />
        <br />
        <a class="a-button" href="javascript:;" onclick="postEdit();">保存修改</a>
    </div>

</asp:Content>
