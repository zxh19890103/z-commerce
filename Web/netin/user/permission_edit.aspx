<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="permission_edit.aspx.cs" Inherits="netin_user_permission_edit" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        #sysNames { width: 800px; height: auto; display: none; }
            #sysNames li { margin: 10px; }
    </style>
    <script type="text/javascript">
        //保存
        function save() {
            if (editForm.SystemName.value == '') {
                alert('系统名不能为空!');
                return;
            }
            editForm.submit();
        }

        /*选择系统名*/
        function selectSysN(sender) {
            nt.removeMask();
            $('#sysNames').hide();
            editForm.SystemName.value = $(sender).text();
        }

        /*打开系统名选项*/
        function openSysNSelector() {
            nt.showMask();
            $('#sysNames').center();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <form action="<%=Request.RawUrl %>" method="post" id="editForm">
        <table class="adminContent">
            <tr>
                <td class="adminTitle">FatherId:</td>
                <td class="adminData">
                    <input type="text" class="input-number" maxlength="9" readonly="readonly" name="FatherId" value="<%=Model.FatherId %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">IsCategory:</td>
                <td class="adminData">
                    <%=Model.IsCategory %>
                    <input type="hidden" name="IsCategory" value="<%=Model.IsCategory %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">SystemName:</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="SystemName" value="<%=Model.SystemName%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">Name:</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="Name" value="<%=Model.Name%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">EnglishName:</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="EnglishName" value="<%=Model.EnglishName%>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">Src:</td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="512" name="Src" value="<%=Model.Src %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">Display:</td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.Display, "Display"); %>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">DisplayOrder:</td>
                <td class="adminData">
                    <input type="text" class="input-number" maxlength="9" name="DisplayOrder" value="<%=Model.DisplayOrder %>" />
                </td>
            </tr>

        </table>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=NtID %>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
            <a class="a-button" href="<%BackScript(); %>">返回</a>
        </div>
    </form>
</asp:Content>
