using Nt.DAL;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Nt.BLL
{
    public class FilterService : BaseService, IService
    {
        public const string FILTER_COOKIE_KEY = "NT.FILTER.COOKIE.KEY";

        Dictionary<string, string> data;
        string cookieID = string.Empty;
        bool hasCookie = false;

        public FilterService()
        {
            if (WebHelper.Request.Cookies[FILTER_COOKIE_KEY] == null)
                return;
            cookieID = WebHelper.Request.Cookies[FILTER_COOKIE_KEY].Value;
            hasCookie = true;
        }

        public void Reset()
        {
            if (WebHelper.Request.QueryString["nt.search.clear"] != null)
                Empty();
            if (WebHelper.Request.QueryString["nt.is.search"] == null)
                return;
          
            string[] query = WebHelper.Request.RawUrl.Split(new char[] { '?' }, StringSplitOptions.RemoveEmptyEntries);
            string value = "";
            if (query.Length > 1)
                value = query[1];

            string sql = "if exists(select id from [{0}] where CookieID=@CookieID)\r\n begin\r\n";
            sql += " update [{0}] set query=@Query where CookieID=@CookieID\r\n";
            sql += "select @CookieID\r\n end \r\n";
            sql += " else \r\n begin \r\n";
            sql += " insert into [{0}](Query,CookieID)values(@Query,newid());\r\n";
            sql += " select CookieID from [{0}] where id=@@IDENTITY \r\n end \r\n";

            sql = string.Format(sql, DbAccessor.GetModifiedTableName("Filter"));

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@CookieID", cookieID);
            parameters[1] = new SqlParameter("@Query", value);

            var o = SqlHelper.ExecuteScalar(SqlHelper.GetConnection(), CommandType.Text, sql, parameters);

            //write cookie
            var cookie = new HttpCookie(FILTER_COOKIE_KEY
                    , o.ToString());
            cookie.Expires = DateTime.Now.AddDays(30);
            WebHelper.Response.SetCookie(cookie);
        }

        public void Get()
        {
            if (!hasCookie) return;

            Filter filter = DbAccessor.GetFirstOrDefault<Filter>("CookieID=N'" + cookieID + "' ", string.Empty);
            if (filter == null) return;
            data = new Dictionary<string, string>();
            foreach (var item in filter.Query.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] kv = item.Split('=');
                if (kv.Length == 1)
                {
                    data[kv[0]] = string.Empty;
                }
                else if (kv.Length > 1)
                {
                    data[kv[0]] = kv[1];
                }
            }
        }

        public void Empty()
        {
            if (!hasCookie) return;

            SqlHelper.ExecuteNonQuery(
                string.Format("update [{0}] set query=N'' where CookieID=N'{1}' ",
                DbAccessor.GetModifiedTableName("Filter"), cookieID));
        }

        public string Params(string name)
        {
            if (data != null && data.ContainsKey(name))
                return data[name];
            else
                return string.Empty;
        }

    }
}
