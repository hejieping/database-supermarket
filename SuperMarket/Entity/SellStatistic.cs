using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Entity
{
    class SellStatistic
    {
        private Good good;
        private int sellquantity;
        public Good SellGood
        {
            get { return good; }
            set { good = value; }
        }
        public int SellQuantity
        {
            get { return sellquantity; }
            set { sellquantity = value; }
        }
    }
}
