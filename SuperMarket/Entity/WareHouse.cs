using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Entity
{
    class WareHouse
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
}
