<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false"
    CodeFile="authorize.aspx.cs" Inherits="netin_user_authorize" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        #level
        {
            width: 150px;
            margin: 3px 0;
            margin-left: 10px;
        }
        .choices
        {
            margin-left: 10px;
        }
    </style>
    <script type="text/javascript">
        function submit() {
            editForm.submit();
        }

        var nt = nt || {};

        function levelChanged(v) {
            window.location.href = "authorize.aspx?level=" + v;
        }

        /*目录全选*/
        function categoryClick(sender) {
            var id = $(sender).val(),
                c = sender.checked;
            $('.father-' + id).each(function () {
                $(this).attr('checked', c);
            });
        }

        /*
        当每个子项被点击之后，
        我们需要判断它所在的目录所包含的子项是否处于全反选状态，
        若是则目录节点上为未选中状态，
        否则为选中状态
        */
        function nodeClick(sender) {
            var id = sender.value,
                father = sender.getAttribute('data-father-id'),
                c = sender.checked;
            if (c) {
                $('#pms_' + father).attr('checked', true);
            } else {
                var checkedCount = $(sender).siblings(':checked').size();
                if (checkedCount == 0) {
                    $('#pms_' + father).attr('checked', false);
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <form method="post" action="authorize.aspx" id="editForm">
    <div class="levels">
        <%HtmlRenderer.DropDownList(Levels, "level", "levelChanged(this.value);"); %>
    </div>
    <div class="clear">
    </div>
    <div class="choices">
        <%RenderRecords(); %>
    </div>
    <div class="submit">
        <a href="javascript:;" onclick="submit();" class="a-button">保存授权</a> <a href="javascript:;"
            onclick="editForm.reset();" class="a-button">重置授权</a>
    </div>
    </form>
</asp:Content>
