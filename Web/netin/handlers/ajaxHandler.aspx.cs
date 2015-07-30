using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.Framework;
using Nt.DAL;
using Nt.BLL;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using Nt.Model.Common;
using Nt.Model;

public partial class Netin_handlers_ajaxHandler : OnlyAjaxPage
{
    /// <summary>
    /// 设置bool值（取反）
    /// 所需参数：ids,field
    /// </summary>
    /// <returns></returns>
    [AjaxMethod]
    public void SetBoolean()
    {
        string ids, field;
        NtJson.EnsureNotNullOrEmpty("table", "缺少参数!", out table);
        NtJson.EnsureInt32Range("ids", out ids);
        NtJson.EnsureNotNullOrEmpty("field", "参数格式错误!", out field);
        table = DbAccessor.GetModifiedTableName(table);
        string sql = string.Format("Update {0} Set [{2}]=1-[{2}] Where id in ({1})",
            table, ids, field);
        SqlHelper.ExecuteNonQuery(sql);
        NtJson.ShowOK();
    }

    /// <summary>
    /// 从数据库删除
    /// </summary>
    [AjaxMethod]
    public void DeleteOnDB()
    {
        string ids;
        NtJson.EnsureNotNullOrEmpty("table", "缺少参数!", out table);
        NtJson.EnsureInt32Range("ids", out ids);
        table = DbAccessor.GetModifiedTableName(table);
        string sql = string.Format("Delete From [{0}] Where id in ({1})",
            table, ids);
        SqlHelper.ExecuteNonQuery(sql);
        NtJson.ShowOK();
    }

    /// <summary>
    /// 管理员登录
    /// </summary>
    [AjaxMethod]
    public void Login()
    {
        string username, password, checkcode;

        NtJson.EnsureNotNullOrEmpty("userName", "用户名不能为空！", out username);
        NtJson.EnsureNotNullOrEmpty("password", "密码不能为空！", out password);
        NtJson.EnsureNotNullOrEmpty("checkCode", "验证码不能为空!", out checkcode);

        //验证码
        var correctCode = Session[ConstStrings.SESSION_KEY_2_SAVE_CHECKCODE];
        if (correctCode == null
            || !correctCode.ToString().Equals(checkcode, StringComparison.OrdinalIgnoreCase))
        {
            NtJson.ShowError("验证码过期或者错误!");
        }

        SecurityService ss = new SecurityService();

        password = ss.Md5(password);
        table = "User";
        object raw = SqlHelper.ExecuteScalar(
           SqlHelper.GetConnection(),
            CommandType.Text,
           string.Format("Select Top 1 ID From [{1}] {0};\r\n" +
           "Update [{1}]  Set LoginTimes=LoginTimes+1,LastLoginDate=getdate(),LastLoginIP='" +
           WebHelper.GetIP() + "' {0} ", "Where (Upper(UserName))=@Param0 And [Password]=@Param1",
           DbAccessor.GetModifiedTableName(table)),
            new SqlParameter[] {
                new SqlParameter("@Param0", username.ToUpper()),
                new SqlParameter("@Param1",password)});

        if (raw == null)
            NtJson.ShowError("登录失败!用户名或密码不正确!");

        NtContext.Current.UserID = (int)raw;
        NtJson.ShowOK("登录成功!", new { username = NtContext.Current.CurrentUser.UserName });
    }

    /// <summary>
    /// 登出
    /// </summary>
    [AjaxMethod]
    public void Logout()
    {
        NtContext.Current.UserLogOut();
        NtJson.ShowOK();
    }

    /// <summary>
    /// 切换语言
    /// </summary>
    [AjaxMethod]
    public void SwitchLanguage()
    {
        int language = 0;
        NtJson.EnsureNumber("language", "参数错误:language", out language);
        NtContext.Current.LanguageID = language;
        NtJson.ShowOK();
    }

    /// <summary>
    /// 服务器端浏览文件夹、文件
    /// </summary>
    [AjaxMethod]
    public void ServerBrowse()
    {
        ServerBrowser sb = new ServerBrowser();
        sb.Init();
        sb.Process();
    }

    /// <summary>
    /// 获取类别迁移的目标选项
    /// type=1   Article_Class
    /// type=2  ProductCategory
    /// type=3  Goods_Class
    /// </summary>
    [AjaxMethod]
    public void OutSelections()
    {
        int from = 0, type = 0;
        NtJson.EnsureNumber("from", "参数错误:from", out from);
        NtJson.EnsureNumber("type", "参数错误:type", out type);

        string filter = string.Format("Display=1 And LanguageId={1} And Crumbs not like '%,{0},%' ",
            from, NtContext.Current.LanguageID);

        DbAccessor db = new DbAccessor();
        List<NtListItem> data = null;

        switch (type)
        {
            case 1:
                data = db.GetDropdownlist<Article_Class>(filter);
                break;
            case 2:
                data = db.GetDropdownlist<ProductCategory>(filter);
                break;
            case 3:
                data = db.GetDropdownlist<Goods_Class>(filter);
                break;
            default:
                NtJson.ShowError("参数type只能为1,2,3!");
                break;
        }
        data.Insert(0, new NtListItem("根级", "0"));

        string html = "";
        html += "<ul>";
        foreach (var item in data)
        {
            html += string.Format("<li val=\"{0}\">{1}</li>",
                item.Value, item.Text);
        }
        html += "</ul>";

        NtJson json = new NtJson();
        json.Json["ul"] = html;
        json.ErrorCode = 0;
        json.Json["message"] = "获取成功！";
        Response.Write(json);
        Response.End();
    }

    /// <summary>
    /// 类别迁移
    /// type=1   Article_Class
    /// type=2  ProductCategory
    /// type=3  Goods_Class
    /// </summary>
    [AjaxMethod]
    public void TreeMigrate()
    {
        int from = 0, to = 0, type = 0;
        NtJson.EnsureNumber("from", "参数错误：from", out from);
        NtJson.EnsureNumber("to", "参数错误：to", out to);
        NtJson.EnsureNumber("type", "参数错误:type", out type);

        string table = "";
        switch (type)
        {
            case 1:
                table = "Article_Class";
                break;
            case 2:
                table = "ProductCategory";
                break;
            case 3:
                table = "Goods_Class";
                break;
            default:
                NtJson.ShowError("参数type只能为1,2,3!");
                break;
        }

        table = DbAccessor.GetModifiedTableName(table);

        if (from == to
            || to == Convert.ToInt32(
            SqlHelper.ExecuteScalar("Select [Parent] From [" + table + "] Where id=" + from)))
            NtJson.ShowError("无效转移!");

        string phy_path = WebHelper.MapPath("/App_Data/Script/tree.migration.sql");
        if (!File.Exists(phy_path))
            NtJson.ShowError("不存在必需的脚本文件!");

        string sql = File.ReadAllText(phy_path);
        sql = sql
            .Replace("{tab}", table)
            .Replace("{targetID}", from.ToString())
            .Replace("{toParent}", to.ToString());

        foreach (string block in
            sql.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries))
        {
            SqlHelper.ExecuteNonQuery(block);
        }

        NtJson.ShowOK();

    }

    /// <summary>
    /// 记录目录迁移
    /// </summary>
    [AjaxMethod]
    public void BatchMigrate()
    {
        string ids = "";
        int to = 0;
        int type = 0;
        NtJson.EnsureInt32Range("ids", out ids);
        NtJson.EnsureNumber("to", "参数错误:to", out to);
        NtJson.EnsureNumber("type", "参数错误:type", out type);

        string table = "";
        string field = "";
        switch (type)
        {
            case 1:
                table = "Article";
                field = "ArticleClassId";
                break;
            case 2:
                table = "Product";
                field = "ProductCategoryId";
                break;
            case 3:
                table = "Goods";
                field = "GoodsClassId";
                break;
            default:
                NtJson.ShowError("参数type只能为1,2,3!");
                break;
        }

        table = DbAccessor.GetModifiedTableName(table);

        SqlHelper.ExecuteNonQuery(
            string.Format("update [{0}] set [{1}]={2} where id in ({3}) ", table, field, to, ids));

        NtJson.ShowOK();

    }

    /// <summary>
    /// 复制商品、产品、文章
    /// </summary>
    [AjaxMethod]
    public void Copy()
    {
        int id = 0, type = -1;
        NtJson.EnsureNumber("id", "参数错误:id", out id);
        NtJson.EnsureNumber("type", "参数错误:type", out type);
        string proc = "";
        switch (type)
        {
            case 0:
                proc = "np_copygoods"; break;
            case 1:
                proc = "np_copyproduct"; break;
            case 2:
                proc = "np_copyarticle"; break;
        }
        SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), CommandType.StoredProcedure, proc, new SqlParameter("@Id", id));
        NtJson.ShowOK();
    }

    /// <summary>
    /// 重新启动软件
    /// </summary>
    [AjaxMethod]
    public void RestartApp()
    {
        NtContext.Current.ReStartApplication();
        NtJson.ShowOK();
    }

    /// <summary>
    /// 载入图片
    /// </summary>
    [AjaxMethod]
    public void ImgLoad()
    {
        string url = "";
        NtJson.EnsureNotNullOrEmpty("url", "参数错误:url!", out url);
        url = Server.UrlDecode(url);
        MediaService ms = new MediaService();
        url = ms.GetPictureUrl(url);
        NtJson.ShowOK(url);
    }

}