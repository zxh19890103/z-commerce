<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/qb_top.ascx" TagPrefix="uc1" TagName="qb_top" %>
<%@ Register Src="~/shared/qb_foot.ascx" TagPrefix="uc1" TagName="qb_foot" %>


<%
    ChannelNo = 1;

    int cno = 0;
    int.TryParse(Request.QueryString["cno"], out cno);
    
%>

<uc1:qb_top runat="server" ID="qb_top" />

<div class="tongchuanggushi">
    <div class="clear" style="height: 30px;"></div>
    <img src="images/wenzititle0<%=cno %>.png" class="wenzititle" />
    <div class="clear" style="height: 40px;"></div>
    <div class="czyj">
        <%
            switch (cno)
            {
                case 0:
                    Response.Write("<img src=\"images/tcgs_index.jpg\"/>");
                    Response.Write("<a href=\"two.aspx?cno=0#a1\" style=\"display:block; width:191px; height:287px; position:absolute; top:10px; left:25px;\"></a>");
                    Response.Write("<a href=\"two.aspx?cno=0#a2\" style=\"display:block; width:191px; height:287px; position:absolute; top:10px; left:250px;\"></a>");
                    Response.Write("<a href=\"two.aspx?cno=0#a3\" style=\"display:block; width:191px; height:287px; position:absolute; top:10px; left:450px;\"></a>");
                    break;
                case 1:
                    Response.Write("<a href=\"list.aspx?cno=1&sid=7\" class=\"btn1 botton\"><img src=\"/images/czyjtext.png\"></a>");
                    Response.Write("<a href=\"list.aspx?cno=1&sid=3\" class=\"btn2 botton\"><img src=\"/images/grzptext.png\"></a>");
                    break;
                case 2:
                    Response.Write("<a href=\"list.aspx?cno=2&sid=9\"><img src=\"images/xqhc_index.jpg\"/></a>");
                    break;
                case 3:
                    Response.Write("<a href=\"list.aspx?cno=3&sid=5\" class=\"btn1 botton\"><img src=\"/images/ctwhtext.png\"></a>");
                    Response.Write("<a href=\"list.aspx?cno=3&sid=6\" class=\"btn2 botton\"><img src=\"/images/xywhtext.png\"></a>");
                    break;
                case 4:
                    Response.Write("<a href=\"list.aspx?cno=4&sid=15\" class=\"btn3 botton\"><img src=\"/images/zpxstext.png\"></a>");
                    Response.Write("<a href=\"list.aspx?cno=4&sid=16\" class=\"btn4 botton\"><img src=\"/images/ayxstext.png\"></a>");
                    Response.Write("<a href=\"list.aspx?cno=4&sid=17\" class=\"btn5 botton\"><img src=\"/images/cyxstext.png\"></a>");
                    break;
                default: break;
            }
            
        %>
    </div>
</div>

<uc1:qb_foot runat="server" ID="qb_foot" />
