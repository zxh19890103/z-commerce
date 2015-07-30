using Nt.BLL;
using Nt.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// ServerBrowser 的摘要说明
/// </summary>
public class ServerBrowser
{
    Hashtable root;
    int cutStartIndex = 0;
    int counter = 0;
    string dirPath;// 文件夹根路径  dir-path
    bool loadSubDir;// 是否载入子文件夹里的内容 load-sub-dir 默认false
    string filter;// 文件过滤  filter 默认 .aspx
    HttpRequest request;
    HttpResponse response;

    public void Init()
    {
        request = WebHelper.Request;
        response = WebHelper.Response;

        dirPath = request["dir-path"];
        if (string.IsNullOrEmpty(dirPath))
            dirPath = "/";
        loadSubDir = false;
        if (!string.IsNullOrEmpty(request["load-sub-dir"]))
            loadSubDir = true;
        filter = request["filter"];
        if (string.IsNullOrEmpty(filter))
            filter = ".aspx";
    }

    public void Process()
    {
        string rootPath = WebHelper.MapPath(HttpUtility.UrlDecode(dirPath));
        cutStartIndex = AppDomain.CurrentDomain.BaseDirectory.Length - 1;
        root = NtJson.CreateFileTreeNode(0, dirPath, counter);
        DirectoryInfo di = new DirectoryInfo(rootPath);
        BrowserDir(di, root);
        NtJson json = new NtJson();
        json.Json["root"] = root;
        response.Write(json);
    }

    /// <summary>
    /// 遍历文件夹里的字文件夹和文件
    /// </summary>
    /// <param name="di"></param>
    /// <param name="dir"></param>
    void BrowserDir(DirectoryInfo di, Hashtable dir)
    {
        foreach (var folder in di.GetDirectories())
        {
            //if (folder.Name.ToLower().Equals("netin"))//屏蔽netin目录
            //    continue;
            counter++;
            Hashtable sub = NtJson.CreateFileTreeNode(0, 
                folder.FullName.Substring(cutStartIndex).Replace("\\","/"), counter);
            dir[folder.Name] = sub;
            if (loadSubDir)
                BrowserDir(folder, sub);
        }

        foreach (var file in di.GetFiles())
        {
            if (!filter.Contains(file.Extension.ToLower())) continue;
            counter++;
            dir[file.Name] = NtJson.CreateFileTreeNode(1,
                file.FullName.Substring(cutStartIndex).Replace("\\", "/"), 
                file.Extension, counter);
        }
    }
}