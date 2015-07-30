<%@ Page Language="C#" Inherits="Nt.Framework.BaseAspx" %>

<%@ Import Namespace="System.Collections.Generic" %>

<%    
    
    //1:article     2:product   3:goods
    SetCatalogDataSource(3);

    /*此处演示的是网站列表页的制作
     * 以商品列表为例
     * 分页
     * 每页显示条数：2
     * */
    DbAccessor db = new DbAccessor();
    int pagesize = 2;
    NtPager pager = new NtPager();//计算页码
    pager.PageSize = pagesize;
    db.List("View_Goods", "display=1 and languageid=" + NtConfig.CurrentLanguage, pager.PageIndex, pagesize).Execute();
    pager.RecordCount = db.GetTotal(0);//总数
    var list = DbAccessor.CreateListByDataTable<View_Goods>(db[0]);    //转换为强类型实例列表
%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>list</title>
</head>
<body>

    <div>
        <a href="/">Home</a>
        <%
            //面包屑
            RenderCrumbs(4, "<a href=\"?sortid={value}\">{text}</a>", "&gt;"); %>
    </div>

    <ul>
        <li>
            <h3>list</h3>
        </li>
        <%
            foreach (var i in list)
            {
                Response.Write("<li>");
                Response.Write("<h5>");
                Response.Write(i.Id);
                Response.Write("</h5>");
                Response.Write("<a href=\"");
                Response.Write(GetDetailUrl(i.Id, "detail.aspx"));//获取详细页URL
                Response.Write("\">");
                Response.Write(i.Name);
                Response.Write("</a>");
                if (GSettings.EnableThumbOnList)
                    Response.Write(Thumb(i.PictureUrl, GSettings.ThumbWidthOnList, GSettings.ThumbHeightOnList));
                Response.Write("</li>");
            }
        %>
    </ul>

    <ul>
        <li>页码：</li>

        <%        
            //输出页码 
            RenderPager(pager); 
        %>
    </ul>

</body>
</html>
