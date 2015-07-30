using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Nt.BLL
{
    public class MailSender
    {
        #region Props

        private EmailAccount _account;
        /// <summary>
        /// 使用的邮件发送者，如果未提供，则使用默认
        /// </summary>
        public EmailAccount EmailAccount
        {
            get { return _account; }
            set { _account = value; }
        }

        /// <summary>
        /// 是否使用异步发送
        /// </summary>
        public bool SendAsyc { get; set; }

        #endregion

        #region ctor

        public MailSender()
        {

        }

        public MailSender(EmailAccount account)
        {
            _account = account;
        }

        #endregion

        #region methods

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">内容，可以是html</param>
        /// <param name="to">发送地址</param>
        /// <param name="toName">接收者显示名</param>
        public void SendMail(string subject, string body, string to, string toName)
        {
            if (_account == null)
                InitEmailAccountFromDB();
            SendMail(subject, body, to, toName, _account.Email, _account.DisplayName, _account.Host, _account.Port, _account.UseDefaultCredentials, _account.EnableSsl, _account.UserName, _account.Password);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容，可以是html</param>
        /// <param name="to">邮件发送目的方</param>
        /// <param name="toName">目的方显示名</param>
        /// <param name="from">邮件发送方</param>
        /// <param name="fromName">发送方显示名</param>
        /// <param name="host">邮箱服务器主机</param>
        /// <param name="port">端口,默认25</param>
        /// <param name="useDefaultCredentials">是否使用DefaultCredentials，默认选false</param>
        /// <param name="enableSsl">是否使用Ssl，默认选false</param>
        /// <param name="username">权限：用户名</param>
        /// <param name="pwd">权限：密码</param>
        public void SendMail(
            string subject, string body,
            string to, string toName,
            string from, string fromName,
            string host, int port,
            bool useDefaultCredentials,
            bool enableSsl,
            string username, string pwd)
        {
            MailMessage message = new MailMessage();
            message.Subject = subject;
            message.To.Add(new MailAddress(to, toName));
            message.From = new MailAddress(from, fromName);
            message.Body = body;
            message.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.UseDefaultCredentials = useDefaultCredentials;
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl = enableSsl;
            if (_account.UseDefaultCredentials)
                smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
            else
                smtpClient.Credentials = new NetworkCredential(username, pwd);
            smtpClient.Send(message);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容，可以是html</param>
        /// <param name="to">邮件发送目的方</param>
        /// <param name="toName">目的方显示名</param>
        /// <param name="from">邮件发送方</param>
        /// <param name="fromName">发送方显示名</param>
        /// <param name="host">邮箱服务器主机</param>
        /// <param name="port">端口</param>
        /// <param name="username">权限：用户名</param>
        /// <param name="pwd">权限：密码</param>
        public void SendMail(
            string subject, string body,
            string to, string toName,
            string from, string fromName,
            string host, int port,
            string username, string pwd
        )
        {
            SendMail(subject, body, to, toName, from, fromName, host, port, false, false, username, pwd);
        }
        
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容，可以是html</param>
        /// <param name="to">邮件发送目的方</param>
        /// <param name="toName">目的方显示名</param>
        /// <param name="from">邮件发送方</param>
        /// <param name="fromName">发送方显示名</param>
        /// <param name="port">端口</param>
        /// <param name="username">权限：用户名</param>
        /// <param name="pwd">权限：密码</param>
        public void SendMail(
           string subject, string body,
           string to, string toName,
           string from, string fromName,
           string host,
           string username, string pwd
       )
        {
            SendMail(subject, body, to, toName, from, fromName, host, 25, false, false, username, pwd);
        }
        
        /// <summary>
        /// 初始化默认的
        /// </summary>
        void InitEmailAccountFromDB()
        {
            _account = DAL.DbAccessor.GetFirstOrDefault<EmailAccount>("isdefault desc");
            if (_account == null)
                throw new Exception("数据库中没有任何邮箱账号，请先添加!");
        }

        #endregion
    }
}
