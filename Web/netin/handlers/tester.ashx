<%@ WebHandler Language="C#" Class="tester" %>

using System;
using System.Web;

public class tester : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World?ids=23d");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}