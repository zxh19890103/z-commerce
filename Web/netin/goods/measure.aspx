<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="measure.aspx.cs" Inherits="netin_goods_measure" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">

    <script type="text/javascript">

        var measureMgr = new nt.tableMgr({
            title: '单位'
        });
        measureMgr.init();
    </script>

</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <div id="list" class="list">
        <%List(); %>
    </div>
    <div class="html-content-wrap">
        <div class="html-content" id="tmEditor" style="width: 800px;">
            <div class="html-content-top nt-drag-bar">
                <span class="html-content-title"></span><a href="javascript:;" role="close">x</a>
            </div>
            <div class="html-content-body">
                <form action="measure.aspx" method="post" id="tmForm">
                    <table class="adminContent">
                        <tr>
                            <td class="adminTitle">单位名:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="50" class="input-long" name="Name" />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">描述:
                            </td>
                            <td class="adminData">
                                <textarea name="Description" rows="1" cols="2" class="textarea-small"></textarea>
                                <span class="tips">请勿超过512个字符!</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">显示:
                            </td>
                            <td class="adminData">
                                <%HtmlRenderer.CheckBox(true, "Display"); %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">排序:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="9" class="input-number" name="DisplayOrder" value="0" />
                            </td>
                        </tr>
                    </table>
                    <input type="hidden" name="Id" value="0" />
                    <input type="hidden" name="LanguageId" value="<%=NtContext.Current.LanguageID %>" />
                </form>
            </div>
            <div class="html-content-footer">
                <a href="javascript:;" class="a-button" role="post">保存</a> <a href="javascript:;"
                    class="a-button" role="close">关闭</a>
            </div>
        </div>
    </div>


</asp:Content>

