using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Entity
{
    public  class Customer
    {
        public Customer()
        {
            discount_deserve = 1;
            customerID = 9999;
        }
        private int customerID;
        private DateTime regdate;
        private decimal membershippoint;
        private decimal discount_deserve;//会员应享有的折扣
        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }
        public DateTime RegDate { get { return regdate; } set { regdate = value; } }
        public decimal MemberShipPoint { get { return membershippoint; } set { membershippoint = value; } }
        public decimal Discount_deserve { get { return discount_deserve; } set { discount_deserve = value; } }
    }
}
