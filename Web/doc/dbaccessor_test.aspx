<%@ Page Language="C#" %>

<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        //var d=DbAccessor.GetList<Article>("","id desc");
        //int c = DbAccessor.MaxID;
        //Response.Write(c);
        //Response.Write("<br/>");
        //foreach (var item in d)
        //{
        //    Response.Write(item.Id);
        //    Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;");
        //    Response.Write(item.Title);
        //    Response.Write("<br/>");
        //}        

        //int c=DbAccessor.GetMaxID(typeof(Article));
        //Response.Write(c);

        //var t = DbAccessor.Top<Article>(3, "id<31", "id desc");
        //foreach (var item in t)
        //{
        //    Response.Write(item.Id);
        //    Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;");
        //    Response.Write(item.Title);
        //    Response.Write("<br/>");
        //}

        //var a = new Article() {Title="new article",Body="body", LanguageId=1,ArticleClassId=1,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now};
        //var nid = DbAccessor.Insert(a);
        //Response.Write(nid);

        //int lang = 1, acid = 1;
        //DateTime now = DateTime.Now;
        //var arts = new Article[]{ 
        //    new Article{Title = "new article5  changed", Body = "body body body", LanguageId = lang, ArticleClassId =acid, CreatedDate = now, UpdatedDate = now,Id=29},
        //    new Article{Title = "new article6  changed", Body = "body body body", LanguageId = lang, ArticleClassId =acid, CreatedDate = now, UpdatedDate = now,Id=30},
        //     };
        //DbAccessor.InsertRange<Article>(arts);

        //var a = new Article() { Title = "new article changed", Body = "body changed", LanguageId = 1, ArticleClassId = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Id = 23 };
        //DbAccessor.Update(a);  

        //int lang = 1, acid = 1;
        //DateTime now = DateTime.Now;
        //var arts = new Article[]{ 
        //    new Article{Title = "new article5  changed", Body = "body body body", LanguageId = lang, ArticleClassId =acid, CreatedDate = now, UpdatedDate = now,Id=29},
        //    new Article{Title = "new article6  changed", Body = "body body body", LanguageId = lang, ArticleClassId =acid, CreatedDate = now, UpdatedDate = now,Id=30},
        //     };
        //DbAccessor.UpdateRange<Article>(arts);

        //var a = new Article() { Title = "new article", Body = "body", LanguageId = 1, ArticleClassId = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now,Id=1 };
        //DbAccessor.UpdateOrInsert(a);


        //DbAccessor.Delete("article", 30);
        //DbAccessor.Delete(typeof(Article), 30);
        //DbAccessor.Delete(typeof(Article), "id>10");
        //DbAccessor.Delete(typeof(Article), "12,2,3");
        //DbAccessor.Delete("article", 10, true);


        var db = new DbAccessor();
        //db.List("article").List("slider").List("goods").Execute();

        //foreach (DataRow item in db[0].Rows)
        //{
        //    Response.Write(item["id"]);
        //    Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;");
        //    Response.Write(item["pictureurl"]);
        //    Response.Write("<br/>");
        //}

        //db.List("Article_Class").Execute();
        //db.TreeBrand = "->";
        //db.Space = "-";
        //db.TreeSourceIndex = 0;
        //var artcls = db.GetTree<Article_Class>();
        //foreach (var item in artcls)
        //{
        //    Response.Write(item.Id);
        //    Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;");
        //    Response.Write(item.Parent);
        //    Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;");
        //    Response.Write(item.Name);
        //    Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;");
        //    Response.Write(item.Crumbs);
        //    Response.Write("<br/>");
        //}

        //var a = new Article() { Title = "new article changed twice", Body = "body", LanguageId = 1, ArticleClassId = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now,Id=30 };
        //db.Change(a).Change(a).Execute();

        //db.Add(a).Add(a).Add(a).Execute();


        //var list1 = db.GetDropdownlist<Article_Class>();        
        //var list=db.GetDropdownlist<Article>("title", "id", "id>10", "id desc");
        //var list = db.GetDropdownlist<Article>("title", "id", "id>10");
        //foreach (var i in list1)
        //{
        //    Response.Write(i.Value);
        //    Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;");
        //    Response.Write(i.Text);
        //    Response.Write("<br/>");
        //}

        //bool b=db.Exists(typeof(Article), "id>10");
        //Response.Write(b);
        //db.Columns("id","title as title2","(select ...) as colName1").List("article").Execute();



    }
</script>
