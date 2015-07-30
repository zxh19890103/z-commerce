<%@ Page Title="" Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false" CodeFile="area.aspx.cs" Inherits="netin_product_area" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">

        var areaMgr = new nt.tableMgr({ title: '日期' });
        areaMgr.init();

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">

    <div id="list" class="list">
        <%List(); %>
    </div>
    
    <div class="html-content-wrap">
        <div class="html-content" id="tmEditor" style="width:600px;">
            <div class="html-content-top nt-drag-bar">
                <span class="html-content-title"></span> <a href="javascript:;" role="close">x</a>
            </div>
            <div class="html-content-body">
                <form action="area.aspx" method="post" id="tmForm">
                    <table class="adminContent">
                        <tr>
                            <td class="adminTitle">地区名:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="50" class="input-long" name="Name" />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">英文地区名:
                            </td>
                            <td class="adminData">
                                <input type="text" maxlength="120" class="input-long" name="EnglishName" />
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

