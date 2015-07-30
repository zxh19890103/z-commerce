<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/qb_top.ascx" TagPrefix="uc1" TagName="qb_top" %>
<%@ Register Src="~/shared/qb_foot.ascx" TagPrefix="uc1" TagName="qb_foot" %>


<%
    ChannelNo = 2;
    int cno = 0;
    int.TryParse(Request.QueryString["cno"], out cno);
    
%>

<uc1:qb_top runat="server" ID="qb_top" />

<style type="text/css">
    li.ejyimgl-item { position: relative; }
        li.ejyimgl-item img { margin-top: 60px; }
        li.ejyimgl-item a.ejyimgl-item-loc { display: block; width: 401px; position: absolute; }
    a.ejyimgl-item-loc01 { top: 250px; left: 0px; height: 250px; }
    a.ejyimgl-item-loc02 { top: 250px; left: 401px; height: 250px; }
    a.ejyimgl-item-loc03 { top: 250px; left: 0px; height: 250px; }
    a.ejyimgl-item-loc04 { top: 250px; left: 401px; height: 250px; }
    a.ejyimgl-item-loc05 { top: 0px; left: 0px; height: 120px; }
    a.ejyimgl-item-loc06 { top: 0px; left: 401px; height: 120px; }
    a.ejyimgl-item-loc07 { top: 0px; left: 0px; height: 120px; }
    a.ejyimgl-item-loc08 { top: 0px; left: 401px; height: 120px; }
</style>

<div class="tongchuanggushi">
    <div class="clear" style="height: 30px;"></div>
    <img src="images/wenzititle0<%=cno %>.png" class="wenzititle">
    <ul class="ejyimgl">

        <%
            switch (cno)
            {
                case 0:
                    Response.Write("<li class=\"ejyimgl-item\"><img src=\"images/tcgs_img0.jpg\">");
                    Response.Write("</li>");

                    Response.Write("<li class=\"ejyimgl-item\"><img src=\"images/tcgs_img1.jpg\">");
                    Response.Write("<a href=\"javascript:;\" onclick=\"ZoomIn('images/zoomcxt00.jpg')\" class=\"ejyimgl-item-loc ejyimgl-item-loc01\"></a>");
                    Response.Write("<a href=\"javascript:;\" onclick=\"ZoomIn('images/zoomcxt01.jpg')\" class=\"ejyimgl-item-loc ejyimgl-item-loc02\"></a>");
                    Response.Write("</li>");

                    Response.Write("<li class=\"ejyimgl-item\"><img src=\"images/tcgs_img2.jpg\">");
                    Response.Write("<a href=\"javascript:;\" onclick=\"ZoomIn('images/zoomcxt02.jpg')\" class=\"ejyimgl-item-loc ejyimgl-item-loc03\"></a>");
                    Response.Write("<a href=\"javascript:;\" onclick=\"ZoomIn('images/zoomcxt03.jpg')\" class=\"ejyimgl-item-loc ejyimgl-item-loc04\"></a>");
                    Response.Write("</li>");

                    Response.Write("<li class=\"ejyimgl-item\"><img src=\"images/huace01.jpg\">");
                    Response.Write("<a href=\"javascript:;\" onclick=\"ZoomIn('images/zoomcxt04.jpg')\" class=\"ejyimgl-item-loc ejyimgl-item-loc05\"></a>");
                    Response.Write("<a href=\"javascript:;\" onclick=\"ZoomIn('images/zoomcxt05.jpg')\" class=\"ejyimgl-item-loc ejyimgl-item-loc06\"></a>");
                    Response.Write("</li>");

                    Response.Write("<li class=\"ejyimgl-item\"><img  src=\"images/huace02.jpg\">");
                    Response.Write("<a href=\"javascript:;\" onclick=\"ZoomIn('images/zoomcxt06.jpg')\" class=\"ejyimgl-item-loc ejyimgl-item-loc07\"></a>");
                    Response.Write("<a href=\"javascript:;\" onclick=\"ZoomIn('images/zoomcxt07.jpg')\" class=\"ejyimgl-item-loc ejyimgl-item-loc08\"></a>");
                    Response.Write("</li>");
                    break;
                case 1:
                    break;
                case 2:
                    Response.Write("<li class=\"ejyimgl-item\"><img src=\"images/xqhc_img1.jpg\">");
                    Response.Write("<a href=\"javascript:;\" onclick=\"ZoomIn('images/xqhc_img00.jpg')\" class=\"ejyimgl-item-loc ejyimgl-item-loc01\"></a>");
                    Response.Write("<a href=\"javascript:;\" onclick=\"ZoomIn('images/xqhc_img01.jpg')\" class=\"ejyimgl-item-loc ejyimgl-item-loc02\"></a>");
                    Response.Write("</li>");
                    Response.Write("<li><img style=\"margin-top: 60px\" src=\"images/xqhc_img2.jpg\"></li>");
                    Response.Write("<li><img style=\"margin-top: 60px\" src=\"images/xqhc_img3.jpg\"></li>");
                    Response.Write("<li><img style=\"margin-top: 60px\" src=\"images/xqhc_img4.jpg\"></li>");
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    break;
            }
            
        %>
    </ul>
</div>

<script type="text/javascript">

    function ZoomIn(src) {
        var img = document.createElement('img');
        var $img = $(img);
        img.id = 'img_zoomin_' + Math.random().toPrecision(9).substr(2);
        $img.click(function () { ZoomOut(img); });
        $img.css({ display: 'none', zIndex: 1990, position: 'absolute' });
        $img.load(function () {
            //center element
            var top = 0;
            var left = 0;
            var width = $img.width();
            var height = $img.height();
            top = (document.documentElement.clientHeight - height) / 2 + document.documentElement.scrollTop + document.body.scrollTop;
            left = (document.documentElement.clientWidth - width) / 2;
            $img.css({ 'top': top, 'left': left });            
            nt.showMask();
            $img.fadeIn(800);
        });
        img.src = src;
        document.body.appendChild(img);
    }

    function ZoomOut(img) {
        $(img).fadeOut(500, null, function () {
            $(img).remove();
            nt.removeMask();
        });
    }
</script>

<uc1:qb_foot runat="server" ID="qb_foot" />
