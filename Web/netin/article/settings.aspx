<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="settings.aspx.cs" Inherits="netin_article_settings" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        //保存
        function save() {
            editForm.submit();
        }
    </script>

</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="body">

    <form action="settings.aspx" method="post" id="editForm">
        <table class="adminContent">

            <tr>
                <td class="adminTitle">列表页显示记录的个数：</td>
                <td class="adminData">
                    <input type="text" class="input-number" name="PageSize" value="<%=Model.PageSize%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">列表页页码的个数：</td>
                <td class="adminData">
                    <input type="text" class="input-number" name="PagerItemCount" value="<%=Model.PagerItemCount%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">公告：</td>
                <td class="adminData">
                    <textarea rows="1" cols="2" name="Notice" class="textarea-small"><%=Model.Notice %></textarea>
                </td>
            </tr>

        </table>
        <div class="submit">
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
        </div>
    </form>

</asp:Content>
