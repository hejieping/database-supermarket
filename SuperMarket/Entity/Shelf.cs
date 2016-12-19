using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Entity
{
    class Shelf
    {
        public Shelf()
        {
            employee = new Employee();
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
}
