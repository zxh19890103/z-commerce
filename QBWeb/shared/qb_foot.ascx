<%@ Control Language="C#" ClassName="qb_foot" Inherits="Nt.Framework.BaseAscx" %>

<div class="clear" style="height: 19px;"></div>

<%
    bool isHome = false;
    if (PageBasedOn != null)
        isHome = PageBasedOn.ChannelNo == 0;
    if (isHome)
    {
        Response.Write("<div class=\"footbox clear\">");
        Response.Write("    <p class=\"p2\">Copyright &copy; 同窗故事艺术作品馆    京ICP备08010314号-19</p>");
        Response.Write("</div>");
    }
    else
    {
        Response.Write("<div class=\"footbox f2\">");
        Response.Write("    <p class=\"p3\">www.qiubaishop.com</p>");
        Response.Write("    <p class=\"p4\">Copyright &copy;同窗故事艺术作品馆    京ICP备08010314号-19</p>");
        Response.Write("</div>");
    }
    
%>

</div>
    </div>
	
	
	
	<!--QQ 在线客服-->

<script type="text/javascript">
    lastScrollY = 0;

    function heartBeat() {
        var diffY;
        if (document.documentElement && document.documentElement.scrollTop)
            diffY = document.documentElement.scrollTop;
        else if (document.body)
            diffY = document.body.scrollTop
        else
        {/*Netscape stuff*/ }

        percent = .1 * (diffY - lastScrollY);
        if (percent > 0) percent = Math.ceil(percent);
        else percent = Math.floor(percent);
        document.getElementById("lovexin12").style.top = parseInt

        (document.getElementById("lovexin12").style.top) + percent + "px";

        lastScrollY = lastScrollY + percent;
        //alert(lastScrollY);
    }

    suspendcode12 = "<DIV id='lovexin12' style='Right:-104px;POSITION:fixed;TOP:380px;z-index:1000000'>";
    var recontent = "<img src='/images/QQ.png'   border='0' usemap='#Map_qq' /><map name='Map_qq' id='Map_qq'><area shape='rect' coords='12,50,180,100' href='tencent://message/?uin=2737067097&Site=在线客服&Menu=yes' /><area shape='rect' coords='22,150,150,100' href='tencent://message/?uin=27370670971111&Site=在线客服&Menu=yes' /></map><a href='javascript:;' onclick='far_close();' style='display:block;left:0px;top:0px;position:absolute;width:25px;height:35px;'></a>";

    document.write(suspendcode12);
    document.write(recontent);
    document.write("</div>");
    //window.setInterval("heartBeat()", 1);

    function far_close() {
        var tr = document.getElementById("lovexin12");
        if (tr.open) {
            tr.style.right = '-104px';
            tr.open = false;
        } else {
            tr.style.right = '0px';
            tr.open = true;
        }
    }

    function setfrme() {
        var tr = document.getElementById("lovexin12");
        var twidth = tr.clientWidth;
        var theight = tr.clientHeight;
        var fr = document.getElementById("frame55la");
        fr.width = twidth - 1;
        fr.height = theight - 30;
    }

    var xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx=window.setInterval(function () {
        if (document.getElementById('bdshare'))
        {
            var qqBox = document.getElementById('lovexin12');
            qqBox.style.top = (parseInt(document.getElementById('bdshare').style.top) + 150) + 'px';
            window.clearInterval(xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx);
        }
    }, 100);
    
</script>

<!-- Baidu Button BEGIN -->
<script type="text/javascript" id="bdshare_js" data="type=slide&img=3&pos=right&uid=6478904"></script>
<script type="text/javascript" id="bdshell_js"></script>
<script type="text/javascript">
    document.getElementById("bdshell_js").src = "http://bdimg.share.baidu.com/static/js/shell_v2.js?cdnversion=" + Math.ceil(new Date() / 3600000);    
</script>
<!-- Baidu Button END -->
</body>
</html>
