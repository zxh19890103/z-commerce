<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>
<%
    InCustomerArea = true;
%>

<uc1:top runat="server" ID="top1" />

<%Include("/html.inc/customer_top.html"); %>


<h3>添加新地址</h3>
<form action="/handlers/ajaxhandler.aspx" invoke="saveAddress" method="post" id="addressForm">
    <div class="nt-customer-info-field">
        <label>姓名：</label><input type="text" name="Name" data-old-value="<%=Model.Name %>" value="<%=Model.Name %>" /><span class="nt-validation-message"></span>
    </div>
    <div class="clear"></div>
    <div class="nt-customer-info-field">
        <label>邮箱：</label><input type="text" name="Email" data-old-value="<%=Model.Email %>" onchange="return validateField(this,'email');" value="<%=Model.Email %>" />
        <span class="nt-validation-message"></span>
    </div>
    <div class="clear"></div>

    <div class="nt-customer-info-field">
        <label>固话：</label><input type="text" name="Phone" data-old-value="<%=Model.Email %>" onchange="return validateField(this,'phone');" value="<%=Model.Phone %>" />
        <span class="nt-validation-message"></span>
    </div>
    <div class="clear"></div>
    <div class="nt-customer-info-field">
        <label>手机号码：</label><input type="text" name="Mobile" data-old-value="<%=Model.Mobile%>" onchange="return validateField(this,'mobile');" value="<%=Model.Mobile %>" />
        <span class="nt-validation-message"></span>
    </div>
    <div class="clear"></div>
    <div class="nt-customer-info-field">
        <label>住址：</label><input type="text" name="Address" data-old-value="<%=Model.Address %>" value="<%=Model.Address %>" />
        <span class="nt-validation-message"></span>
    </div>
    <div class="clear"></div>
    <div class="nt-customer-info-field">
        <label>邮政编码：</label><input type="text" name="Zip" data-old-value="<%=Model.Zip %>" onchange="return validateField(this,'zip');" value="<%=Model.Zip %>" />
        <span class="nt-validation-message"></span>
    </div>
    <div class="clear"></div>

    <div class="nt-customer-save-button">
        <input type="hidden" name="Id" value="<%=Model.Id %>" />
        <input type="button" class="nt-submit" onclick="saveAddress();" value="保存修改" />
        <input type="button" class="nt-submit" onclick="go('myaddress.aspx');" value="返回" />
    </div>
</form>

<script type="text/javascript">
    function saveAddress() {
        nt.ajaxSubmit('#addressForm', {
            ok: function (j) {
                alert(j.message, function () {
                    nt.reload();
                });
            }
        });
    }
</script>


<%Include("/html.inc/customer_bottom.html"); %>
<uc1:foot runat="server" ID="foot" />

<script runat="server">

    Customer_Consignee Model;

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        int id = 0;
        if (int.TryParse(Request.QueryString["id"], out id) && id > 0)
        {
            Model = DbAccessor.GetById<Customer_Consignee>(id);
            if (Model == null || Model.CustomerId != Customer.Id)
                Goto("myaddress.aspx", "没有发现该记录!");
        }
        else
        {
            Model = new Customer_Consignee();
        }
    }
</script>
