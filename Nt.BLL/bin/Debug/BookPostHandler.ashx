<%@ WebHandler Language="C#" Class="BookPostHandler" %>

using System;
using System.Web;
using Nt.Model;
using Nt.Framework;
using Nt.BLL;
using Nt.BLL.Extension;
using Nt.BLL.Mail;
using Nt.Web;
using Nt.Model.SettingModel;

public class BookPostHandler : BaseHandler
{
    BookSettings settings = null;//设置

    protected override void Handle()
    {
        settings = SettingService.GetSettingModel<BookSettings>();
        Nt_Book book = new Nt_Book();
        book.InitDataFromPage();
        book.Language_Id = NtContext.Current.LanguageID;
        book.Type = 0;//默认分类为10，如果有需要分类，则需要在表单提供分类选择(<select></select>)
        book.Display = false;//默认为不显示
        book.AddDate = DateTime.Now;
        if (Validate(book))
        {
            PostMessage(book);
        }
        string redirectUrl = NtConfig.CurrentTemplatesPath;
        if (!string.IsNullOrEmpty(Request.QueryString["redirectUrl"]))
        {
            redirectUrl = Request.QueryString["redirectUrl"];
        }
        Alert(redirectUrl);
    }

    /// <summary>
    /// 提交留言
    /// </summary>
    /// <param name="book"></param>
    void PostMessage(Nt_Book book)
    {
        try
        {
            BookService service = new BookService();

            if (settings.EnableSendEmail)
            {
                try
                {
                    MailSendService mailsender = new MailSendService();
                    mailsender.SendMail("游客留言:" + book.Title,
                        book.Body,
                        settings.EmailAddressToReceiveEmail, book.Name);
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message);
                }
            }

            if (settings.FiltrateSensitiveWords)
            {
                book.Body = CommonUtility.FilterSensitiveWords(book.Body, settings.SensitiveWords);
            }

            book.Language_Id = NtContext.Current.LanguageID;
            service.Insert(book);
            AppendMessage("Message submission is successful!");
        }
        catch
        {
            AppendMessage("Message submission failed!");
        }
    }


    /// <summary>
    /// 验证
    /// </summary>
    /// <param name="book"></param>
    /// <returns></returns>
    bool Validate(Nt_Book book)
    {
        bool flag = true;

        if (settings.EnableCheckCode)
        {
            var cookie = Request.Cookies[ConstStrings.COOKIES_KEY_2_SAVE_CHECKCODE];
            if (cookie == null
                || Request.Form["CheckCode"] == null
                || !cookie.Value.Equals(Request.Form["CheckCode"].ToString(), StringComparison.OrdinalIgnoreCase))
            {
                AppendMessage("Verification code error!");
                flag = false;
            }
        }

        if (book.Title == "")
        {
            AppendMessage("Title can not be empty!");
            flag = false;
        }
        if (book.Body == "")
        {
            AppendMessage("Content can not be empty!");
            flag = false;
        }

        if (!NtUtility.IsValidEmail(book.Email))
        {
            AppendMessage("E-mail address is incorrect!");
            flag = false;
        }

        if (settings.FiltrateUrl && CommonUtility.ContainsUrl(book.Body))
        {
            AppendMessage("Content can not contain URLs!");
            flag = false;
        }

        return flag;

    }

}