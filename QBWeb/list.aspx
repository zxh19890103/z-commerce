<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Register Src="~/shared/qb_top.ascx" TagPrefix="uc1" TagName="qb_top" %>
<%@ Register Src="~/shared/qb_foot.ascx" TagPrefix="uc1" TagName="qb_foot" %>

<%
    ChannelNo = 3;
    int cno = 0;
    int.TryParse(Request.QueryString["cno"], out cno);

    int sid = 0;
    int.TryParse(Request.QueryString["sid"], out sid);
    if (sid == 0) sid = 1;
    string orderby = "settop desc,recommended desc,displayorder desc,createddate desc";
    string where = "display=1 and deleted=0 and goodsClassId=" + sid;
    //View_GoodsClass gc = DbAccessor.GetById<View_GoodsClass>(sid);
    //if (gc == null)
    //    Goto("没有发现该类别记录!");
    NtPager pager = new NtPager();
    var listD = DbAccessor.GetList<View_Goods>(where, orderby, pager.PageIndex, pager.PageSize);
    if (listD.Count == 0)
        Goto("index.aspx", "没有内容!");
    pager.RecordCount = DbAccessor.Total;
%>

<uc1:qb_top runat="server" ID="qb_top" />

<div class="tongchuanggushi">
    <div class="clear" style="height: 30px;"></div>
    <img src="images/wenzititle0<%=cno %>-<%=sid %>.png" class="wenzititle">
    <div class="class-name" style="font-family: ‘微软雅黑’; font-size: 0.9em; font-weight: 800; color: rgb(136, 136, 136); margin-top: 20px;">
        <%
            switch (sid)
            {
                case 7:
                case 3:
                    Response.Write("韩国进口特种纸，日本专业柯达相纸");
                    break;
                case 5:
                    Response.Write("传统文化");
                    break;
                case 6:
                    Response.Write("校园文化");
                    break;
                default:
                    break;
            }
        %>
    </div>
    <div class="clear" style="height: 40px;"></div>
    <div id="nt-grid">
        <ul class="nt-grid-wrap" id="ntGridWrap">
            <%
                foreach (var item in listD)
                {
                    Response.Write("<li class=\"nt-grid-item\">");
                    Response.Write("<a href=\"detail.aspx?id=");
                    Response.Write(item.Id);
                    Response.Write("\"><img src=\"");
                    Response.Write(item.PictureUrl);
                    Response.Write("\"/>");
                    Response.Write("<p><b>");
                    Response.Write(NtUtility.GetSubString(item.Name, 20));
                    Response.Write("</b>");
                    if (cno == 1)
                    {
                        Response.Write("<br/>规格:");
                        Response.Write(item.Width.ToString("f"));
                        Response.Write("x");
                        Response.Write(item.Height.ToString("f"));
                    }
                    /*if(cno==3){
                        Response.Write("<br/>");
                        Response.Write(NtUtility.GetSubString(item.ClassName, 20));
                    }*/
                    Response.Write("<br/>价格:");
                    Response.Write(item.Price.ToString("f"));
                    Response.Write("</p></a>");
                    Response.Write("</li>");
                }
            %>
        </ul>
    </div>

    <div class="list_yema">
        <%
            Response.Write("<a class=\"ym_prev\"");
            if (pager.PrePageNo == 0)
            {
                Response.Write(" href=\"javascript:;\"");
            }
            else
            {
                Response.Write(" href=\"?cno=");
                Response.Write(cno);
                Response.Write("&sid=");
                Response.Write(sid);
                Response.Write("&page=");
                Response.Write(pager.PrePageNo);
                Response.Write("\"");
            }
            Response.Write("></a>");
            Response.Write("<ul class=\"ym_ul\">");
            foreach (var item in pager.Pager)
            {
                Response.Write("<li><a");
                if (item.Selected)
                {
                    Response.Write(" class=\"active\"");
                    Response.Write(" href=\"javascript:;\"");
                }
                else
                {
                    Response.Write(" href=\"?cno=");
                    Response.Write(cno);
                    Response.Write("&sid=");
                    Response.Write(sid);
                    Response.Write("&page=");
                    Response.Write(item.Value);
                    Response.Write("\"");
                }
                Response.Write(">");
                Response.Write(item.Text);
                Response.Write("</a></li>");
            }

            Response.Write("</ul>");
            Response.Write("<a class=\"ym_next\"");
            if (pager.NextPageNo == 0)
            {
                Response.Write(" href=\"javascript:;\"");
            }
            else
            {
                Response.Write(" href=\"?cno=");
                Response.Write(cno);
                Response.Write("&sid=");
                Response.Write(sid);
                Response.Write("&page=");
                Response.Write(pager.NextPageNo);
                Response.Write("\"");
            }
            Response.Write("></a>");
        %>
    </div>

</div>

<script type="text/javascript">

    function reGrid() {
        //定宽、定高，但是不定每列格子的个数
        var cols = 10, margin = 10, total = 0;
        var gridWrap = $('#ntGridWrap');
        var totalWidth = gridWrap.width();
        var width = $('li.nt-grid-item', gridWrap).width();
        var width2 = width + 2 * margin;
        var x = 0, y = 0;
        var xPaddingLeft = 0;
        cols = parseInt((totalWidth / width2).toFixed(0));
        xPaddingLeft = (totalWidth - cols * width2) / 2;
        total = $('li.nt-grid-item', gridWrap).length;
        var j = 0, l = 0;
        var i = 0;
        var totalHeight = 150;
        var maxHeightPerLayer = 0;

        function moveNext() {
            if (j == cols) {
                j = 0;
                l++;

            }
            if (l == 0) {
                y = 0;

            } else {
                var upperGridItem = $('li.nt-grid-item:eq(' + (i - cols) + ')', gridWrap);
                var itemH = upperGridItem.height() + 2 * margin;
                y = itemH + upperGridItem.position().top;
            }

            x = j * width2;
            j++;
            i++;
            return { x: x + xPaddingLeft, y: y };
        }

        function dg() {
            if (i == total) {
                return;
            }
            var n = $('li.nt-grid-item:eq(' + i + ')', gridWrap);
            var p = moveNext();
            n.animate({ top: p.y, left: p.x }, 100, 'easeInBack', function () { dg(); });
        }

        dg();

        $('li.nt-grid-item', gridWrap).each(function (i, n) {
            var h = $(n).height() + 2 * margin;
            if (maxHeightPerLayer < h) maxHeightPerLayer = h;
            if (i % cols == 0) {
                totalHeight += maxHeightPerLayer;
                maxHeightPerLayer = 0;
            }
        });

        gridWrap.height(totalHeight);

    }

    reGrid();

    $(window).resize(function () {
        reGrid();
    });

</script>

<uc1:qb_foot runat="server" ID="qb_foot" />
