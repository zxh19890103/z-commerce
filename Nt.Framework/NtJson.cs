using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using System.Collections;
using Nt.Model;
using System.Web;
using System.Text.RegularExpressions;
using Nt.BLL;

namespace Nt.Framework
{
    public class NtJson
    {
        public const int OK = 0;
        public const int ERROR = 1;
        public const int NOT_LOGIN = 2;
        public const int NOT_AUTHORIZED = 3;

        Hashtable hash;
        public NtJson(object data)
        {
            hash = new Hashtable();
            ParseJson(data);
        }

        public NtJson()
        {
            hash = new Hashtable();
        }

        public void ParseJson(object data)
        {
            if (data == null) return;
            foreach (var item in data.GetType().GetProperties())
            {
                hash[item.Name] = item.GetValue(data, null);
            }
        }

        int error = 0;
        /// <summary>
        /// 错误码：0表示没有错误
        /// </summary>
        public int ErrorCode
        {
            get
            {
                return error;
            }
            set
            {
                error = value;
                Json["error"] = error;
            }
        }

        string msg = "no message!";
        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get { return msg; }
            set
            {
                msg = value;
                Json["message"] = msg;
            }
        }

        public Hashtable Json
        {
            get { return hash; }
        }

        static HttpRequest Request { get { return WebHelper.Request; } }
        static HttpResponse Response { get { return WebHelper.Response; } }

        #region ajax utility

        /// <summary>
        /// 确保请求参数不为空
        /// </summary>
        public static void EnsureNotNullOrEmpty(string paramName, string msgIfError, out string str)
        {
            if (string.IsNullOrEmpty(Request[paramName]))
                ShowError(msgIfError);
            str = Request[paramName];
        }

        /// <summary>
        /// 确保请求参数为数字
        /// </summary>
        public static void EnsureNumber(string paramName, string msgIfError, out int num)
        {
            if (!int.TryParse(Request[paramName], out num))
                ShowError(msgIfError);
        }

        /// <summary>
        /// 确保请求参数为bool型数据
        /// </summary>
        public static void EnsureBoolean(string paramName, string msgIfError, out bool b)
        {
            if (!bool.TryParse(Request[paramName], out b))
                ShowError(msgIfError);
        }

        /// <summary>
        /// 确保请求参数与指定的正则表达式相匹配
        /// </summary>
        public static void EnsureMatch(string paramName, string pattern, string msgIfError, out string str)
        {
            str = Request[paramName];
            if (str == null) ShowError(msgIfError);
            if (!Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase))
                ShowError(msgIfError);
        }

        /// <summary>
        /// 确保字符串为url格式
        /// </summary>
        public static void EnsureAbsUrl(string paramName, string msgIfError, out string url)
        {
            EnsureMatch(paramName, @"^(/[\w\.-]+)+\.(\w+)$", msgIfError, out url);
        }

        /// <summary>
        /// 密码
        /// </summary>
        public static void EnsurePassword(string paramName, out string pass)
        {
            NtJson.EnsureMatch(paramName,
           @"^(\w){6,20}$",
           "密码只能输入6-20个字母、数字、下划线", out pass);
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public static void EnsureUserName(string paramName, out string name)
        {
            NtJson.EnsureMatch(paramName,
            "^[a-zA-Z][a-zA-Z0-9]{4,20}$",
            "登陆名错误，以字母开头，4-20位字母或数字!", out name);
        }

        /// <summary>
        /// 是否电话号码，包括固话和手机
        /// </summary>
        public static void EnsurePhone(string paramName, out string num)
        {
            NtJson.EnsureMatch(paramName,
            @"(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)",
            "电话号码格式错误!", out num);
        }

        /// <summary>
        ///确保邮箱格式
        /// </summary>
        public static void EnsureEmail(string paramName, string msgIfError, out string email)
        {
            EnsureMatch(paramName,
                "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$",
                msgIfError, out email);
        }

        /// <summary>
        /// 判断字符串是否是形似1,2,3,4,5,...
        /// </summary>
        public static void EnsureInt32Range(string paramName, out string input)
        {
            NtJson.EnsureMatch(paramName,
            @"^([0-9]\d*)(,[0-9]\d*)*$",
            "参数格式错误:" + paramName + "!", out input);
        }

        /// <summary>
        /// 多个字符串，用逗号隔开
        /// </summary>
        public static void EnsureStrArray(string paramName, out string input)
        {
            NtJson.EnsureMatch(paramName,
        @"^(.+)(,(.+))*$",
        "参数格式错误:" + paramName + "!", out input);
        }

        /// <summary>
        /// 匹配Url
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="input">输入字符串</param>
        public static void EnsureUrl(string paramName, out string input)
        {
            NtJson.EnsureMatch(paramName,
        @"^(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?$",
        "参数格式错误:" + paramName + "!", out input);
        }

        #endregion

        #region Error OR OK

        /// <summary>
        /// 提示错误
        /// </summary>
        /// <param name="message"></param>
        public static void ShowError(string message)
        {
            Response.Clear();
            Response.Write(new NtJson(new { error = 1, message = message }));
            Response.End();
        }

        public static void ShowError(string message, object args)
        {
            Response.Clear();
            Response.Write(new NtJson(new { error = 1, message = message }));
            Response.End();
        }

        /// <summary>
        /// 显示错误
        /// </summary>
        /// <param name="errorCode">错误码</param>
        /// <param name="message">消息</param>
        public static void ShowError(int errorCode, string message)
        {
            Response.Clear();
            Response.Write(new NtJson(new { error = errorCode, message = message }));
            Response.End();
        }

        /// <summary>
        /// 操作失败
        /// </summary>
        public static void ShowError()
        {
            ShowError("操作失败!");
        }

        /// <summary>
        /// 提示成功
        /// </summary>
        /// <param name="message">消息</param>
        public static void ShowOK(string message)
        {
            Response.Clear();
            Response.Write(new NtJson(new { error = 0, message = message }));
            Response.End();
        }

        /// <summary>
        /// /// 成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="arg">json</param>
        public static void ShowOK(string message, object arg)
        {
            Response.Clear();
            var json = new NtJson(new { error = 0, message = message });
            json.ParseJson(arg);
            Response.Write(json);
            Response.End();
        }

        /// <summary>
        /// 操作成功
        /// </summary>
        public static void ShowOK()
        {
            ShowOK("操作成功!");
        }
        #endregion

        #region tree node

        public static Hashtable CreateFileTreeNode(int type, string url, string format, int id)
        {
            Hashtable hash = new Hashtable();
            hash["type"] = type;
            hash["url"] = url;
            hash["id"] = id;
            hash["format"] = format;
            return hash;
        }

        public static Hashtable CreateFileTreeNode(int type, string url, string format)
        {
            Hashtable hash = new Hashtable();
            hash["type"] = type;
            hash["url"] = url;
            hash["format"] = format;
            return hash;
        }

        public static Hashtable CreateFileTreeNode(int type, string url, int id)
        {
            Hashtable hash = new Hashtable();
            hash["type"] = type;
            hash["url"] = url;
            hash["id"] = id;
            return hash;
        }

        public static Hashtable CreateFileTreeNode(int type, string url)
        {
            Hashtable hash = new Hashtable();
            hash["type"] = type;
            hash["url"] = url;
            return hash;
        }

        #endregion

        public override string ToString()
        {
            return JsonMapper.ToJson(hash);
        }
    }
}
