<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>


<%
    
    //此处演示的是网站首页的制作方法

    //需要获取的数据库内容：banner、幻灯片、新闻、产品
    //ConstStrings.COMMON_ORDER_BY="settop desc,recommended desc,diplayorder desc,createddate desc,id desc";
    DbAccessor db = new DbAccessor();
    db
        .List("Banner", "display=1")
        .List("Slider", "display=1")
        .Top("Article", 5, "display=1", ConstStrings.COMMON_ORDER_BY)
        .Top("Product", 10, "display=1 and productcategoryid=1")
        .List("FriendLink")
        .Execute();
        
%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>home</title>
    <script src="../../js/jquery.min.js"></script>
</head>
<body>


    <%
        Response.Write("<h3>输出导航</h3>");
        //输出导航
        RenderMenu(2);

        Response.Write("<h3>输出侧边导航</h3>");
        //输出侧边导航
        RenderLeftMenu(3, 1);

        Response.Write("<h3>输出商品分类信息</h3>");
        //输出商品分类信息
        SetCatalogDataSource(2);
        RenderCatalog(0, 1, "<a href=\"news.aspx?sid={value}\">{text}</a>");
        
        
        
        
    %>


    <a href="javascript:;" onclick="$.get('/handlers/ajax.aspx',{action:'Ajax'},function(j){alert(j.message);},'json');">Button</a>
    
    <ul>
        <li>
            <h3>Banner</h3>
        </li>
        <%
            foreach (DataRow r in db[0].Rows)
            {
                Response.Write("<li>");
                Response.Write(r["id"]);
                Response.Write("&nbsp;&nbsp;");
                Response.Write(r["title"]);
                Response.Write("</li>");
            }
        %>

        <li>
            <h3>Slider</h3>
        </li>
        <%
            foreach (DataRow r in db[1].Rows)
            {
                Response.Write("<li>");
                Response.Write(r["id"]);
                Response.Write("&nbsp;&nbsp;");
                Response.Write(r["title"]);
                Response.Write("</li>");
            }
        %>

        <li>
            <h3>FriendLink</h3>
        </li>
        <%
            foreach (DataRow r in db[4].Rows)
            {
                Response.Write("<li>");
                Response.Write(r["id"]);
                Response.Write("&nbsp;&nbsp;");
                Response.Write(r["title"]);
                Response.Write("</li>");
            }
        %>

        <li>
            <h3>Article</h3>
        </li>
        <%
            foreach (DataRow r in db[2].Rows)
            {
                Response.Write("<li>");
                Response.Write(r["id"]);
                Response.Write("&nbsp;&nbsp;");
                Response.Write(r["title"]);
                Response.Write("</li>");
            }
        %>

        <li>
            <h3>Product</h3>
        </li>
        <%
            foreach (DataRow r in db[3].Rows)
            {
                Response.Write("<li>");
                Response.Write(r["id"]);
                Response.Write("&nbsp;&nbsp;");
                Response.Write(r["name"]);
                Response.Write("</li>");
            }
        %>
    </ul>
</body>
</html>
