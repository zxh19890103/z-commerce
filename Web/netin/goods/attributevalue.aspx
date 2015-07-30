<%@ Page Language="C#" AutoEventWireup="false" CodeFile="attributevalue.aspx.cs" Inherits="Netin_Goods_attributevalue" %>

<%@ Register Src="~/netin/shared/head.ascx" TagPrefix="uc1" TagName="head" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=Title %></title>
    <uc1:head runat="server" ID="head" />
    <link href="../script/colpick/css/colpick.css" rel="stylesheet" />
    <script src="../script/colpick/js/colpick.js" type="text/javascript"></script>
    <script type="text/javascript">
        var valueCount=<%=valueCount%>;
        var t=<%=Model.AttributeValueTypeId%>;
        var CT_DROPDOWNLIST = <%= Goods_Attribute_Mapping.CT_DROPDOWNLIST%>;
        var CT_RADIOBUTTONLIST = <%= Goods_Attribute_Mapping.CT_RADIOBUTTONLIST%>;
        var CT_CHECKBOXES = <%= Goods_Attribute_Mapping.CT_CHECKBOXES%>;
        var CT_TEXTBOX = <%= Goods_Attribute_Mapping.CT_TEXTBOX%>;
        var CT_MUTILINETEXTBOX = <%= Goods_Attribute_Mapping.CT_MUTILINETEXTBOX%>;
        var CT_DATEPICKER = <%= Goods_Attribute_Mapping.CT_DATEPICKER%>;
        var CT_FILEUPLOAD = <%= Goods_Attribute_Mapping.CT_FILEUPLOAD%>;
        var CT_COLORSQUARES = <%= Goods_Attribute_Mapping.CT_COLORSQUARES%>;
        //提交
        function save() {

            if(editForm.Name.value==''){
                alert('值名称不能为空!');
                return;
            }

            editForm.submit();
        }

        /*复制*/
        function copyName(){
            if(
                t===CT_COLORSQUARES
                ||t===CT_FILEUPLOAD
                ||t===CT_DATEPICKER)
                return;
            editForm.AttributeValue.value=editForm.Name.value;
        }
    </script>
</head>
<body>
    <div id="list" class="list">
        <%List(); %>
    </div>
    <div class="edit-area">
        <form action="<%=Request.Url.PathAndQuery %>" method="post" id="editForm">
            <table class="adminContent">
                <tr>
                    <td class="adminTitle">值名称:</td>
                    <td class="adminData">
                        <input type="text" class="input-long" name="Name" onchange="" maxlength="126" value="<%=Model.Name %>" />
                        <a href="javascript:;" onclick="copyName();" class="a-button">复制</a>
                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">值:</td>
                    <td class="adminData">
                        <%                            
                            switch (Model.AttributeValueTypeId)
                            {
                                case Goods_Attribute_Mapping.CT_COLORSQUARES:
                                    Response.Write("<div class=\"colpick-cube\" id=\"colpicker\" style=\"background-color:#" +
                                        Model.AttributeValue + "\"></div>");
                                    Response.Write("<input type=\"hidden\" name=\"AttributeValue\" value=\"");
                                    Response.Write(Model.AttributeValue);
                                    Response.Write("\" />");
                                    Response.Write("<script type=\"text/javascript\">");
                                    Response.Write("	$(document).ready(function () {");
                                    Response.Write("		nt.hasColpicker = true;");
                                    Response.Write("		$('#colpicker').colpick({");
                                    Response.Write("			layout: 'hex',");
                                    Response.Write("			submit: 1,");
                                    Response.Write("			onSubmit: function (hsb, hex, rgb, el, bySetColor) {");
                                    Response.Write("				editForm.AttributeValue.value = hex;");
                                    Response.Write("				colpicker.style.backgroundColor = '#' + hex;");
                                    Response.Write("				$(el).colpickHide();");
                                    Response.Write("			}");
                                    Response.Write("		});");
                                    Response.Write("	})");
                                    Response.Write("</script>");
                                    break;
                                case Goods_Attribute_Mapping.CT_CHECKBOXES:
                                case Goods_Attribute_Mapping.CT_DROPDOWNLIST:
                                case Goods_Attribute_Mapping.CT_RADIOBUTTONLIST:
                                case Goods_Attribute_Mapping.CT_TEXTBOX:
                                    Response.Write("<input type=\"text\" class=\"input-long\" name=\"AttributeValue\" value=\"");
                                    Response.Write(Model.AttributeValue);
                                    Response.Write("\" />");
                                    break;
                                case Goods_Attribute_Mapping.CT_MUTILINETEXTBOX:
                                    Response.Write(" <textarea class=\"textarea-normal\" cols=\"1\" rows=\"2\" name=\"AttributeValue\">");
                                    Response.Write(Model.AttributeValue);
                                    Response.Write("</textarea>");
                                    break;
                            }
                        %> 

                    </td>
                </tr>
                <tr>
                    <td class="adminTitle">预选:</td>
                    <td class="adminData">
                        <%HtmlRenderer.CheckBox(Model.Selected, "Selected"); %></td>
                </tr>
                <tr>
                    <td class="adminTitle">排序:</td>
                    <td class="adminData">
                        <input type="text" class="input-number" maxlength="9" name="DisplayOrder" value="<%=Model.DisplayOrder %>" /></td>
                </tr>
                <%
                    bool needAdjustment = false;
                    needAdjustment = (
                        Model.AttributeValueTypeId == Goods_Attribute_Mapping.CT_COLORSQUARES
                        || Model.AttributeValueTypeId == Goods_Attribute_Mapping.CT_RADIOBUTTONLIST
                        || Model.AttributeValueTypeId == Goods_Attribute_Mapping.CT_DROPDOWNLIST);
                    if (needAdjustment)
                    {
                        Response.Write("<tr>");
                        Response.Write("	<td class=\"adminTitle\">价格微调量:</td>");
                        Response.Write("	<td class=\"adminData\">");
                        Response.Write("		<input type=\"text\" class=\"input-decimal\" maxlength=\"20\" name=\"PriceAdjustment\" value=\"");
                        Response.Write(Model.PriceAdjustment.ToString("f"));
                        Response.Write("\" /></td>");
                        Response.Write("</tr>");
                    }
                %>
            </table>
            <div class="submit">
                <input type="hidden" name="Id" value="<%=Model.Id %>" />
                <input type="hidden" name="GoodsVariantAttributeId" value="<%=Model.GoodsVariantAttributeId %>" />
                <a class="a-button" href="javascript:;" onclick="save();">保存</a>
                <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
                <a class="a-button" href="javascript:;" onclick="window.close();">关闭窗口</a>
            </div>
        </form>
    </div>
</body>
</html>
