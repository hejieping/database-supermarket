using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SuperMarket.DataAccess;
using SuperMarket.DataAccessLayer;
using SuperMarket.Entity;

namespace SuperMarket.UIWpf
{
    /// <summary>
    /// PersonalInfo.xaml 的交互逻辑
    /// </summary>
    public partial class PersonalInfo : UserControl
    {
        private Employee employee;
        public PersonalInfo(Employee e)
        {
            InitializeComponent();
            employee = e;
            init();
        }
        private void init()
        {
            tname.Text = employee.Name;
            tid.Text = employee.EmployeeID.ToString();
            tcontact.Text = employee.ContactMethod;
            taddress.Text = employee.Address;
            tsalary.Text = employee.Salary.ToString();
            taccount.Text = employee.Account;
            photo.Source = employee.Photo;
        }
    }
}
