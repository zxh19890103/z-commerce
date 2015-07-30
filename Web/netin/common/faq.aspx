<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="faq.aspx.cs" Inherits="netin_common_faq" %>

<asp:Content ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        var faqMgr = new nt.tableMgr({ title: 'FAQ', width: 1000 });
        faqMgr.init();
    </script>


</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">

    <div class="list" id="list">
        <%List(); %>
    </div>

    <div class="html-content-wrap">
        <div class="html-content" id="tmEditor">
            <div class="html-content-top nt-drag-bar">
                <span class="html-content-title"></span><a href="javascript:;" role="close">x</a>
            </div>
            <div class="html-content-body">
                <form action="faq.aspx" method="post" id="tmForm">
                    <table class="adminContent">
                        <tr>
                            <td class="adminTitle">Type:
                            </td>
                            <td class="adminData">

                                <%
                                    HtmlRenderer.DropDownList(StaticDataProvider.Instance.FaqTypeProvider, "Type");
                                %>

                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">Question:
                            </td>
                            <td class="adminData">
                                <textarea cols="1" rows="1" class="textarea-small" name="Question"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">Answer:
                            </td>
                            <td class="adminData">
                                <textarea cols="1" rows="1" class="textarea-big" name="Answer"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">Display:
                            </td>
                            <td class="adminData">
                                <%
                                    HtmlRenderer.CheckBox(true, "Display");
                                %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">DisplayOrder:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="9" value="0" class="input-number" name="DisplayOrder" />
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
