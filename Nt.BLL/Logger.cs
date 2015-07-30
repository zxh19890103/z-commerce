using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.BLL
{
    public class Logger : BaseService
    {
        /// <summary>
        /// 插入日志信息
        /// </summary>
        /// <param name="description"></param>
        /// <param name="args"></param>
        public void Log(string description, params object[] args)
        {
            if (string.IsNullOrEmpty(description))
                return;
            DB.Add(new Nt.Model.Log()
            {
                CreatedDate = DateTime.Now,
                Description = string.Format(description, args),
                LoginIP = WebHelper.GetIP(),
                UserID = NtContext.Current.CurrentUser.Id,
                RawUrl = WebHelper.Request.RawUrl
            }).Execute();
        }

        /// <summary>
        /// 用户xx于yyyy-MM-dd hh:mm:ss修改[添加]了text:value
        /// </summary>
        /// <param name="edit"></param>
        /// <param name="title"></param>
        public void Log(bool edit, string text, string value)
        {
            Log("{0}于{1}{2}了{3}:{4}",
                NtContext.Current.CurrentUser.UserName,
                DateTime.Now,
                edit ? "修改" : "添加",
                text,
                value);
        }

        private static readonly Logger _instance = new Logger();
        /// <summary>
        /// 一个只读实例
        /// </summary>
        public static Logger Instance
        {
            get
            {
                return _instance;
            }
        }

    }
}
