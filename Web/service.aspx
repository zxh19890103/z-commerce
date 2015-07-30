<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>


<uc1:top runat="server" ID="top1" />
<div class="content">
    <div class="container">
        <div class="about_box">
            <div class="about_content">
                <div class="show_img">
                    <img src="/content/img/service.jpg" alt="" />
                </div>
                <h2>服务中心<span>service</span></h2>
                <div class="line_01"><span></span></div>
                <div class="content_text">
                    <ul>

                        <% 
                            int total = DbAccessor.GetRecordCount("View_Article");
                            var pager = new NtPager(total, 12);
                            var data = DbAccessor.GetList<View_Article>("settop desc,recommended desc,createddate desc", pager.PageIndex, pager.PageSize);
                            foreach (var item in data)
                            {
                                Response.Write(" <li>");
                                Response.Write("<a href=\"servicedetail.aspx?id=");
                                Response.Write(item.Id);
                                Response.Write("\">");
                                Response.Write(item.Title);
                                Response.Write("<span>");
                                Response.Write(item.CreatedDate.ToString("yyyy-MM-dd"));
                                Response.Write("</span></a>");
                                Response.Write("</li>");
                            }
                        %>
                    </ul>
                    <div class="control_box">
                        <a href="?page=<%=pager.FirstPageNo %>">首页</a>
                        <a href="?page=<%=pager.PrePageNo %>">上一页</a>
                        <ul class="yema">
                            <%
                                foreach (var item in pager.Pager)
                                {
                                    Response.Write("<li><a href=\"?page=");
                                    Response.Write(item.Value);
                                    Response.Write("\">");
                                    Response.Write(item.Text);
                                    Response.Write("</a></li>");
                                }
                            %>
                        </ul>
                        <a href="?page=<%=pager.NextPageNo %>">下一页</a>
                        <a href="?page=<%=pager.EndPageNo %>">末页</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<uc1:foot runat="server" ID="foot" />

