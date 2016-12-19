using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Entity
{
    class Type
    {
        public Type()
        {
            employee = new Employee();
        }
        private int typeID;
        private string name;
        private Employee employee;//商品类别对应的负责采购的员工
        public int TypeID { get { return typeID; } set { typeID = value; } }
        public string Name { get { return name; } set { name = value; } }
        public Employee Employee { get { return employee; } set { employee = value; } }
    }
}
