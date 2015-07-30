using Nt.BLL;
using Nt.DAL;
using Nt.Model.View;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

/// <summary>
/// PriceArgs 的摘要说明
/// </summary>
public class PriceHandler
{

    #region fields

    View_ShoppingCart _cart;

    #region per
    /// <summary>
    /// (单个)标准单价
    /// </summary>
    decimal _price;
    /// <summary>
    /// (单个)价格调整（不同规格）
    /// </summary>
    decimal _adjustment;
    /// <summary>
    /// 单个商品的折扣量
    /// </summary>
    decimal _discount;

    #endregion

    #region sub

    /// <summary>
    /// 订单项总价
    /// </summary>
    decimal _subtotal;
    /// <summary>
    /// 订单项总折扣
    /// </summary>
    decimal _subtotalDiscount;
    /// <summary>
    /// 订单项总运费
    /// </summary>
    decimal _subtotalMoneyforshipping;
    /// <summary>
    /// 订单项总支付费
    /// </summary>
    decimal _subtotalMoneyforpayment;

    int _subtotalPoints;

    #endregion

    #region total

    /// <summary>
    /// 订单总费用
    /// </summary>
    decimal _total;
    /// <summary>
    /// 订单总折扣
    /// </summary>
    decimal _totalDiscount;
    /// <summary>
    /// 订单总运费
    /// </summary>
    decimal _totalMoneyforshipping;
    /// <summary>
    /// 订单总支付费
    /// </summary>
    decimal _totalMoneyforpayment;
    /// <summary>
    /// 订单总商品数
    /// </summary>
    int _totalQuantity;
    /// <summary>
    /// 总积分
    /// </summary>
    int _totalPoints;

    #endregion



    #endregion

    public PriceHandler()
    {
        Reset();
    }

    /// <summary>
    /// 重置
    /// </summary>
    public void Reset()
    {
        _totalQuantity = 0;
        _totalMoneyforshipping = 0.00M;
        _totalMoneyforpayment = 0.00M;
        _totalDiscount = 0.00M;
        _total = 0.00M;
        _totalPoints = 0;
    }

    /// <summary>
    /// 从购物车获取所购商品信息
    /// </summary>
    /// <param name="cart"></param>
    public void Run(View_ShoppingCart cart)
    {
        _cart = cart;

        _adjustment = 0.00M;
        _price = 0.00M;
        _discount = 0.00M;

        #region adjustment
        /*这里解析购物者所挑选的属性的xml文档
        *此xml文档的形式为
        *  <attributeXml>
        *      <add valueId="2" adjustment="0.00">xxx</add>
        *      <add valueId="3" adjustment="0.00">xxx</add>
        *      ...
        *  </attributeXml>
        */

        Regex regx = new Regex(@"adjustment=""([-\d\.]+)""");
        MatchCollection mats = regx.Matches(_cart.AttributesXml);
        foreach (Match m in mats)
        {
            var group = m.Groups[1];
            _adjustment += Convert.ToDecimal(group.Value);
        }

        #endregion

        #region last price

        DateTime now = DateTime.Now;

        if (_cart.EnableSpecialPrice
            && (_cart.SpecialPriceStartDate <= now && now <= _cart.SpecialPriceEndDate))
        {
            _price = _cart.UnitSpecialPrice;
        }
        else
        {
            if (_cart.EnableVipPrice)
                _price = _cart.UnitVipPrice;
            else
                _price = _cart.UnitPrice;
        }

        _price += _adjustment;//将因商品属性导致的价差考虑进去

        #endregion

        #region discount

        if (_cart.UseDiscount)
        {
            //DiscountPercentage是指折后价相对于折前价的百分比
            if (_cart.UsePercentage)
                _discount = StandardPrice * (1 - _cart.DiscountPercentage / 100);
            else
                _discount = _cart.DiscountAmount;
        }

        #endregion

        _subtotal = StandardPrice * _cart.Quantity;
        _subtotalDiscount = DiscountAmount * _cart.Quantity;
        _subtotalPoints = _cart.Points * _cart.Quantity;
        _subtotalMoneyforshipping = 0.00M;
        _subtotalMoneyforpayment = 0.00M;

        _total += _subtotal;
        _totalDiscount += _subtotalDiscount;
        _totalMoneyforpayment += _subtotalMoneyforshipping;
        _totalMoneyforshipping += _subtotalMoneyforpayment;
        _totalPoints += _subtotalPoints;
        _totalQuantity += _cart.Quantity;
    }

    #region per

    /// <summary>
    /// 最后的售价
    /// 优先判断是否使用特价，然后判断是否使用vip价,并入价格微调
    /// 这个是售价
    /// </summary>
    public decimal StandardPrice
    {
        get
        {
            return _price;
        }
    }

    /// <summary>
    /// (单个商品)价格调整
    /// </summary>
    public decimal Adjustment
    {
        get { return _adjustment; }
    }

    /// <summary>
    /// 单个商品的折扣量
    /// </summary>
    public decimal DiscountAmount
    {
        get
        {
            return _discount;
        }
    }

    #endregion

    #region sub
    /// <summary>
    /// 总价(x数量)
    /// </summary>
    public decimal SubTotal
    {
        get
        {
            return _subtotal;
        }
    }

    /// <summary>
    /// 总折扣(x数量)
    /// </summary>
    public decimal SubTotalDiscountAmount
    {
        get
        {
            return _subtotalDiscount;
        }
    }

    /// <summary>
    /// 总积分(x数量)
    /// </summary>
    public int SubTotalPoints
    {
        get
        {
            return _subtotalPoints;
        }
    }

    /// <summary>
    /// 运费（运输）
    /// </summary>
    public decimal SubTotalMoneyforshipping { get { return _subtotalMoneyforshipping; } }

    /// <summary>
    /// 手续费(网上付款)
    /// </summary>
    public decimal SubTotalMoneyforpayment { get { return _subtotalMoneyforpayment; } }

    #endregion

    #region total
    /// <summary>
    /// 最终计算出来的总价不包含折扣、运费、支付费
    /// 因此在计算应付额时需要作一下加减运算
    /// </summary>
    public decimal Total { get { return _total; } }
    public decimal TotalDiscountAmount { get { return _totalDiscount; } }
    public int TotalPoints { get { return _totalPoints; } }
    public decimal TotalMoneyforshipping { get { return _totalMoneyforshipping; } }
    public decimal TotalMoneyforpayment { get { return _totalMoneyforpayment; } }
    public int TotalQuantity { get { return _totalQuantity; } }
    #endregion

}