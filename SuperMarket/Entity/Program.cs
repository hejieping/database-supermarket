using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace Project_1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main2()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //        Application.Run(new frmLogin());
            Bill bill = new Bill();
            Good good = new Good(); good.GoodID = 1;
            Good good1 = new Good(); good1.GoodID = 1;
            bill.addGood(good, 5);
            bill.removeGood(good1, 3);
        }
        //员工
        public class Employee
        {
            public Employee()
            {

            }
            private int employeeID;
            private string name;
            private string contactMethod;
            private string address;
            private decimal salary;
            private string identity;
            private string account;
            private string password;
            
            private Image photo;
            public int EmployeeID { get { return employeeID; } private set { employeeID = value; } }
            public string Name { get { return name; } set { name = value; } }
            public string ContactMethod { get { return contactMethod; } set { contactMethod = value; } }
            public decimal Salary { get { return salary; } set { salary = value; } }
            public string Identity { get { return identity; } set { identity = value; } }
            public string Account { get { return account; } set { account = value; } }
            public string Password { get { return password; } set { password = value; } }
            public Image image { get { return photo; } set { photo = value; } }
            public string Address { get { return address; } set { address = value; } }
        }

        //货架
        public class Shelf
        {
            public Shelf()
            {

            }
            private int shelfID;
            private string shelfName;
            private string location;
            private Employee employee;//货架对应的负责员工
            public int SelfID { get { return shelfID; } set { shelfID = value; } }

            public string ShelfName { get { return shelfName; } set { shelfName = value; } }
            public string Location { get { return location; } set { location = value; } }
            public Employee Employee { get { return employee; } set { employee = value; } }
        }

        public class Type
        {
            public Type()
            {

            }
            private int typeID;
            private string name;
            private Employee employee;//商品类别对应的负责采购的员工
            public int TypeID { get { return typeID; } set { typeID = value; } }
            public string Name { get { return name; } set { name = value; } }
            public Employee Employee { get { return employee; } set { employee = value; } }
        }
        public class WareHouse
        {
            public WareHouse()
            {

            }
            private int warehouseID;
            private string name;
            private string address;
            public int WareHouseID { get { return warehouseID; } set { warehouseID = value; } }
            public string Name { get { return name; } set { name = value; } }
            public string Address { get { return address; } set { address = value; } }
        }

        //顾客
        public class Customer
        {
            public Customer()
            {

            }
            private int customerID;
            private DateTime regdate;
            private decimal membershippoint;
            private decimal discount_deserve;//会员应享有的折扣
            public int CustomerID
            {
                get { return customerID; }
                private set { customerID = value; }
            }
            public DateTime RegDate { get { return regdate; } set { regdate = value; } }
            public decimal MemberShipPoint { get { return membershippoint; } set { membershippoint = value; } }
            public decimal Discount_deserve { get { return discount_deserve; } set { discount_deserve = value; } }
        }

        //商品
        public class Good
        {
            public Good()
            {

            }
            private int goodID;
            private string name;
            private decimal price;
            private decimal discount;
            private Shelf shelf;//商品所在的货架
            private Type type;//商品的类别
            private int amount_in_shelf;
            private int total_in_warehouse;
            private int shelfMin;
            private int warehouseMin;

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

        //账单
        public class Bill
        {

            private int billID;
            private Customer customer;//账单对应的顾客
            private DateTime purchaseingDate;
            private Employee employee;//账单对应的收银员

            private Dictionary<Good, int> goodsells;//账单包含的商品以及数量
            public int BillID { get { return billID; } set { billID = value; } }
            public Customer Customer { get { return customer; } set { customer = value; } }
            public DateTime PurchaseingDate { get { return purchaseingDate; } set { purchaseingDate = value; } }
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
                    total = total * customer.Discount_deserve;//乘以会员应享有的折扣
                    return total;
                }
                private set
                {

                }
            }
            public int total_amount
            {
                get
                {
                    return goodsells.Count;
                }
                private set
                {

                }
            }
            public Bill()
            {
                goodsells = new Dictionary<Good, int>();
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


        //采购单
        public class Procurement
        {
            public Procurement()
            {
                good_procurement = new Dictionary<Good, int>();
            }
            private int procurementID;
            private Employee employee;//该次采购对应的员工
            private WareHouse warehouse;//该次采购存入的仓库
            private DateTime storageTime;
            private Dictionary<Good, int> good_procurement; //采购单包含的商品数量
            public int ProcurementID { get { return procurementID; } set { procurementID = value; } }
            public Employee Employee { get { return employee; } set { employee = value; } }
            public WareHouse WareHouse { get { return warehouse; } set { warehouse = value; } }
            public DateTime StorageTime { get { return storageTime; } set { storageTime = value; } }

            public decimal totalMoney//采购总价
            {
                get
                {
                    decimal total = 0;
                    //遍历采购单内的每件商品
                    foreach (KeyValuePair<Good, int> entry in good_procurement)
                    {
                        Good good = entry.Key;
                        int proc_amount = entry.Value;
                        total += good.PiecePrice * proc_amount;//某件商品的采购总价（采购数量*采购单价）
                    }
                    return total;
                }
                private set
                {

                }
            }

            public int PieceAmount
            {
                get { return good_procurement.Count; }
            }

            //向采购单添加某件商品
            public void addGood(Good good, int amount = 1)
            {
                if (good_procurement.Keys.Contains(good))
                {
                    good_procurement[good] += amount;
                    return;
                }
                good_procurement.Add(good, amount);
            }

            //从采购单中移除某件商品，amount表示数量
            public bool removeGood(Good good, int amount)
            {
                int total;
                try
                {
                    total = good_procurement[good];
                }
                catch
                {
                    return false;
                }
                if (total < amount)
                    return false;
                else if (total == amount)
                {
                    good_procurement.Remove(good);
                    return true;
                }
                else
                {
                    good_procurement[good] -= amount;
                    return true;
                }
            }

            //移货
            public class Remove
            {
                private int removeID;
                private Dictionary<Good, int> good_remove;//该次移货的所有商品
                private DateTime removeTime;//移货时间
                private Employee employee;//移货员
                private WareHouse warehouse;//移货仓库
                public int RemoveID
                {
                    get { return removeID; }
                    set { removeID = value; }
                }
                public int RemoveTotal
                {
                    get
                    {
                        return good_remove.Count;
                    }
                    private set
                    {

                    }
                }
                public DateTime RemoveTime
                {
                    get
                    {
                        return removeTime;
                    }
                    set
                    {
                        removeTime = value;
                    }
                }

                public Employee Employee
                {
                    get
                    {
                        return employee;
                    }
                    set
                    {
                        employee = value;
                    }
                }
                public WareHouse WareHouse
                {
                    get
                    {
                        return warehouse;
                    }
                    set
                    {
                        warehouse = value;
                    }
                }

                //向移货记录中添加一件商品，amount表示数量
                public void addGood(Good good, int amount = 1)
                {
                    if (good_remove.Keys.Contains(good))
                    {
                        good_remove[good] += amount;
                        return;
                    }
                    good_remove.Add(good, amount);
                }

                //从采购单中移除某件商品，amount表示数量
                public bool removeGood(Good good, int amount)
                {
                    int total;
                    try
                    {
                        total = good_remove[good];
                    }
                    catch
                    {
                        return false;
                    }
                    if (total < amount)
                        return false;
                    else if (total == amount)
                    {
                        good_remove.Remove(good);
                        return true;
                    }
                    else
                    {
                        good_remove[good] -= amount;
                        return true;
                    }
                }


            }
            public class SellStatistic
            {
                private Good good;
                private int  sellquantity;
                public Good SellGood
                {
                    get {return good ;}
                    set {good = value ;}
                }
                public int SellQuantity
                {
                    get { return sellquantity; }
                    set { sellquantity = value; }
                }
            }
        }
    }
}
