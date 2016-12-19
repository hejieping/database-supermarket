using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Entity
{
    class Good
    {
        public Good()
        {
            shelf = new Shelf();
            type = new Type();
        }
        private int goodID;
        private string name;
        private decimal price;
        private decimal discount = 1;
        private Shelf shelf;//商品所在的货架
        private Type type;//商品的类别
        private int amount_in_shelf;
        private int total_in_warehouse;
        private int shelfMin;
        private int warehouseMin;
        public decimal sellprice { get { return sellprice; } private set { } }
        private decimal piecePrice;//采购员采购的该商品时的价格
        public int GoodID { get { return goodID; } set { goodID = value; } }
        public string Name { get { return name; } set { name = value; } }
        public decimal Price { get { return price; } set { price = value; } }
        public decimal Discount { get { return discount; } set { discount = value; } }
        public Shelf Shelf { get { return shelf; } set { shelf = value; } }
        public Type Type { get { return type; } set { type = value; } }
        public int Amount_in_shelf { get { return amount_in_shelf; } set { amount_in_shelf = value; } }
        public int Total_in_warehouse { get { return total_in_warehouse; } set { total_in_warehouse = value; } }
        public int ShelfMin { get { return shelfMin; } set { shelfMin = value; } }
        public decimal PiecePrice { get { return piecePrice; } set { piecePrice = value; } }
        public int WareHouseMin { get { return warehouseMin; } set { warehouseMin = value; } }
        public override bool Equals(object obj)
        {

            if (obj is Good)
            {
                Good good = obj as Good;
                if (this.goodID == good.goodID)
                    return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.goodID * this.goodID;
        }
    }
}
