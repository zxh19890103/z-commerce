<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>
<%
    InCustomerArea = true;
%>

<!----container---->
<uc1:top runat="server" ID="top1" />

<%Include("/html.inc/customer_top.html"); %>
<h3>我的地址薄</h3>
<table class="nt-table">
    <thead>
        <tr>
            <th>姓名</th>
            <th>手机</th>
            <th>固话</th>
            <th>邮箱</th>
            <th>地址</th>
            <th>邮编</th>
            <th>操作</th>
        </tr>
    </thead>
    <tbody>
        <%
            var data = DbAccessor.GetList<Customer_Consignee>("customerid=" + Customer.Id);

            if (data.Count == 0)
            {
                Response.Write("<tr>");
                Response.Write("<td colspan=\"7\"><p class=\"no-record-found\">您暂无地址记录，<a href=\"address_edit.aspx\">添加</a>!</p></td>");
                Response.Write("</tr>");
            }
            else
            {

                foreach (var item in data)
                {
                    Response.Write("<tr>");
                    Response.Write("<td>");
                    Response.Write(item.Name);
                    Response.Write("</td>");
                    Response.Write("<td>");
                    Response.Write(item.Mobile);
                    Response.Write("</td>");
                    Response.Write("<td>");
                    Response.Write(item.Phone);
                    Response.Write("</td>");
                    Response.Write("<td>");
                    Response.Write(item.Email);
                    Response.Write("</td>");
                    Response.Write("<td>");
                    Response.Write(item.Address);
                    Response.Write("</td>");
                    Response.Write("<td>");
                    Response.Write(item.Zip);
                    Response.Write("</td>");
                    Response.Write("<td class=\"edit-cell\">");
                    Response.Write("<a href=\"address_edit.aspx?id=");
                    Response.Write(item.Id);
                    Response.Write("\">修改</a><a href=\"javascript:;\" onclick=\"delMyAddress(");
                    Response.Write(item.Id);
                    Response.Write(");\">删除</a>");
                    Response.Write("</td>");
                    Response.Write("</tr>");
                }

                Response.Write("<tr>");
                Response.Write("<td colspan=\"6\"></td>");
                Response.Write("<td><a href=\"address_edit.aspx\">添加</a></td>");
                Response.Write("</tr>");
            }
        %>
    </tbody>
</table>

<script type="text/javascript">
    function delMyAddress(id) {
        confirm('删除后将无法恢复! 您确定删除该条记录?', function () {
            nt.ajax({
                action: 'delMyAddress',
                data: { id: id },
                success: function (j) {
                    if (j.error) error(j.message);
                    else {
                        nt.reload();
                    }
                }
            });
        });
    }
</script>

<%Include("/html.inc/customer_bottom.html"); %>

<uc1:foot runat="server" ID="foot" />
