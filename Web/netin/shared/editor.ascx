<%@ Control Language="C#" AutoEventWireup="false" CodeFile="editor.ascx.cs" Inherits="Netin_Shared_editor" %>
<link rel="stylesheet" href="../Editor/themes/default/default.css" />
<link rel="stylesheet" href="../Editor/plugins/code/prettify.css" />
<script charset="utf-8" src="../Editor/kindeditor-all-min.js" type="text/javascript"></script>
<script charset="utf-8" src="../Editor/lang/zh_CN.js" type="text/javascript"></script>
<script charset="utf-8" src="../Editor/plugins/code/prettify.js" type="text/javascript"></script>
<script type="text/javascript">
    var <%=ID%>;
    KindEditor.ready(function (K) {
        <%=ID%>= K.create('textarea[name="<%=TextareaName%>"]', {
            cssPath: '../Editor/plugins/code/prettify.css',
            uploadJson: "../Editor/asp.net/upload_json.ashx",
            fileManagerJson: "../Editor/asp.net/file_manager_json.ashx",
            allowFileManager: false,
            items: [<%=Items%>],
            afterCreate: function () {
            }
        });
        prettyPrint();
    });
</script>
