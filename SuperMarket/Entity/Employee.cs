using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
namespace SuperMarket.Entity
{
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
        private BitmapImage photo;
        public int EmployeeID { get { return employeeID; }  set { employeeID = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string ContactMethod { get { return contactMethod; } set { contactMethod = value; } }
        public decimal Salary { get { return salary; } set { salary = value; } }
        public string Identity { get { return identity; } set { identity = value; } }
        public string Account { get { return account; } set { account = value; } }
        public string Password { get { return password; } set { password = value; } }
        public  BitmapImage  Photo { get { return photo; } set { photo = value; } }
        public string Address { get { return address; } set { address = value; } }
        public void test()
        {
            
        }

    }
}
