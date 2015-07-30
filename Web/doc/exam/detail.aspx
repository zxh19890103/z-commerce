<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Import Namespace="System.Collections.Generic" %>

<%    
    
    //WebsiteInfo
    //GoodsClasses
    //ProductCategories
    //ArticleClasses
    //Pages       
    //NavigationData
    
    //ASettings,PSettings,GSettings
    
    //1:article     2:product   3:goods
    SetCatalogDataSource(3);

    /*此处演示的是网站详细页的制作
     * 以商品为例
     * */

    //获取ID
    int id = IMPOSSIBLE;
    View_Goods M = null;
    if (!int.TryParse(Request.QueryString["id"], out id))
        Goto("list.aspx", "参数错误：Id!");
    M = DbAccessor.GetById<View_Goods>(id);
    if (M == null)
        Goto("list.aspx", "没有该记录!");
    //此处的filter和orderby必须和list页面中的设置一致才能保证上一篇和下一篇的准确性！
    //查：db.List("View_Goods", "display=1", pager.PageIndex, pagesize).Execute();
    CalcSiblings(M, "display=1", "");
    
    //如果你想获取新闻分类、产品分类、商品分类，请参照以下方法
    var gClass = GoodsClasses.Find(x => x.Id == M.GoodsClassId);
    
    //for seo setting
    SeoTitle = M.SeoTitle;
    SeoKeywords = M.SeoKeywords;
    SeoDescription = M.SeoDescription;
    
    
%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>detail</title>
</head>
<body>

    <div>
        <h3>面包屑</h3>
        <a href="/">Home</a>
        <%
            //面包屑
            RenderCrumbs(2, "<a href=\"?sortid={value}\">{text}</a>", "&gt;"); %>
    </div>

    <ul>
        <li>
            <h3>Catalog</h3>
        </li>
        <%
            RenderCatalog(0,2, "<a href=\"list.aspx?sortid={value}\">{text}</a>");
        %>
    </ul>

    <ul>
        <li>
            <h3>Detail</h3>
        </li>
        <li>
            <h4><%=M.Name %></h4>
            <div>
                <%=M.FullDescription %>
            </div>
            <div>
                <a href="<%=GetDetailUrl(M.PreID) %>">
                    Previous:<%=M.PreTitle %>
                </a>
                <a href="<%=GetDetailUrl(M.NextID) %>">
                    Next:<%=M.NextTitle %>
                </a>                
            </div>
        </li>
    </ul>




</body>
</html>
