<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="ask.aspx.cs" Inherits="netin_goods_ask" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">

        //打开编辑备注的窗口
        function askNote(id) {
            $('#AskId', '#askNoteEditor').val(id);
            $('#askNoteEditor').center().movable();
            nt.showMask();
        }

        /*保存备注*/
        function saveAskNote() {
            nt.ajax({
                data: { id: AskId.value, note: $('#askNoteValue').val() },
                action: 'SaveAskNote',
                success: function (json) {
                    closeEditor();
                    alert(json.message);
                }
            });
        }

        function closeEditor() {
            $('#askNoteEditor').hide();
            nt.removeMask();
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <div class="list" id="list">
        <%List(); %>
    </div>

    <div class="html-content" id="askNoteEditor">
        <div class="html-content-top nt-drag-bar">
            <span>商品咨询备注</span>
            <a href="javascript:;" onclick="closeEditor();">x</a>
        </div>
        <div class="html-content-body">
            <textarea rows="2" cols="1" id="askNoteValue" class="textarea-normal"></textarea>
        </div>
        <div class="html-content-footer">
            <input type="hidden" id="AskId" name="AskId" />
            <a href="javascript:;" class="a-button" onclick="saveAskNote();">保存修改</a>
            <a href="javascript:;" class="a-button" onclick="closeEditor();">关闭窗口</a>
        </div>
    </div>
</asp:Content>
