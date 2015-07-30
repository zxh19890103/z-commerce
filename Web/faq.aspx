<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>

<uc1:top runat="server" ID="top1" />
<div class="content">
    <div class="container">
        <div class="about_box">
            <div class="faq_content">
                <ul class="faqlist">
                    <%
                        var data = DbAccessor.GetList<Faq>("display=1", "displayorder desc");
                        foreach (var item in data)
                        {
                            Response.Write("<li>");
                            Response.Write("	<div class=\"lil question\">");
                            Response.Write(item.Question);
                            Response.Write("</div>");
                            Response.Write("	<div class=\"lil answer\">");
                            Response.Write(item.Answer);
                            Response.Write("</div>");
                            Response.Write("	<div class=\"clear\" style=\"height: 15px;\"></div>");
                            Response.Write("</li>");
                        }
                    %>
                </ul>
            </div>
        </div>
    </div>
</div>
<uc1:foot runat="server" ID="foot" />
