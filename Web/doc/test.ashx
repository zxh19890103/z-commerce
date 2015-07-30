<%@ WebHandler Language="C#" Class="test" %>

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
using System;
using System.IO;
using System.Data.SqlClient;
using System.Data;

public class test : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        //Class2SqlGenerator c2s = new Class2SqlGenerator("/sql.sql");
        //c2s.Run();
        var m = new View_Article();
        m.Id =13;
        CalcSiblings(m, "", "");
        context.Response.Write(m.NextID);
        context.Response.Write("Hello World");
    }


    /// <summary>
    /// 计算上一页和下一页
    /// </summary>
    public void CalcSiblings(ISiblingsTraceable istM, string filter, string orderby)
    {
        Type t = istM.GetType();
        string tablename = t.Name;
        string namefield = "name";
        string sql = File.ReadAllText(WebHelper.MapPath("/app_data/script/calc.siblings.sql"));
        if (istM is Article)
            namefield = "title";
        if (string.IsNullOrEmpty(filter)) filter = "1=1";
        if (string.IsNullOrEmpty(orderby)) orderby = "1";
        sql = sql
            .Replace("{namefield}", namefield)
            .Replace("{tablename}", tablename)
            .Replace("{filter}", filter)
            .Replace("{orderby}", orderby)
            .Replace("{specifiedid}", istM.Id.ToString());
        using (SqlConnection conn = new SqlConnection(SqlHelper.GetConnSting()))
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            SqlDataReader rs = cmd.ExecuteReader(CommandBehavior.SingleRow);
            if (rs.Read())
            {
                istM.PreID = rs.GetInt32(0);
                istM.PreTitle = rs.GetString(1);
                istM.NextID = rs.GetInt32(2);
                istM.NextTitle = rs.GetString(3);
            }
            conn.Close();
        }
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}