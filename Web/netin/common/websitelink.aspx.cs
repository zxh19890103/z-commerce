using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.Model;
using Nt.Model.Common;
using Nt.Model.Setting;
using Nt.Model.View;
using Nt.Framework;
using Nt.Framework.Admin;
using Nt.DAL;
using Nt.BLL;
using System.Text;
using System.Data.SqlClient;
using System.Data;

public partial class netin_common_websitelink : ListBase<WebsiteLinkItem>, IAllAjax
{

    protected override string Where
    {
        get
        {
            return "LanguageId=" + NtContext.Current.LanguageID;
        }
    }

    public override void List()
    {
        BeginTable("ID", "连接词", "链接", "应用链接词", "操作");

        foreach (var item in DataSource)
        {
            Row(() =>
            {
                Td(item.Id);
                Td(item.Word);
                Td(item.Url);
                TdF("<a href=\"javascript:;\" onclick=\"apply({0});\">应用</a><a href=\"javascript:;\" onclick=\"unApply({0});\">取消应用</a>", item.Id);
                TdEditViaAjax(item.Id);
            });
        }
        EndTable(() =>
        {
            TdSpan(3);
            Td("<a href=\"javascript:;\" onclick=\"apply();\" class=\"a-button\">应用全部</a><a href=\"javascript:;\" onclick=\"unApply();\" class=\"a-button\">取消全部应用</a>");
            Html(TAG_TD_BEGIN);
            AddNewButtonViaAjax();
            Html(TAG_TD_END);
        });
    }

    /// <summary>
    /// 应用链接词
    /// </summary>
    [AjaxMethod]
    public void Apply()
    {
        int id = IMPOSSIBLE;
        int.TryParse(Request["id"], out id);
        var links = DbAccessor.GetList<WebsiteLinkItem>();
        AddWebLink(typeof(Article), "Body", links, id);
        AddWebLink(typeof(Product), "FullDescription", links, id);
        NtJson.ShowOK();
    }

    /// <summary>
    /// 取消应用链接词
    /// </summary>
    [AjaxMethod]
    public void UnApply()
    {
        int id = IMPOSSIBLE;
        int.TryParse(Request["id"], out id);
        var links = DbAccessor.GetList<WebsiteLinkItem>();
        CancelWebLink(typeof(Article), "Body", links, id);
        CancelWebLink(typeof(Product), "FullDescription", links, id);
        NtJson.ShowOK();
    }

    /// <summary>
    /// 取消所有的关键词链接  nt-website-link
    /// id=0时，表示应用所有的链接词
    /// </summary>
    /// <param name="links"></param>
    /// <param name="tab"></param>
    /// <returns></returns>
    int CancelWebLink(Type table, string field, List<WebsiteLinkItem> links, int id)
    {
        using (SqlConnection conn = SqlHelper.GetConnection())
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            string sql = string.Format("Select Id,[{2}] From {0} Where LanguageId={1}",
                M(table), Nt.BLL.NtContext.Current.LanguageID, field);
            cmd.CommandText = sql;
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(data);
            int counter = 0;
            string oldvalue = string.Empty;
            string newvalue = string.Empty;
            foreach (DataRow r in data.Rows)
            {
                string body = r[1].ToString();
                if (body == "")
                    continue;
                foreach (var item in links)
                {
                    if (id == IMPOSSIBLE || id == item.Id)
                    {
                        oldvalue = string.Format("<a class=\"nt-website-link\" href=\"{0}\">{1}</a>", item.Url, item.Word);
                        newvalue = item.Word;
                        body = body.Replace(oldvalue, newvalue);
                    }
                }
                r[field] = body;
            }
            adapter.UpdateCommand =
                new SqlCommand(
                    string.Format("UPDATE [{0}] Set [{1}]=@{1} Where ID=@ID;", M(table), field), conn);
            adapter.UpdateCommand.Parameters.Add("@" + field,
               SqlDbType.VarChar, int.MaxValue, field);
            adapter.UpdateCommand.Parameters.Add("@ID",
               SqlDbType.Int, 4, "ID");
            adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
            adapter.Update(data);
            return counter;
        }
    }

    /// <summary>
    /// 添加所有指定的链接到表tab
    /// </summary>
    /// <param name="links"></param>
    /// <param name="tab"></param>
    /// <returns></returns>
    int AddWebLink(Type table, string field, List<WebsiteLinkItem> links, int id)
    {
        using (SqlConnection conn = SqlHelper.GetConnection())
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            string sql = string.Format("Select Id,[{1}] From {0} Where LanguageId={2}",
                M(table), field, Nt.BLL.NtContext.Current.LanguageID);
            cmd.CommandText = sql;
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(data);
            int counter = 0;
            foreach (DataRow r in data.Rows)
            {
                string body = r[1].ToString();
                if (body == "")
                    continue;
                foreach (var item in links)
                {
                    if (id == IMPOSSIBLE || id == item.Id)
                    {
                        int c = 0;
                        body = WrapLink(body, item.Word, item.Url, out c);
                        counter += c;
                    }
                }
                r[field] = body;
            }
            adapter.UpdateCommand =
                new SqlCommand(
                    string.Format("UPDATE [{0}] Set [{1}]=@{1} Where ID=@ID;", M(table), field), conn);
            adapter.UpdateCommand.Parameters.Add("@" + field,
               SqlDbType.VarChar, int.MaxValue, field);
            adapter.UpdateCommand.Parameters.Add("@ID",
               SqlDbType.Int, 4, "ID");
            adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
            adapter.Update(data);
            return counter;
        }
    }

    /// <summary>
    /// 给指定的字符串中的关键词添加指定的链接
    /// </summary>
    /// <param name="input"></param>
    /// <param name="word"></param>
    /// <param name="href"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    string WrapLink(string input, string word, string href, out int c)
    {
        c = 0;
        if (input.Length == 0
            || word.Length == 0
            || input.Length < word.Length)
            return input;

        StringBuilder sb = new StringBuilder(input);
        int p = 0;//当前位置，起始索引
        int next = 0;//下一个进行匹配的起始索引
        int len = word.Length;//链接词的长度
        int maxIndex = 0;//最大索引值
        string tagBegin = string.Format("<a class=\"nt-website-link\" href=\"{0}\">", href, word);//开始标签
        int tagBeginLen = tagBegin.Length;//开始标签的长度
        string tagEnd = "</a>";//结束标签
        int tagEndLen = 4;//结束标签的长度
        int j = 0;
        bool success = true;//一个值，指示当前匹配是否成功
        bool breakable = false;//一个值，指示是否该退出循环
        while (true)
        {
            maxIndex = sb.Length - 1;
            success = true;
            j = 0;
            for (int i = 0; i < len; i++)
            {
                if (i + p > maxIndex)//如果指定超出字符串的最大索引，则指示可以退出循环
                    breakable = true;
                else
                {
                    if (sb[i + p] != word[j])
                    {
                        success = false;
                        break;
                    }
                    j++;
                }
            }
            if (breakable) break;

            if (success)
            {
                next = p + len;
                //进一步确认当前匹配是否有效
                if (next + 3 <= maxIndex
                   && sb[next] == '<'
                   && sb[next + 1] == '/'
                   && sb[next + 2] == 'a'
                   && sb[next + 3] == '>')
                {
                    next += tagEndLen;
                }
                else
                {
                    sb.Insert(p, tagBegin);
                    next += tagBeginLen;
                    sb.Insert(next, tagEnd);
                    next += tagEndLen;
                    c++;
                }
            }
            else
            {
                next = p + 1;
            }
            p = next;
        }
        return sb.ToString();
    }

    [AjaxMethod]
    public void TMPost()
    {
        AjaxSave<WebsiteLinkItem>();
    }

    [AjaxMethod]
    public void TMDel()
    {
        int id = IMPOSSIBLE;
        NtJson.EnsureNumber("id", "参数错误:id", out id);
        var links = DbAccessor.GetList<WebsiteLinkItem>();
        CancelWebLink(typeof(Article), "Body", links, id);
        CancelWebLink(typeof(Product), "FullDescription", links, id);
        DbAccessor.Delete(typeof(WebsiteLinkItem), id);
        NtJson.ShowOK();
    }

    [AjaxMethod]
    public void TMGet()
    {
        AjaxGet<WebsiteLinkItem>();
    }

    [AjaxMethod]
    public void TMList()
    {
        List();
    }

    protected override void Prepare()
    {
        base.Prepare();
        Title = "站内链接";
    }
}