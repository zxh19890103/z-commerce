using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model.View
{
    public class View_Order : Order, IView
    {
        public string PaymentMethod { get; set; }
        public string ShippingMethod { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public int MaxID { get; set; }
        public decimal AmountPayable { get; set; }

        public string GetScript()
        {
            return
                " Select *," +
                " (Select max(ID) From ##Order##) As MaxID," +
                " (Select Top 1 Name From ##PaymentMethod## Where Id=PaymentMethodId) As PaymentMethod," +
                " (Select Top 1 Name From ##ShippingMethod## Where Id=ShippingMethodId) As ShippingMethod," +
                " (OrderTotal-OrderTotalDiscount-RefundedAmount+ShippingExpense+CommissionCharge) As AmountPayable" +
                " From ##Order## As T0" +
                " Left Join" +
                " (Select Id As TempID,[Name],[Phone],[Mobile],[Email],[Address],[Zip] From ##Customer_Consignee##) As T1" +
                " On T0.CustomerConsigneeId=T1.TempID";
        }
    }
}
