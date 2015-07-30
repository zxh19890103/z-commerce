using Nt.Model.NtAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public class Order : BaseEntity, IDeletable
    {
        public Guid OrderGuid { get; set; }

        [FKConstraint("Customer", "Id")]
        public int CustomerId { get; set; }
        public string CustomerIp { get; set; }

        public int CustomerConsigneeId { get; set; }

        public int Status { get; set; }
        public int ShippingStatus { get; set; }
        public int PaymentStatus { get; set; }
        public int PaymentMethodId { get; set; }
        public int ShippingMethodId { get; set; }

        /// <summary>
        /// 不算折扣的总计
        /// </summary>
        public decimal OrderTotal { get; set; }
        /// <summary>
        /// 总的折扣
        /// </summary>
        public decimal OrderTotalDiscount { get; set; }
        /// <summary>
        /// 退款
        /// </summary>
        public decimal RefundedAmount { get; set; }

        [FieldSize(100)]
        public string CardType { get; set; }
        [FieldSize(256)]
        public string CardName { get; set; }
        [FieldSize(100)]
        public string CardNumber { get; set; }

        public DateTime PaidDate { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Note { get; set; }

        public string OrderMessage { get; set; }

        /// <summary>
        /// 配送运费
        /// </summary>
        public decimal ShippingExpense { get; set; }
        /// <summary>
        /// 购物手续费
        /// </summary>
        public decimal CommissionCharge { get; set; }

    }
}
