<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>

<!----container---->
<uc1:top runat="server" ID="top1" />

<div class="content">
<div class="container">
    <div class="about_box">
        <div class="activity_content">
            <h3>活动中心<span>activity</span></h3>
            <div class="line_01"><span></span></div>
            <div class="clear" style="height: 25px;"></div>
            <ul class="activity_list">
                <%
                            
                    var data = DbAccessor.GetList<Campaign>();

                    foreach (var item in data)
                    {
                        Response.Write("<li>");
                        Response.Write("<h1>");
                        Response.Write(item.Name);
                        Response.Write("</h1>");
                        Response.Write("<h2>");
                        Response.Write(item.Subject);
                        Response.Write("</h2><div class=\"line_02\"></div><p>");
                        Response.Write(item.Body);
                        Response.Write("</p></li>");
                    }                            
                %>
            </ul>
        </div>
    </div>
</div>
    </div>
<uc1:foot runat="server" ID="foot" />
