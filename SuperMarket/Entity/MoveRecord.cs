using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperMarket.Entity
{
    class MoveRecord
    {
        public MoveRecord() { good_remove = new Dictionary<Good, int>(); employee = new Employee();warehouse = new WareHouse(); }
        private int removeID;
        private Dictionary<Good, int> good_remove;//该次移货的所有商品
        private DateTime removeTime;//移货时间
        private Employee employee;//移货员
        private WareHouse warehouse;//移货仓库
        private int removeTotal;
        public int RemoveID
        {
            get { return removeID; }
            set { removeID = value; }
        }
        public Dictionary<Good, int> Good_Remove
        {
            get { return good_remove; }
            private set { }
        }

        public int RemoveTotal
        {
            get
            {
                return good_remove.Count>removeTotal?Good_Remove.Count:removeTotal;
            }
             set
            {
                removeTotal = value;
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
}
