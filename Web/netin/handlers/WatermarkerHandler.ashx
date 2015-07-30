<%@ WebHandler Language="C#" Class="WatermarkerHandler" %>

using System;
using System.Web;
using Nt.Framework;

public class WatermarkerHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        NtJson json = new NtJson();
        json.ErrorCode = 0;
        json.Json["message"] = Nt.BLL.WaterMarker.Instance.Message;
        json.Json["isRunning"] = Nt.BLL.WaterMarker.Instance.IsRunning;
        context.Response.Write(json);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}