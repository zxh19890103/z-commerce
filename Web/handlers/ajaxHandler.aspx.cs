using Nt.DAL;
using Nt.Framework;
using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nt.BLL;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Nt.Model.Setting;
using Nt.Model.Enum;
using Nt.Model.View;

public partial class Handlers_ajaxHandler : OnlyAjaxPage
{
    /// <summary>
    /// 添加至购物车
    /// </summary>
    [AjaxMethod]
    public void AddToCart()
    {
        var user = Nt.BLL.NtContext.Current.CurrentCustomer;
        if (user == null)
            NtJson.ShowError(2, "没有登录!");

        int goodsid = 0;
        NtJson.EnsureNumber("goodsid", "参数错误!", out goodsid);
        string attrXml = Request["attrXml"];
        if (!Regex.IsMatch(attrXml, @"<ol>(<li[\s]+valueId=""\d+""[\s]adjustment=""[\d\.]+"">[\s\S]*</li>)*</ol>"))
            NtJson.ShowError("属性xml格式错误!");

        int quantity = 1;
        int.TryParse(Request["qty"], out quantity);

        var createdDate = DateTime.Now;

        ShoppingCartItem record = new ShoppingCartItem()
        {
            AttributesXml = attrXml,
            CreatedDate = createdDate,
            GoodsId = goodsid,
            CustomerId = user.Id,
            Quantity = quantity,
            UpdatedDate = DateTime.Now
        };

        DbAccessor.Insert(record);

        user.TotalOfShoppingCartItem += quantity;//商品的个数变了

        NtJson json = new NtJson();
        json.ErrorCode = NtJson.OK;
        json.Message = "已添加至购物车!";
        json.Json["total"] = user.TotalOfShoppingCartItem;
        Response.Write(json);
    }

    /// <summary>
    /// 从购物车里移除一个商品
    /// </summary>
    [AjaxMethod]
    public void DelFromCart()
    {
        if (Customer.TotalOfShoppingCartItem == 0)
            NtJson.ShowError("购物车里没有商品!");
        int id = 0;
        NtJson.EnsureNumber("id", "参数错误！", out id);
        DbAccessor.Delete(typeof(ShoppingCartItem), id);
        Customer.TotalOfShoppingCartItem--;
        NtJson.ShowOK("删除成功!", new { quantity = Customer.TotalOfShoppingCartItem });
    }

    /// <summary>
    /// 设置购物车里的商品数量
    /// </summary>
    [AjaxMethod]
    public void SetQuantity()
    {
        int quantity = 0;
        int id = 0;
        NtJson.EnsureNumber("id", "参数格式错误!", out id);
        NtJson.EnsureNumber("quantity", "参数格式错误!", out quantity);
        string sql = string.Format(
            "update [{0}] set quantity={1} where id={2}",
            DbAccessor.GetModifiedTableName("ShoppingCartItem"),
            quantity,
            id);
        SqlHelper.ExecuteNonQuery(sql);
        NtJson.ShowOK("恭喜");
    }

    /// <summary>
    /// 登录
    /// </summary>
    [AjaxMethod]
    public void Login()
    {
        var m = Nt.BLL.NtContext.Current.CurrentCustomer;
        if (m != null)
        {
            NtJson.ShowError("您已经登录！");
        }
        string loginname;
        NtJson.EnsureNotNullOrEmpty("LoginName", "登录名不能为空!", out loginname);
        string password;
        NtJson.EnsureNotNullOrEmpty("Password", "密码不能为空!", out password);
        string checkCode;
        NtJson.EnsureNotNullOrEmpty("checkCode", "验证码不能为空!", out checkCode);

        //验证码
        var correctCode = Session[ConstStrings.SESSION_KEY_2_SAVE_CHECKCODE];
        if (correctCode == null
            || !correctCode.ToString().Equals(checkCode, StringComparison.OrdinalIgnoreCase))
        {
            NtJson.ShowError("验证码过期或者错误!");
        }

        SecurityService ss = new SecurityService();

        string table = "Customer";
        table = DbAccessor.GetModifiedTableName(table);

        password = ss.Md5(password);
        object raw = SqlHelper.ExecuteScalar(
           SqlHelper.GetConnection(),
            CommandType.Text,
           string.Format("Select Top 1 ID From [{1}] {0};\r\n" +
           "Update [{1}]  Set LoginTimes=LoginTimes+1,LastLoginDate=getdate(),LastLoginIP='" +
           WebHelper.GetIP() + "' {0} ", "Where (Upper(Name))=@Param0 And [Password]=@Param1", table),
            new SqlParameter[] {
                new SqlParameter("@Param0", loginname.ToUpper()),
                new SqlParameter("@Param1",password)});

        if (raw == null)
            NtJson.ShowError("登录失败!用户名或密码不正确!");

        NtContext.Current.CustomerID = (int)raw;

        string redirectUrl = "/index.aspx";
        if (Request["redirectUrl"] != null)
            redirectUrl = Request["redirectUrl"];

        NtJson json = new NtJson();
        json.ErrorCode = NtJson.OK;
        json.Message = "登录成功!";
        json.Json["redirectUrl"] = redirectUrl;
        json.Json["user"] = raw;
        Response.Write(json);
    }

    /// <summary>
    /// 退出登录
    /// </summary>
    [AjaxMethod]
    public void Logout()
    {
        NtContext.Current.CustomerLogOut();
        NtJson.ShowOK("已经安全退出!");
    }

    /// <summary>
    /// 会员注册
    /// params:loginname,Password,Password.Again,Email,checkCode
    /// </summary>
    [AjaxMethod]
    public void Register()
    {
        if (Nt.BLL.NtContext.Current.CurrentCustomer != null)
        {
            NtJson.ShowError("已经有用户登录!请先退出!");
        }
        string loginnname;
        NtJson.EnsureUserName("loginname", out loginnname);
        string pass;
        NtJson.EnsurePassword("Password", out pass);
        string passAgain;
        NtJson.EnsureNotNullOrEmpty("Password.Again", "二次输入的密码不能为空!", out passAgain);

        if (pass != passAgain)
            NtJson.ShowError("两次输入的密码不一致!");

        string email;
        NtJson.EnsureEmail("Email", "邮箱格式不正确!此邮箱可帮你找回失去的密码。", out email
            );

        string ckcode;
        NtJson.EnsureNotNullOrEmpty("checkCode", "验证码不能为空!", out ckcode);

        //验证码
        var correctCode = Session[ConstStrings.SESSION_KEY_2_SAVE_CHECKCODE];
        if (correctCode == null
            || !correctCode.ToString().Equals(ckcode, StringComparison.OrdinalIgnoreCase))
        {
            NtJson.ShowError("验证码过期或者错误!");
        }

        int roleId = 1;

        #region 添加一个会员记录，并设置Session


        var role = DbAccessor.GetById<CustomerRole>(roleId);//默认组为1

        SecurityService ss = new SecurityService();

        Customer m = new Customer();
        m.Active = true;//注册后即可生效
        m.CreatedDate = DateTime.Now;
        m.BirthDay = DateTime.Now;
        m.LastLoginDate = DateTime.Now;
        m.Email = email;
        m.Name = loginnname;
        m.Password = ss.Md5(pass);
        m.CustomerRoleId = role.Id;
        m.Gender = true;
        m.Address = "";
        m.Phone = "";
        m.RealName = "";
        m.Zip = "";
        m.Mobile = "";
        m.Points = role.MeetPoints;
        m.RealName = loginnname;
        m.LastLoginDate = DateTime.Now;
        m.LastLoginIP = WebHelper.GetIP();
        m.LoginTimes = 1;

        int id = DbAccessor.Insert(m);
        NtContext.Current.CustomerID = id;
        NtJson.ShowOK("恭喜！注册成功！");

        #endregion
    }

    /// <summary>
    /// 自由留言
    /// 暂时还没有应用设置
    /// </summary>
    [AjaxMethod]
    public void SubmitGuestBook()
    {
        string email, name, title, body;

        var setting = SettingService.GetSettingModel<GuestBookSetting>(NtConfig.CurrentLanguageModel.LanguageCode);

        if (setting.EnableCheckCode)
        {
            string checkcode;
            NtJson.EnsureNotNullOrEmpty("checkcode", "验证码不能为空!", out checkcode);
            //验证码
            var correctCode = Session[ConstStrings.SESSION_KEY_2_SAVE_CHECKCODE];
            if (correctCode == null
                || !correctCode.ToString().Equals(checkcode, StringComparison.OrdinalIgnoreCase))
            {
                NtJson.ShowError("验证码过期或者错误!");
            }
        }

        NtJson.EnsureNotNullOrEmpty("body", "留言内容不能为空!", out body);

        //如果内容里包含了网址，则不允许留言
        if (setting.FiltrateUrl)
        {
            if (Regex.IsMatch(body, @"http[s]?://[\w\/\?\.]+"))
                NtJson.ShowError("对不起！内容中不允许包含网址！");
        }

        //替换敏感词汇为***
        if (setting.FiltrateSensitiveWords)
        {
            foreach (var w in setting.SensitiveWords.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                body = Regex.Replace(
                    body,
                    w,
                    new MatchEvaluator(mat =>
                    {
                        var s = "";
                        for (var i = 0; i < mat.Length; i++)
                            s += "*";
                        return s;
                    }),
                    RegexOptions.IgnoreCase);
            }
        }

        NtJson.EnsureNotNullOrEmpty("name", "姓名不能为空!", out name);
        NtJson.EnsureEmail("email", "邮箱格式不正确!", out email);
        NtJson.EnsureNotNullOrEmpty("title", "主题不能为空!", out title);

        GuestBook m = new GuestBook()
        {
            Type = 10,//10是什么意思？在App_Code里去寻找
            LanguageId = NtConfig.CurrentLanguage,
            Name = name,
            Body = body,
            Email = email,
            Title = title,
            Company = "",
            CreatedDate = DateTime.Now,
            Display = false,
            DisplayOrder = 1,
            EduDegree = "",
            Address = "",
            BirthDate = DateTime.Now,
            Gender = true,
            Grade = "",
            GraduatedFrom = "",
            Mobile = "",
            Nation = "",
            NativePlace = "",
            Note = "",
            PersonID = "",
            PoliticalRole = "",
            Tel = "",
            Viewed = false,
            ZipCode = ""
        };

        DbAccessor.Insert(m);
        NtJson json = new NtJson();
        json.ErrorCode = NtJson.OK;

        //发送邮件
        if (setting.EnableSendEmail)
        {
            //异步发送邮件
            new System.Threading.Thread(() =>
            {
                var mailsender = new MailSender();
                try
                {

                    mailsender.SendMail(title, body, setting.EmailAddressToReceiveEmail, setting.EmailToName);
                }
                catch (Exception ex)
                {
                    Logger.Instance.Log(ex.Message);//错误
                }
            }).Start();
        }

        json.Message = "恭喜:您的留言提交成功，请耐心等待管理员回复!";

        Response.Write(json);
    }

    /// <summary>
    /// 提交商品评论
    /// </summary>
    [AjaxAuthorize(Nt.Framework.AuthorizeFlag.Customer)]
    public void PostReview()
    {
        int goodsid = 0;
        NtJson.EnsureNumber("goodsid", "参数错误!", out goodsid);
        string body = "";
        NtJson.EnsureNotNullOrEmpty("body", "评论内容不能为空!", out body);

        Goods_Review m = new Goods_Review()
        {
            Title = NtUtility.GetSubString(body, 25),
            Rating = 0,
            CustomerId = Customer.Id,
            CreatedDate = DateTime.Now,
            GoodsId = goodsid,
            Body = body,
            IsApproved = false
        };

        DbAccessor.Insert(m);
        NtJson.ShowOK("恭喜:评论成功!请等待管理员审核!");
    }

    /// <summary>
    /// 评论点击量
    /// </summary>
    [AjaxAuthorize(AuthorizeFlag.Customer)]
    public void ReviewRating()
    {
        int id = 0;
        NtJson.EnsureNumber("id", "参数错误:id", out id);
        var o = SqlHelper.ExecuteScalar(
            string.Format(
            "update [{0}] set rating=rating+1 where id={1};\r\n select rating from [{0}] where id={1};",
            DbAccessor.GetModifiedTableName("goods_review"), id));
        string msg = o == null ? "点赞成功!" : o.ToString();
        NtJson.ShowOK(msg);
    }

    /// <summary>
    /// 提交订单
    /// </summary>
    [AjaxAuthorize(Nt.Framework.AuthorizeFlag.Customer)]
    public void SubmitOrder()
    {
        /*订单提交的操作步骤：
         * 0--处理购物车信息，如计算折扣、总价、手续费等等
         * 1--将当前会员购物车里的商品信息移动至订单分项
         * 2--删除购物车信息
         * 3--计算总价格和总折扣，生成总订单,
         */
        //表单验证

        int consigneeId = 0;
        NtJson.EnsureNumber("consigneeId", "请提供收货人!", out consigneeId);

        //configneeid == 0表示插入新收货人
        if (consigneeId == 0)
        {
            string zip, address, name, mobile, phone, email;
            NtJson.EnsureNotNullOrEmpty("ConsigneeName", "收货人姓名不能为空!", out name);
            NtJson.EnsureNotNullOrEmpty("ConsigneeAddress", "地址不能为空!", out address);
            NtJson.EnsureMatch("ConsigneeMobile", @"^1\d{10}$", "手机格式不正确!", out mobile);
            NtJson.EnsureMatch("ConsigneePhone", @"^\d{8,15}$", "联系电话格式不正确!", out phone);
            NtJson.EnsureEmail("ConsigneeEmail", "邮箱格式不正确!", out email);
            NtJson.EnsureMatch("ConsigneeZip", @"^\d{6}$", "邮政编码格式不正确!", out zip);

            Customer_Consignee consignee = new Customer_Consignee()
            {
                Address = address,
                CustomerId = Customer.Id,
                Email = email,
                Mobile = mobile,
                Name = name,
                Phone = phone,
                Zip = zip
            };

            consigneeId = DbAccessor.Insert(consignee);
        }

        int pmid, smid;
        NtJson.EnsureNumber("paymentId", "请提供支付方式!", out pmid);
        NtJson.EnsureNumber("shippingId", "请提供配送方式!", out smid);

        string ordermsg = string.Empty;
        ordermsg = Request["ordermsg"];

        PriceHandler pricer = new PriceHandler();
        var shoppingcartitems = DbAccessor.GetList<View_ShoppingCart>("customerid=" + Customer.Id, "");
        List<OrderItem> orderitems = new List<OrderItem>();
        foreach (var item in shoppingcartitems)
        {
            pricer.Run(item);

            orderitems.Add(new OrderItem()
            {
                AttributesXml = item.AttributesXml,

                Adjustment = pricer.Adjustment,
                DiscountAmount = pricer.DiscountAmount,
                Price = pricer.StandardPrice,

                SubTotal = pricer.SubTotal,
                SubMoneyforpayment = pricer.SubTotalMoneyforpayment,
                SubMoneyforshipping = pricer.SubTotalMoneyforshipping,
                SubTotalDiscount = pricer.SubTotalDiscountAmount,
                SubTotalPoints = pricer.SubTotalPoints,

                GoodsId = item.GoodsId,
                OrderItemGuid = Guid.NewGuid(),
                Quantity = item.Quantity,
                Note = string.Empty,
                OrderId = 0
            });
        }

        Order order = new Order()
        {
            OrderGuid = Guid.NewGuid(),
            CustomerId = Customer.Id,
            CustomerIp = WebHelper.GetIP(),
            CustomerConsigneeId = consigneeId,
            Status = (int)OrderStatus.Pending,
            ShippingStatus = smid == 0 ? (int)ShippingStatus.ShippingNotRequired : (int)ShippingStatus.NotYetShipped,
            PaymentStatus = (int)PaymentStatus.Pending,
            PaymentMethodId = pmid,
            ShippingMethodId = smid,
            OrderTotal = pricer.Total,
            OrderTotalDiscount = pricer.TotalDiscountAmount,
            RefundedAmount = 0.00M,//no
            CardType = string.Empty,
            CardName = string.Empty,
            CardNumber = string.Empty,
            PaidDate = DateTime.Now,
            Deleted = false,
            CreatedDate = DateTime.Now,
            Note = string.Empty,
            OrderMessage = ordermsg,
            ShippingExpense = pricer.TotalMoneyforshipping,
            CommissionCharge = pricer.TotalMoneyforpayment,
        };

        //插入订单
        int orderid = DbAccessor.Insert(order);

        orderitems.ForEach(o =>
        {
            o.OrderId = orderid;
        });

        //插入订单分项
        DbAccessor.InsertRange<OrderItem>(orderitems);

        //删除购物车
        DbAccessor.Delete(typeof(ShoppingCartItem), "customerid=" + Customer.Id);

        Customer.TotalOfShoppingCartItem = 0;

        NtJson.ShowOK("订单提交成功!");
    }

    /// <summary>
    /// 保存常用地址
    /// </summary>
    [AjaxMethod]
    public void SaveAddress()
    {
        Customer_Consignee consignee = new Customer_Consignee();
        consignee.InitDataFromPage();
        consignee.CustomerId = Customer.Id;
        DbAccessor.UpdateOrInsert(consignee);
        NtJson.ShowOK();
    }

    /// <summary>
    /// 修改会员密码
    /// </summary>
    [AjaxMethod]
    public void ChangePwd()
    {
        string oldpwd = Request["OldPwd"], newPwd = Request["NewPwd"], newPwd2 = Request["NewPwd.Again"];
        //check if the provided pwd equals to the  pwd of specified customer;
        SecurityService ss = new SecurityService();

        if (!ss.Md5(oldpwd).Equals(Customer.Password))
            NtJson.ShowError("您所提供的原始密码不正确!");

        //check farmat of pwd
        NtJson.EnsurePassword("NewPwd", out newPwd);

        //check if these two pwd equals to each other.
        if (!newPwd.Equals(newPwd2))
            NtJson.ShowError("两次输入的新密码不一致!");

        //update pwd
        string md5Pwd = ss.Md5(newPwd);
        SqlHelper.ExecuteNonQuery(string.Format("update [{0}] set password=N'{2}' where id={1}",
            DbAccessor.GetModifiedTableName("customer"),
            Customer.Id,
             md5Pwd));

        NtJson.ShowOK("密码修改成功!");

    }

    /// <summary>
    /// 保存会员账户信息
    /// </summary>
    [AjaxMethod]
    public void SaveAccountInfo()
    {
        string sql = string.Format("update [{0}] set RealName=@RealName,BirthDay=@BirthDay,Gender=@Gender,Email=@Email,Phone=@Phone,Mobile=@Mobile,Address=@Address,Zip=@Zip where Id=@Id", DbAccessor.GetModifiedTableName("customer"));

        System.Data.SqlClient.SqlParameter[] parameters = new System.Data.SqlClient.SqlParameter[] {
        new System.Data.SqlClient.SqlParameter("@RealName",Request["RealName"]),
        new System.Data.SqlClient.SqlParameter("@BirthDay",Request["BirthDay"]),
        new System.Data.SqlClient.SqlParameter("@Gender",Request["Gender"]),
        new System.Data.SqlClient.SqlParameter("@Email",Request["Email"]),
        new System.Data.SqlClient.SqlParameter("@Phone",Request["Phone"]),
        new System.Data.SqlClient.SqlParameter("@Mobile",Request["Mobile"]),
        new System.Data.SqlClient.SqlParameter("@Address",Request["Address"]),
        new System.Data.SqlClient.SqlParameter("@Zip",Request["Zip"]),
        new System.Data.SqlClient.SqlParameter("@Id",Customer.Id),
        };

        SqlHelper.ExecuteNonQuery(SqlHelper.GetConnection(), System.Data.CommandType.Text, sql, parameters);

        NtContext.Current.ClearCache();

        NtJson.ShowOK();
    }

    /// <summary>
    /// 删除常用地址
    /// </summary>
    [AjaxMethod]
    public void DelMyAddress()
    {
        int id;
        NtJson json = new NtJson();
        json.ErrorCode = NtJson.ERROR;
        json.Message = "参数错误:id";

        if (Int32.TryParse(Request["id"], out id) && id > 0)
        {
            int i = DbAccessor.Delete(DbAccessor.GetModifiedTableName("Customer_Consignee"), id);
            json.ErrorCode = i == 0 ? 1 : 0;
            json.Message = i == 0 ? "没有删除任何项!" : "恭喜:成功删除地址记录!";
        }

        Response.Write(json);
    }

    /// <summary>
    /// 删除收藏夹
    /// </summary>
    [AjaxMethod]
    public void DelWishlistItem()
    {
        int id;
        NtJson json = new NtJson();
        json.ErrorCode = NtJson.ERROR;
        json.Message = "参数错误:id";

        if (Int32.TryParse(Request["id"], out id) && id > 0)
        {
            int i = DbAccessor.Delete(DbAccessor.GetModifiedTableName("Customer_Wishlist"),
                string.Format("id={0} and customerid={1}", id, Customer.Id));
            json.ErrorCode = i == 0 ? 1 : 0;
            json.Message = i == 0 ? "没有删除任何项!" : "恭喜:成功删除收藏记录!";
        }
        Response.Write(json);
    }




}