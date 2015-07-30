<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="day.aspx.cs" Inherits="netin_product_day" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <script type="text/javascript">

        var dayMgr = new nt.tableMgr({title:'日期'});
        dayMgr.init();

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <div id="list" class="list">
        <%List(); %>
    </div>
    
    <div class="html-content-wrap">
        <div class="html-content" id="tmEditor" style="width:600px;">
            <div class="html-content-top nt-drag-bar">
                <span class="html-content-title"></span> <a href="javascript:;" role="close">x</a>
            </div>
            <div class="html-content-body">
                <form action="day.aspx" method="post" id="tmForm">
                    <table class="adminContent">
                        <tr>
                            <td class="adminTitle">标题:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="50" class="input-long" name="Title" />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">英文标题:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="120" class="input-long" name="EnglishTitle" />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">描述:
                            </td>
                            <td class="adminData">
                                <textarea name="Description" rows="1" cols="2" class="textarea-small"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">排序:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="9" class="input-number" name="DisplayOrder"  value="0"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">日期:
                            </td>
                            <td class="adminData">
                                <%HtmlRenderer.DateTimePicker(DateTime.Now, "Date"); %>
                            </td>
                        </tr>
                    </table>
                    <input type="hidden" name="Id" value="0" />
                </form>
            </div>
            <div class="html-content-footer">
                <a href="javascript:;" class="a-button" role="post">保存</a> <a href="javascript:;"
                    class="a-button" role="close">关闭</a>
            </div>
        </div>
    </div>

</asp:Content>
