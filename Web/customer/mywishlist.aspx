<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>
<%
    InCustomerArea = true;
%>

<!----container---->
<uc1:top runat="server" ID="top1" />

<%Include("/html.inc/customer_top.html"); %>


<h3>我的收藏夹</h3>
<table class="nt-table">
    <thead>
        <tr>
            <th>图片</th>
            <th>商品名</th>
            <th>操作</th>
        </tr>
    </thead>
    <tbody>
        <%
            System.Data.SqlClient.SqlDataReader rs = SqlHelper.ExecuteReader("select id,(select name from nt_goods where id=nt_customer_wishlist.goodsid) as name,(select pictureurl from nt_goods where id=nt_customer_wishlist.goodsid) as pictureurl from nt_customer_wishlist where customerid=" + Customer.Id);

            if (rs.HasRows)
            {

                while (rs.Read())
                {
                    Response.Write("<tr>");
                    Response.Write("<td><img width=\"80\" height=\"80\" src=\"");
                    Response.Write(_mediaService.GetThumbnailUrl(rs[2].ToString(), 80, 80));
                    Response.Write("\"/></td>");
                    Response.Write("<td><a href=\"/detail.aspx?id=");
                    Response.Write(rs[0]);
                    Response.Write("\">");
                    Response.Write(rs[1]);
                    Response.Write("</a></td>");
                    Response.Write("<td>");
                    Response.Write("<a href=\"javascript:;\" onclick=\"delWishlistItem(");
                    Response.Write(rs[0]);
                    Response.Write(");\">删除</a>");
                    Response.Write("</td>");
                    Response.Write("</tr>");
                }
            }
            else
            {
                Response.Write("<tr>");
                Response.Write("<td colspan=\"3\"><p class=\"no-record-found\">您暂无收藏!</p></td>");
                Response.Write("</tr>");
            }
        %>
    </tbody>
</table>

<script type="text/javascript">
    /*删除商品收藏*/
    function delWishlistItem(id) {
        confirm('删除后将无法恢复! 您确定删除该条记录?', function () {
            nt.ajax({
                action: 'delWishlistItem',
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

