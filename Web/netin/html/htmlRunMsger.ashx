<%@ WebHandler Language="C#" Class="htmlRunMsger" %>

using System;
using System.Web;
using Nt.BLL;

public class htmlRunMsger : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        if (ThreadManager.Instance.IsRunning)
            context.Response.Write(ThreadManager.Instance.Msg);
        else
            context.Response.Write("stopped");
       // context.Response.Write(ThreadManager.Instance.IsRunning);
        //context.Response.Write(ThreadManager.Instance.Msg);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}