<%@ Page Language="C#" MasterPageFile="~/netin/layout.master" AutoEventWireup="false"
    CodeFile="goods_edit.aspx.cs" Inherits="Netin_Goods_goods_edit" %>

<%@ OutputCache Duration="30" VaryByParam="id" %>

<%@ Register Src="~/Netin/shared/uploader.ascx" TagPrefix="uc1" TagName="uploader" %>
<%@ Register Src="~/netin/shared/editor.ascx" TagPrefix="uc1" TagName="editor" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script src="../script/goods.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.nt-tabs-wrap').tabs();
            $('#tags').tag();
        });

        //提交
        function save() {
            if (editForm.GoodsClassId.value == '0') {
                alert('请选择分类!');
                return false;
            }

            if (editForm.Name.value == '') {
                alert('请输入商品名!');
                return false;
            }

            if (editForm.GoodsGuid.value == '') {
                alert('请输入商品货号!');
                return false;
            }

            if (editForm.PictureUrl.value == '') {
                alert('请上传图片!');
                return false;
            }
            editor.sync();
            editForm.submit();
        }

        /*检测商品序号的唯一性*/
        function VerifyUnique() {
            nt.ajax({
                action: 'VerifyUnique',
                data: { goodsGuid: editForm.GoodsGuid.value },
                success: function (json) {
                    if (json.error) {
                        validateMsg4GoodsGuid.innerHTML = json.message;
                        editForm.GoodsGuid.focus();
                    }
                    else
                        validateMsg4GoodsGuid.innerHTML = '此货号可用';
                }
            })
        }

    </script>
    <uc1:editor runat="server" ID="editor" TextareaName="FullDescription" />
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="body">
    <form action="<%=Request.RawUrl %>" method="post" id="editForm" name="editForm">
        <div class="submit-top">
            <a class="a-button" href="javascript:;" onclick="save();">保存</a>
            <a class="a-button" href="javascript:;" onclick="editForm.reset();">重置</a>
            <%
                if (IsEdit)
                {
                    //复制商品
                    Html("<a class=\"a-button\" href=\"javascript:;\" onclick=\"copy({0},0);\">复制</a>", Model.Id);
                }
            %>
        </div>
        <div class="nt-tabs-wrap">
            <div class="nt-tabs-wrap-inner">
                <ul class="nt-tabs" id="nt_tabs">
                    <li><a href="javascript:;">基本信息</a></li>
                    <li><a href="javascript:;">图片</a></li>
                    <li><a href="javascript:;">参数</a></li>
                    <li><a href="javascript:;">规格属性</a></li>
                    <li><a href="javascript:;">营销</a></li>
                    <li><a href="javascript:;">相关商品</a></li>
                    <li><a href="javascript:;">折扣</a></li>
                    <li><a href="javascript:;">商品标签</a></li>
                    <li><a href="javascript:;">扩展分类</a></li>
                </ul>
                <div class="nt-tabs-content-wrap" id="nt_tab_content_wrap">
                    <div class="tab-item">
                        <table class="adminContent">
                            <tr>
                                <td class="adminTitle">商品分类:
                                </td>
                                <td class="adminData">
                                    <%
                                        if (Model.Id > 0)
                                        {
                                            Response.Write(Model.ClassName);
                                            HtmlRenderer.Hidden(Model.GoodsClassId, "GoodsClassId");
                                        }
                                        else
                                        {
                                            if (Categories.Count < 1)
                                                Goto("category_edit.aspx", "请先添加分类！");
                                            NtUtility.ListItemSelect(Categories, Model.GoodsClassId);
                                            HtmlRenderer.DropDownList(Categories, "GoodsClassId");
                                        }
                                    %>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">商品编号(唯一):
                                </td>
                                <td class="adminData">
                                    <input type="text" onchange="VerifyUnique();" class="input-long" maxlength="120"
                                        name="GoodsGuid" value="<%=Model.GoodsGuid %>" />
                                    <span class="tips" id="validateMsg4GoodsGuid"></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">商品名:
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-short" maxlength="120" name="Name" value="<%=Model.Name %>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">商品品牌:
                                </td>
                                <td class="adminData">
                                    <%
                                        
                                        HtmlRenderer.DropDownList(
                                            NtUtility.ListItemSelect(DB.GetDropdownlist<Brand>("Name", "Id", "display=1", "displayorder desc"), Model.BrandId)
                                            , "BrandId");
                                    %>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">简述:
                                </td>
                                <td class="adminData">
                                    <textarea cols="20" rows="5" name="ShortDescription" class="textarea-small"><%=Model.ShortDescription %></textarea>
                                    <span class="tips">请勿超过1024个字符</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">内容:
                                </td>
                                <td class="adminData">
                                    <textarea cols="20" rows="5" name="FullDescription" style="width: 800px; height: 300px;" class="textarea-big"><%=Server.HtmlEncode(Model.FullDescription) %></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">推广标题:
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-long" maxlength="512" name="SeoTitle" value="<%=Model.SeoTitle %>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">推广关键词:
                                </td>
                                <td class="adminData">
                                    <textarea cols="20" rows="5" name="SeoKeywords" class="textarea-small"><%=Model.SeoKeywords %></textarea><span class="tips">请勿超过1024个字符</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">推广描述:
                                </td>
                                <td class="adminData">
                                    <textarea cols="20" rows="5" name="SeoDescription" class="textarea-small"><%=Model.SeoDescription %></textarea><span class="tips">请勿超过1024个字符</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">属性:
                                </td>
                                <td class="adminData">
                                    <span class="checkbox-with-label">
                                        <%HtmlRenderer.CheckBox(Model.Display, "Display", "显示"); %>
                                    </span><span class="checkbox-with-label">
                                        <%HtmlRenderer.CheckBox(Model.SetTop, "SetTop", "置顶"); %>
                                    </span><span class="checkbox-with-label">
                                        <%HtmlRenderer.CheckBox(Model.IsNew, "IsNew", "新品"); %>
                                    </span><span class="checkbox-with-label">
                                        <%HtmlRenderer.CheckBox(Model.Recommended, "Recommended", "推荐"); %>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">点击次数:
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-number" maxlength="9" name="Rating" value="<%=Model.Rating %>" />
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
                            <tr>
                                <td class="adminTitle">修改日期:
                                </td>
                                <td class="adminData">
                                    <%HtmlRenderer.DateTimePicker(Model.UpdatedDate, "UpdatedDate"); %>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">图片:
                                </td>
                                <td class="adminData">
                                    <%
                                        uploader.FieldValue = Model.PictureUrl;
                                    %>
                                    <uc1:uploader runat="server" ID="uploader" FieldName="PictureUrl" PostUrl="/Netin/handlers/uploadHandler.aspx" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!--图片-->
                    <div class="tab-item">
                        <div id="list_picture">
                            <%
                                RenderPictures();
                            %>
                        </div>
                    </div>
                    <!--参数-->
                    <div class="tab-item">
                        <div id="list_parameter">
                            <%
                                RenderParams();
                            %>
                        </div>
                    </div>
                    <!--属性-->
                    <div class="tab-item">
                        <div id="list_attribute">
                            <%RenderAttribute();%>
                        </div>
                    </div>
                    <!--营销-->
                    <div class="tab-item">
                        <table class="adminContent">
                            <tr>
                                <td class="adminTitle">成本:
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-decimal" maxlength="20" name="Cost" value="<%=Model.Cost %>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">市场价:
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-decimal" maxlength="20" name="MarketPrice" value="<%=Model.MarketPrice %>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">原价格:
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-decimal" maxlength="20" name="OldPrice" value="<%=Model.OldPrice %>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">价格:
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-decimal" maxlength="20" name="Price" value="<%=Model.Price %>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">启用会员价:
                                </td>
                                <td class="adminData">
                                    <%HtmlRenderer.CheckBox(Model.EnableVipPrice, "EnableVipPrice"); %>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">会员价:
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-decimal" maxlength="20" name="VipPrice" value="<%=Model.VipPrice %>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">启用特价:
                                </td>
                                <td class="adminData">
                                    <%HtmlRenderer.CheckBox(Model.EnableSpecialPrice, "EnableSpecialPrice"); %>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">特价:
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-decimal" maxlength="20" name="SpecialPrice" value="<%=Model.SpecialPrice %>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">特价开始日期:
                                </td>
                                <td class="adminData">
                                    <%HtmlRenderer.DateTimePicker(Model.SpecialPriceStartDate, "SpecialPriceStartDate"); %>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">特价结束日期:
                                </td>
                                <td class="adminData">
                                    <%HtmlRenderer.DateTimePicker(Model.SpecialPriceEndDate, "SpecialPriceEndDate"); %>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">储存量:
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-number" maxlength="9" name="StockQuantity" value="<%=Model.StockQuantity%>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">积分值:
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-number" maxlength="9" name="Points" value="<%=Model.Points%>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">重量(g):
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-decimal" maxlength="20" name="Weight" value="<%=Model.Weight%>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">长度(cm):
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-decimal" maxlength="20" name="Length" value="<%=Model.Length%>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">高度(cm):
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-decimal" maxlength="20" name="Height" value="<%=Model.Height%>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">宽度(cm):
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-decimal" maxlength="20" name="Width" value="<%=Model.Width%>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">页数:
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-number" maxlength="9" name="PageNum" value="<%=Model.PageNum%>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">商品度量单位(如：个、箱等):
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-short" maxlength="20" name="Measure" value="<%=Model.Measure%>" />
                                    &nbsp;&nbsp;
                                    <%                                                                                                                var measureSelections = DB.GetDropdownlist<Measure>("Name", "Name", "Display=1", "DisplayOrder desc");
                                                                                                                                                      measureSelections.Insert(0, new NtListItem("请选择单位", ""));
                                                                                                                                                      HtmlRenderer.DropDownList(measureSelections, "MeasureSelections", "editForm.Measure.value=this.value;", 120);
                                    %>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">购买量:
                                </td>
                                <td class="adminData">
                                    <input type="text" class="input-number" maxlength="9" name="SellNumber" value="<%=Model.SellNumber%>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">禁止购买:
                                </td>
                                <td class="adminData">
                                    <%HtmlRenderer.CheckBox(Model.DisableBuyButton, "DisableBuyButton"); %>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">禁止收藏:
                                </td>
                                <td class="adminData">
                                    <%HtmlRenderer.CheckBox(Model.DisableWishlistButton, "DisableWishlistButton"); %>
                                </td>
                            </tr>
                            <tr>
                                <td class="adminTitle">下架:
                                </td>
                                <td class="adminData">
                                    <%HtmlRenderer.CheckBox(Model.Deleted, "Deleted"); %>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!--相关产品-->
                    <div class="tab-item">
                        <div id="goodsBinding">
                            <%RenderBindingGoods(); %>
                        </div>
                    </div>
                    <!--折扣-->
                    <div class="tab-item">
                        <%RenderDiscount(); %>
                    </div>
                    <!--标签-->
                    <div class="tab-item">
                        <div id="tags">
                            <%RenderTags(); %>
                        </div>
                    </div>
                    <!--扩展分类-->
                    <div class="tab-item">
                        <div id="otherClass">
                            <%RenderOtherClass(); %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="submit">
            <input type="hidden" name="Id" value="<%=Model.Id %>" />
            <input type="hidden" name="LanguageId" value="<%=Model.LanguageId %>" />
            <a class="a-button" href="javascript:;" onclick="save();">保存</a> <a class="a-button"
                href="javascript:;" onclick="editForm.reset();">重置</a> <a class="a-button" href="<%BackScript(); %>">返回</a>
        </div>
    </form>
    <!-----------------------------------------编辑窗Html------------------------------------------->
    <div class="html-content-wrap">
        <div id="edit_parameter" class="html-content" style="width: 400px; height: auto;">
            <div class="html-content-top nt-drag-bar">
                <span class="html-content-title">参数编辑/添加</span> <a href="javascript:;" onclick="param_cancel();">x</a>
            </div>
            <div class="html-content-body">
                <form id="paramEditForm" action="goods_edit.aspx" method="post">
                    <table class="adminContent">
                        <tr>
                            <td class="adminTitle">参数组：
                            </td>
                            <td class="adminData">
                                <%
                                    var list = DB.GetDropdownlist<Goods_ParameterGroup>("GroupName", "Id", "Display=1");
                                    HtmlRenderer.DropDownList(list, "GoodsParameterGroupId");
                                %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">参数名：
                            </td>
                            <td class="adminData">
                                <input type="text" class="input-short" maxlength="256" name="Name" />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">参数值：
                            </td>
                            <td class="adminData">
                                <textarea name="Value" rows="2" cols="1"></textarea>
                            </td>
                        </tr>
                    </table>
                    <input type="hidden" name="Id" />
                    <input type="hidden" name="GoodsId" value="<%=Model.Id%>" />
                </form>
            </div>
            <div class="html-content-footer">
                <a class="a-button" href="javascript:;" onclick="param_save();">保存</a> <a class="a-button"
                    href="javascript:;" onclick="paramEditForm.reset();">重置</a> <a class="a-button" href="javascript:;"
                        onclick="param_cancel();">取消</a>
            </div>
        </div>
        <div id="edit_attribute" class="html-content" style="width: 600px; height: auto;">
            <div class="html-content-top nt-drag-bar">
                <span class="html-content-title">属性编辑/添加</span> <a href="javascript:;" onclick="attribute_cancel();">x</a>
            </div>
            <div class="html-content-body">
                <form action="goods_edit.aspx" method="post" id="attributeEditForm">
                    <table class="adminContent">
                        <tr>
                            <td class="adminTitle">属性:
                            </td>
                            <td class="adminData">
                                <%
                                    var attribute_selection = DB.GetDropdownlist<GoodsAttribute>("Name", "Id", "");
                                    HtmlRenderer.DropDownList(attribute_selection, "GoodsAttributeId");
                                %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">属性类别:
                            </td>
                            <td class="adminData">
                                <%
                                    HtmlRenderer.DropDownList(StaticDataProvider.Instance.SpecificationTypeProvider,
                                       "ControlType");
                                %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">排序:
                            </td>
                            <td class="adminData">
                                <input type="text" class="input-number" maxlength="9" name="DisplayOrder" value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">是否必需:
                            </td>
                            <td class="adminData">
                                <%HtmlRenderer.CheckBox(true, "IsRequired"); %>
                            </td>
                        </tr>
                        <tr>
                            <td class="adminTitle">文字说明:
                            </td>
                            <td class="adminData">
                                <input type="text" class="input-long" name="TextPrompt" maxlength="1024" value="" />
                            </td>
                        </tr>
                    </table>
                    <input type="hidden" name="Id" />
                    <input type="hidden" name="GoodsId" value="<%=Model.Id %>" />
                </form>
            </div>
            <div class="html-content-footer">
                <a class="a-button" href="javascript:;" onclick="attribute_save();">保存</a> <a class="a-button"
                    href="javascript:;" onclick="attributeEditForm.reset();">重置</a> <a class="a-button"
                        href="javascript:;" onclick="attribute_cancel();">取消</a>
            </div>
        </div>
    </div>
</asp:Content>
