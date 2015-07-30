<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="backup.aspx.cs" Inherits="netin_db_backup" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        #download_backup { display: none; cursor: pointer; color: #808080; }
            #download_backup :hover { text-decoration: underline; }
    </style>
    <script type="text/javascript">

        //备份数据库
        function backup() {
            var data = {};
            data.type = backupType_0.checked ? 0 : 1;
            //以脚本形式备份
            if (data.type == 0) {
                var str_array = [];
                var str_array1 = [];
                $('input[name="tables"]', '#tables_selections').each(function (i, n) {
                    if (n.checked) {
                        str_array.push(n.value);
                        str_array1.push(n.getAttribute('data-text'));
                    }
                });
                if (str_array.length < 1) {
                    error('请选择至少一个数据表!');
                    return;
                }
                data.tables_id = str_array.join(',');
                data.tables = str_array1.join(',');
            }

            nt.ajax({
                action: 'Backup',
                data: data,
                success: function (json) {
                    if (json.error) error(json.message);
                    else {
                        success(json.message);
                        $('#download_backup').show().attr('data-bak-name', json.link);
                    }
                }
            });
        }

        //全选表
        function selectAllTables() {
            $('input', '#tables').attr('checked', select_all_tables.checked);
        }

        //切换备份形式
        function switchType(t) {
            if (t === 0) {
                if (!window.tablesinfoLoaded) {
                    nt.ajax({
                        action: 'RenderTablesInfo',
                        success: function (s) {
                            tables_selections.innerHTML = s;
                            window.tablesinfoLoaded = true;
                        },
                        type: 'text'
                    });
                }
                tables_selections.style.display = 'block';
            } else {
                if (window.tablesinfoLoaded)
                    tables_selections.style.display = 'none';
            }
        }

        //下载备份
        function downloadBackup(s) {
            if (window.downloadIfr === undefined) {
                var iframe = document.createElement("iframe");
                iframe.style.display = 'none';
                window.downloadIfr = iframe;
                document.body.appendChild(iframe);
            }
            var name = $(s).attr('data-bak-name');
            window.downloadIfr.src = '/netin/handlers/downloader.ashx?filename=' + name;            
        }

        //重写数据库脚本
        function reWriteSql() {
            nt.ajax({
                action: 'reWriteSql',
                success: function (json) {
                    if (json.error) error(json.message);
                    else {
                        success(json.message);
                        $('#download_backup').show().attr('data-bak-name', json.link);
                    }
                }
            });
        }

    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="body">
    <div>
        <h4>选择备份形式：</h4>
        <span>
            <input id="backupType_1" type="radio" onclick="switchType(1);" name="backupType" value="1" checked="checked" />
            <label for="backupType_1" onclick="switchType(1);">以文件形式备份</label>
            <input id="backupType_0" type="radio" onclick="switchType(0);" name="backupType" value="0" />
            <label onclick="switchType(0);" for="backupType_0">以脚本形式备份</label>
        </span>
        <hr />
        <div id="tables_selections">
        </div>
        <div id="download_backup" data-bak-name="" onclick="downloadBackup(this);">
            下载备份文件
        </div>
        <a class="a-button" href="javascript:;" onclick="backup();">开始备份</a>
        <a class="a-button" href="javascript:;" onclick="reWriteSql();">重新生成数据库脚本</a>
    </div>
    <script type="text/javascript">
        backupType_1.checked = true;
    </script>
</asp:Content>
