<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" Debug="true" AutoEventWireup="false" CodeFile="backupfiles.aspx.cs" Inherits="netin_db_backupfiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //下载备份
        function downloadBackup(n) {
            if (window.downloadIfr === undefined) {
                var iframe = document.createElement("iframe");
                iframe.style.display = 'none';
                window.downloadIfr = iframe;
                document.body.appendChild(iframe);
            }
            window.downloadIfr.src = '/netin/handlers/downloader.ashx?filename=' + n;
        }

        /*
        删除备份文件
        */
        function delBackup(i, n) {
            confirm('您确定删除此文件?', function () {
                nt.ajax({
                    action: 'delBackup',
                    data: { filename: n },
                    success: function (j) {
                        if (j.error) error(j.message);
                        else {
                            success('删除成功');
                            $('#id_' + i).remove();
                        }
                    }
                });
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <%
        RenderBKFilesList("脚本备份", "backup-sql", "backup_*.sql");
        RenderBKFilesList("文件备份", "backup-bak", "backup_*.bak");
        RenderBKFilesList("数据库创建脚本", "sql-sql", "sql_*.sql");
    %>
</asp:Content>

