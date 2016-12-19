using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Entity
{
    class Procurement
    {
        public Procurement()
        {
            good_procurement = new Dictionary<Good, int>();
            Employee = new Employee();
            WareHouse = new WareHouse();
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
        public Dictionary<Good, int> Good_Procurement { get { return good_procurement; } private set { } }
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
    }
}
