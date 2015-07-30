<%@ WebHandler Language="C#" Class="delOrderHandler" %>

using System;
using System.Web;
using Nt.Framework;
using Nt.DAL;

public class delOrderHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        int id = 0;
        NtJson.EnsureNumber("id", "参数错误:Id", out id);
        NtJson json = new NtJson();
        SqlHelper.ExecuteNonQuery("Update [" +
            DbAccessor.GetModifiedTableName("Order") + "] Set Deleted=1 Where ID=" + id);
        json.ErrorCode = 0;
        json.Json["message"] = Nt.BLL.WaterMarker.Instance.Message;
        context.Response.Write(json);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}