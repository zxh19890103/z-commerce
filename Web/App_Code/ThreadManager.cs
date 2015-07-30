using Nt.BLL;
using Nt.DAL;
using Nt.Model;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

/// <summary>
/// fuck 的摘要说明
/// </summary>
public class ThreadManager
{
    #region singletion
    static ThreadManager _current = null;
    static readonly object padlock = new object();

    public static ThreadManager Instance
    {
        get
        {
            lock (padlock)
            {
                if (_current == null)
                {
                    _current = new ThreadManager();
                }
                return _current;
            }
        }
    }
    #endregion

    int _interval = 1000;
    /// <summary>
    /// 静态页面生成的时间间隔，毫秒为单位，最少为1000
    /// </summary>
    public int Interval { get { return _interval; } set { if (value > 1000)_interval = value; } }
    bool _isRunning = false;
    /// <summary>
    /// 是否运行中
    /// </summary>
    public bool IsRunning { get { return _isRunning; } }
    string _msg = string.Empty;
    /// <summary>
    /// 消息
    /// </summary>
    public string Msg { get { return string.IsNullOrEmpty(_msg) ? "no msg!" : _msg; } private set { _msg = value; } }

    public ThreadManager()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 首页静态化
    /// </summary>
    public void IndexPagesHtmlize()
    {
        if (IsRunning)
        {
            Msg = "program is running.";
            return;
        }

        var thread = new Thread(new ThreadStart(() =>
        {
            _isRunning = true;
            string fileContent = File.ReadAllText(WebHelper.MapPath("/netin/html/indexfiles.config"));
            string[] couples = fileContent.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Htmlizer h = new Htmlizer();
            string aspx, html;
            for (int i = 0; i < couples.Length; i++)
            {
                aspx = couples[i];
                i++;
                html = couples[i];
                Msg = string.Format("{0} to {1}", aspx, html);
                h.GenerateHtml(aspx, html);
                Thread.Sleep(Interval);
            }
            _isRunning = false;
            Msg = "program is stop.";
        }));

        thread.Start();
    }

    /// <summary>
    /// 生成详细页静态文件，
    /// 文章、产品、商品、二级页
    /// </summary>
    public void DetailPagesHtmlize<T>()
        where T : BaseEntity, IHtmlizable, new()
    {
        if (IsRunning)
        {
            Msg = "program is running.";
            return;
        }

        var thread = new Thread(new ThreadStart(() =>
        {
            _isRunning = true;
            var data = DbAccessor.GetList<T>();
            Htmlizer h = new Htmlizer();
            foreach (var d in data)
            {
                h.GenerateHtml(d);
                Msg = string.Format("{0} to /html/x/{1}.html", d.DetailTemplate, d.Id);
                Thread.Sleep(Interval);
            }
            _isRunning = false;
            Msg = "program is stop.";
        }));

        thread.Start();
    }
}