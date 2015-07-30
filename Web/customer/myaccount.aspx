<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>
<%
    InCustomerArea = true;
%>


<!----container---->
<uc1:top runat="server" ID="top1" />

<%Include("/html.inc/customer_top.html"); %>
<h3>基本信息</h3>
<form action="/handlers/ajaxhandler.aspx" method="post" id="accountForm" name="accountForm" invoke="saveAccountInfo">
    <div class="nt-customer-info">

        <p class="nt-customer-login-info">
            您上次本次登录的IP为:<%=Customer.LastLoginIP %>
            <br />
            您上次本次登录时间为:<%=Customer.LastLoginDate.ToString("yyyy/MM/dd hh:mm:ss") %><br />
            您这是第<%=Customer.LoginTimes %>次登录                
        </p>
        <hr class="nt-hr" />

        <div class="nt-customer-info-field">
            <label>姓名：</label><input type="text" data-old-value="<%=Customer.RealName %>" onchange="return validateField(this, 'text');" name="RealName" value="<%=Customer.RealName %>" />
            <span class="nt-validation-message"></span>
        </div>
        <div class="clear"></div>
        <div class="nt-customer-info-field">
            <label>出生日期：</label>
            <input type="text" name="BirthDay" data-old-value="<%=Customer.BirthDay.ToString("yyyy-MM-dd") %>" onchange="return validateField(this, 'date');" value="<%=Customer.BirthDay.ToString("yyyy-MM-dd") %>" />
            <span class="nt-validation-message"></span>
        </div>
        <div class="clear"></div>
        <div class="nt-customer-info-field nt-customer-info-sex">
            <label>性别：</label>
            <select name="Gender">
                <option value="1" <%=Customer.Gender?"checked=\"checked\"":"" %>>男</option>
                <option value="0" <%=Customer.Gender?"":"checked=\"checked\"" %>>女</option>
            </select>
        </div>
        <div class="clear"></div>
        <div class="nt-customer-info-field">
            <label>邮箱：</label><input type="text" name="Email" data-old-value="<%=Customer.Email %>" onchange="return validateField(this,'email');" value="<%=Customer.Email %>" />
            <span class="nt-validation-message"></span>
        </div>
        <div class="clear"></div>

        <div class="nt-customer-info-field">
            <label>固话：</label><input type="text" name="Phone" onchange="return validateField(this,'phone');" data-old-value="<%=Customer.Phone %>" value="<%=Customer.Phone %>" />
            <span class="nt-validation-message"></span>
        </div>
        <div class="clear"></div>
        <div class="nt-customer-info-field">
            <label>手机号码：</label><input type="text" name="Mobile" onchange="return validateField(this,'mobile');" data-old-value="<%=Customer.Mobile %>" value="<%=Customer.Mobile %>" />
            <span class="nt-validation-message"></span>
        </div>
        <div class="clear"></div>
        <div class="nt-customer-info-field">
            <label>住址：</label><input type="text" name="Address" value="<%=Customer.Address %>" />
            <span class="nt-validation-message"></span>
        </div>
        <div class="clear"></div>
        <div class="nt-customer-info-field">
            <label>邮政编码：</label><input type="text" name="Zip" onchange="return validateField(this,'zip');" data-old-value="<%=Customer.Zip %>" value="<%=Customer.Zip %>" />
            <span class="nt-validation-message"></span>
        </div>
        <div class="clear"></div>
    </div>

    <div class="nt-customer-save-button">
        <input type="button" class="nt-submit" onclick="saveAccount();" value="保存修改" />
    </div>

</form>


<script type="text/javascript">

    function saveAccount() {
        nt.ajaxSubmit('#accountForm', {
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

