<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>
<%
    InCustomerArea = true;
%>


<!----container---->
<uc1:top runat="server" ID="top1" />

<%Include("/html.inc/customer_top.html"); %>

<h3>我的消息</h3>
<table class="nt-table">
    <thead>
        <tr>
            <th>标题</th>
            <th>发送日期</th>
            <th>查看内容</th>
        </tr>
    </thead>
    <tbody>

        <%
            var data = DbAccessor.GetList<Customer_Message>("customerid=" + Customer.Id, "createddate desc");

            if (data.Count == 0)
            {
                Response.Write("<tr>");
                Response.Write("<td colspan=\"3\"><p class=\"no-record-found\">您暂无消息!</p></td>");
                Response.Write("</tr>");
            }
            else
            {
                foreach (var item in data)
                {
                    Response.Write("<tr>");
                    Response.Write("<td>");
                    Response.Write(item.Title);
                    Response.Write("</td>");
                    Response.Write("<td>");
                    Response.Write(item.CreatedDate);
                    Response.Write("</td>");
                    Response.Write("<td>");
                    Response.Write("<a href=\"javascript:;\" onclick=\"viewMsg(");
                    Response.Write(item.Id);
                    Response.Write(");\">查看</a>");
                    Response.Write("</td>");
                    Response.Write("</tr>");
                }
            }
                
                
        %>
    </tbody>
</table>

<script type="text/javascript">
    /*
    查看消息内容
    */
    function viewMsg(id) {
        alert('view message where id=' + id);
    }
</script>

<%Include("/html.inc/customer_bottom.html"); %>

<uc1:foot runat="server" ID="foot" />


