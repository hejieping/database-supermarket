using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Entity
{
    class Bill
    {
        private int billID;
        private Customer customer;//账单对应的顾客
        private DateTime purchaseingDate;
        private Employee employee;//账单对应的收银员
        private decimal total_price;
        private int total_amount;
        private Dictionary<Good, int> goodsells;//账单包含的商品以及数量
        public Dictionary<Good,int> GoodSells { get { return goodsells; } set { goodsells = value; } }
        public int BillID { get { return billID; } set { billID = value; } }
        public Customer Customer { get { return customer; } set { customer = value; } }
        public DateTime PurchaseingDate { get { return purchaseingDate; } set { purchaseingDate = value; } }
        public Employee Employee { get { return employee; } set { employee = value; } }
        public decimal TotalPrice//该账单的总金额
        {
            get
            {
                decimal total = 0;
                //遍历账单内的每件商品
                foreach (KeyValuePair<Good, int> entry in goodsells)
                {
                    Good good = entry.Key;
                    int sell_amount = entry.Value;
                    total += good.Price * good.Discount * sell_amount;//某件商品的总价（单价*数量*折扣）
                }
                if(customer.Discount_deserve!=0)
                    total = total * customer.Discount_deserve;//乘以会员应享有的折扣
                if (total_price > total) total = total_price;
                total = decimal.Round(total,2);
                return total;
            }
            set
            {
                total_price = value;
            }
        }
        public int Total_amount
        {
            get
            {
                return goodsells.Count>total_amount?goodsells.Count:total_amount;
            }
             set
            {
                total_amount = value;
            }
        }
        public Bill()
        {
            goodsells = new Dictionary<Good, int>();
            customer = new Customer();
            purchaseingDate = new DateTime();
            employee = new Employee();
        }
        //添加一件商品
        public void addGood(Good good, int amount = 1)
        {
            if (goodsells.Keys.Contains(good))
            {
                goodsells[good] += amount;
                return;
            }
            goodsells.Add(good, amount);
        }
        //删除某种商品，删除数量为amount
        public bool removeGood(Good good, int amount = 1)
        {
            int total;
            try
            {
                total = goodsells[good];
            }
            catch
            {
                return false;
            }
            if (total < amount)
                return false;
            else if (total == amount)
            {
                goodsells.Remove(good);
                return true;
            }
            else
            {
                goodsells[good] -= amount;
                return true;
            }
        }
    }
}
