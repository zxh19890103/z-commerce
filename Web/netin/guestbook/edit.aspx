<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="edit.aspx.cs" Inherits="netin_guestbook_edit" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        /*文章保存*/
        function save() {
            editForm.submit();
        }
    </script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <form action="<%=Request.RawUrl %>" method="post" id="editForm">
        <div class="submit-top">
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
        </div>
        <table class="adminContent" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="adminTitle">留言分类:
                </td>
                <td class="adminData">
                    <%
                        NtUtility.ListItemSelect(StaticDataProvider.Instance.GuestBookTypeProvider, Model.Type);
                        HtmlRenderer.DropDownList(StaticDataProvider.Instance.GuestBookTypeProvider, "Type");
                    %>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">标题:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="256" name="Title" value="<%=Model.Title %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">留言者姓名:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="256" name="Name" value="<%=Model.Name %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">性别:
                </td>
                <td class="adminData">
                    <%HtmlRenderer.XY(Model.Gender, "Gender"); %>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">出生年月日:
                </td>
                <td class="adminData">
                    <%HtmlRenderer.BirthDay(Model.BirthDate, "BirthDate"); %>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">固话:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="256" name="Tel" value="<%=Model.Tel %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">手机:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="256" name="Mobile" value="<%=Model.Mobile %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">邮箱:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="256" name="Email" value="<%=Model.Email %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">祖籍:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="256" name="NativePlace" value="<%=Model.NativePlace %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">现住址:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="256" name="Address" value="<%=Model.Address %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">民族:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="256" name="Nation" value="<%=Model.Nation %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">身份证ID:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="256" name="PersonID" value="<%=Model.PersonID %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">学历:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="256" name="EduDegree" value="<%=Model.EduDegree %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">毕业学校:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="256" name="GraduatedFrom" value="<%=Model.GraduatedFrom %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">在校成绩:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="256" name="Grade" value="<%=Model.Grade %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">政治面貌:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="256" name="PoliticalRole" value="<%=Model.PoliticalRole %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">公司:
                </td>
                <td class="adminData">
                    <input type="text" class="input-long" maxlength="1024" name="Company" value="<%=Model.Company %>" />
                </td>
            </tr>

            <tr>
                <td class="adminTitle">备注:
                </td>
                <td class="adminData">
                    <textarea rows="1" cols="2" name="Note"><%=Model.Note %></textarea>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">内容:
                </td>
                <td class="adminData">
                    <textarea rows="1" cols="2" name="Body"><%=Model.Body %></textarea>
                </td>
            </tr>

            <tr>
                <td class="adminTitle">审核:
                </td>
                <td class="adminData">
                    <%HtmlRenderer.CheckBox(Model.Display, "Display"); %>
                </td>
            </tr>
            <tr>
                <td class="adminTitle">排序:
                </td>
                <td class="adminData">
                    <input type="text" class="input-number" maxlength="9" name="DisplayOrder" value="<%=Model.DisplayOrder %>" />
                </td>
            </tr>
            <tr>
                <td class="adminTitle">添加日期:
                </td>
                <td class="adminData">
                    <%HtmlRenderer.DateTimePicker(Model.CreatedDate, "CreatedDate"); %>
                </td>
            </tr>
        </table>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=NtID %>" />
            <input type="hidden" name="LanguageId" value="<%=Model.LanguageId %>" />
            <input type="hidden" name="Viewed" value="<%=Model.Viewed %>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
            <a class="a-button" href="<%BackScript(); %>">返回</a>
        </div>
    </form>
</asp:Content>
