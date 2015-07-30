using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderItemGuid { get; set; }

        [NtAttribute.FKConstraint("Order", "Id")]
        public int OrderId { get; set; }

        public int GoodsId { get; set; }

        public int Quantity { get; set; }

        /// <summary>
        /// 单产品折扣
        /// </summary>
        public decimal DiscountAmount { get; set; }
        /// <summary>
        /// 单商品价调
        /// </summary>
        public decimal Adjustment { get; set; }
        /// <summary>
        /// 最终单价(已计算价调)
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 订单项折扣
        /// </summary>
        public decimal SubTotalDiscount { get; set; }
        /// <summary>
        /// 订单项总价
        /// </summary>
        public decimal SubTotal { get; set; }
        /// <summary>
        /// 订单项运费
        /// </summary>
        public decimal SubMoneyforshipping { get; set; }
        /// <summary>
        /// 订单项支付手续费
        /// </summary>
        public decimal SubMoneyforpayment { get; set; }
        /// <summary>
        /// 订单项赠送的积分
        /// </summary>
        public decimal SubTotalPoints { get; set; }

        public string AttributesXml { get; set; }

        public string Note { get; set; }

    }
}
