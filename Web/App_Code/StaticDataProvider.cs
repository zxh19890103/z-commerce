using Nt.DAL;
using Nt.Model;
using Nt.Model.Common;
using Nt.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// StaticDataProvider 的摘要说明
/// </summary>
public class StaticDataProvider
{

    private static readonly StaticDataProvider _instance = new StaticDataProvider();

    public static StaticDataProvider Instance { get { return _instance; } }

    private List<NtListItem> _specificationTypeProvider;
    /// <summary>
    /// 规格属性的前台展现方式
    /// </summary>
    public List<NtListItem> SpecificationTypeProvider
    {
        get
        {
            if (_specificationTypeProvider == null)
            {
                _specificationTypeProvider = new List<NtListItem>();
                _specificationTypeProvider.AddRange(new NtListItem[] {
                    new NtListItem("颜色",Goods_Attribute_Mapping.CT_COLORSQUARES),
                    new NtListItem("下拉框",Goods_Attribute_Mapping.CT_DROPDOWNLIST),
                    new NtListItem("多行文本",Goods_Attribute_Mapping.CT_MUTILINETEXTBOX),
                    new NtListItem("单行文本",Goods_Attribute_Mapping.CT_TEXTBOX),
                    new NtListItem("单选",Goods_Attribute_Mapping.CT_RADIOBUTTONLIST),
                    new NtListItem("复选框",Goods_Attribute_Mapping.CT_CHECKBOXES),
                    new NtListItem("上传文件",Goods_Attribute_Mapping.CT_FILEUPLOAD),
                    new NtListItem("日期选择",Goods_Attribute_Mapping.CT_DATEPICKER),
                    });
            }
            return _specificationTypeProvider;
        }
    }


    private List<NtListItem> _orderStatusProvider;
    /// <summary>
    /// 订单状态
    /// </summary>
    public List<NtListItem> OrderStatusProvider
    {
        get
        {
            if (_orderStatusProvider == null)
            {
                _orderStatusProvider = new List<NtListItem>();
                _orderStatusProvider.AddRange(new NtListItem[] {
                    new NtListItem("等待处理",(int)OrderStatus.Pending),
                    new NtListItem("正在处理",(int)OrderStatus.Processing),
                    new NtListItem("订单处理完毕",(int)OrderStatus.Complete),
                    new NtListItem("订单已取消",(int)OrderStatus.Cancelled),
                    });
            }
            return _orderStatusProvider;
        }
    }

    private List<NtListItem> _paymentStatusProvider;
    /// <summary>
    /// 支付状态
    /// </summary>
    public List<NtListItem> PaymentStatusProvider
    {
        get
        {
            if (_paymentStatusProvider == null)
            {
                _paymentStatusProvider = new List<NtListItem>();
                _paymentStatusProvider.AddRange(new NtListItem[] {
                    new NtListItem("等待支付",(int)PaymentStatus.Pending),
                    new NtListItem("已授权支付",(int)PaymentStatus.Authorized),
                    new NtListItem("已支付",(int)PaymentStatus.Paid),
                    new NtListItem("部分退款",(int)PaymentStatus.PartiallyRefunded),
                    new NtListItem("全部退款",(int)PaymentStatus.Refunded),
                    new NtListItem("交易完成",(int)PaymentStatus.Voided),
                    });
            }
            return _paymentStatusProvider;
        }
    }

    private List<NtListItem> _shippingStatusProvider;
    /// <summary>
    /// 运送状态
    /// </summary>
    public List<NtListItem> ShippingStatusProvider
    {
        get
        {
            if (_shippingStatusProvider == null)
            {
                _shippingStatusProvider = new List<NtListItem>();
                _shippingStatusProvider.AddRange(new NtListItem[] {
                    new NtListItem("无需运送",(int)ShippingStatus.ShippingNotRequired),
                    new NtListItem("尚未发送",(int)ShippingStatus.NotYetShipped),
                    new NtListItem("部分已送达",(int)ShippingStatus.PartiallyShipped),
                    new NtListItem("全部送达",(int)ShippingStatus.Shipped),
                    new NtListItem("已交货",(int)ShippingStatus.Delivered),
                    });
            }
            return _shippingStatusProvider;
        }
    }


    private List<NtListItem> _guestBookTypeProvider;
    /// <summary>
    /// 运送状态
    /// </summary>
    public List<NtListItem> GuestBookTypeProvider
    {
        get
        {
            if (_guestBookTypeProvider == null)
            {
                _guestBookTypeProvider = new List<NtListItem>();
                _guestBookTypeProvider.AddRange(new NtListItem[] {
                    new NtListItem("未归档留言",0),
                    new NtListItem("游客留言",10),
                    });
            }
            return _guestBookTypeProvider;
        }
    }

    private List<NtListItem> _thumbGenerationModeProvider;
    /// <summary>
    /// 缩略图生成模式
    /// "HW", "H", "W", "CUT", "CUTA"
    /// "指定宽高(可能变形)", "指定高缩小", "指定宽缩小", "按指定宽高剪切", "指定高宽裁减（不变形）自定义" 
    /// </summary>
    public List<NtListItem> ThumbGenerationModeProvider
    {
        get
        {
            if (_thumbGenerationModeProvider == null)
            {
                _thumbGenerationModeProvider = new List<NtListItem>();
                _thumbGenerationModeProvider.AddRange(new NtListItem[] {
                    new NtListItem("按指定宽高剪切",(int)ThumbnailGenerationMode.CUT),
                    new NtListItem("指定高宽裁减（不变形）自定义",(int)ThumbnailGenerationMode.CUTA),
                    new NtListItem("指定高缩小",(int)ThumbnailGenerationMode.H),
                    new NtListItem("指定宽高(可能变形)",(int)ThumbnailGenerationMode.HW),
                    new NtListItem("指定宽缩小",(int)ThumbnailGenerationMode.W),
                    });
            }
            return _thumbGenerationModeProvider;
        }
    }


    private List<NtListItem> _markPositionProvider;
    /// <summary>
    /// 水印生成位置
    /// </summary>
    public List<NtListItem> MarkPositionProvider
    {
        get
        {
            if (_markPositionProvider == null)
            {
                _markPositionProvider = new List<NtListItem>();
                _markPositionProvider.AddRange(new NtListItem[] {
                    new NtListItem("Center",(int)WaterMarkPositionOption.Center),
                    new NtListItem("Down",(int)WaterMarkPositionOption.Down),
                    new NtListItem("LeftDown",(int)WaterMarkPositionOption.LeftDown),
                    new NtListItem("LeftUp",(int)WaterMarkPositionOption.LeftUp),
                    new NtListItem("RightDown",(int)WaterMarkPositionOption.RightDown),
                    new NtListItem("RightUp",(int)WaterMarkPositionOption.RightUp),
                    new NtListItem("Up",(int)WaterMarkPositionOption.Up),
                    });
            }
            return _markPositionProvider;
        }
    }

    private List<NtListItem> _fontFamilyProvider;
    /// <summary>
    /// 水印字体
    /// "Arial", "System", "仿宋", "黑体", "楷体", "Times New Roman", "微软雅黑", "宋体"
    /// </summary>
    public List<NtListItem> FontFamilyProvider
    {
        get
        {
            if (_fontFamilyProvider == null)
            {
                _fontFamilyProvider = new List<NtListItem>();
                _fontFamilyProvider.AddRange(new NtListItem[] {
                    new NtListItem("Arial","Arial"),
                    new NtListItem("System","System"),
                    new NtListItem("仿宋","仿宋"),
                    new NtListItem("黑体","黑体"),
                    new NtListItem("楷体","楷体"),
                    new NtListItem("Times New Roman","Times New Roman"),
                    new NtListItem("微软雅黑","微软雅黑"),
                    new NtListItem("宋体","宋体"),
                    });
            }
            return _fontFamilyProvider;
        }
    }


    private List<NtListItem> _naviTypeProvider;
    /// <summary>
    /// 导航类型
    /// wenzhang chanping shangpin erjiye xiazai qita
    /// </summary>
    public List<NtListItem> NaviTypeProvider
    {
        get
        {
            if (_naviTypeProvider == null)
            {
                _naviTypeProvider = new List<NtListItem>();
                _naviTypeProvider.AddRange(new NtListItem[] {
                     new NtListItem("链接",(int)ModuleType.Link),
                     new NtListItem("文章",(int)ModuleType.Article),
                     new NtListItem("产品",(int)ModuleType.Product),
                     new NtListItem("商品",(int)ModuleType.Goods),
                     new NtListItem("二级页",(int)ModuleType.Page),
                     new NtListItem("下载",(int)ModuleType.Download),
                     new NtListItem("栏目",(int)ModuleType.Folder),
                    });
            }
            return _naviTypeProvider;
        }
    }


    private List<NtListItem> _faqTypeProvider;
    /// <summary>
    /// Fag分类
    /// </summary>
    public List<NtListItem> FaqTypeProvider
    {
        get
        {
            if (_faqTypeProvider == null)
            {
                _faqTypeProvider = new List<NtListItem>();
                _faqTypeProvider.AddRange(new NtListItem[] {
                    new NtListItem("未归档问题",0),
                    new NtListItem("基本问题",10),
                    });
            }
            return _faqTypeProvider;
        }
    }


}