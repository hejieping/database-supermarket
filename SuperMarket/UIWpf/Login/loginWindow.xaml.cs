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
using System.Windows.Shapes;
using SuperMarket.UIWpf.Buyer;
using SuperMarket.UIWpf.Manager;
using SuperMarket.UIWpf.Mover;
using SuperMarket.UIWpf.Seller;
using SuperMarket.DataAccess;
using SuperMarket.DataAccessLayer;
using SuperMarket.Entity;

namespace SuperMarket
{
    /// <summary>
    /// loginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class loginWindow : Window
    {

        public loginWindow()
        {
            InitializeComponent();
            //ICustomerDao customer = new CustomerDao();
            //customer.getNormalCustomer();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            string name = username.Text;
            string passwd = password.Password;
            IEmployeeDao dao = new EmployeeDao();
            Employee employee = dao.getEmployeeWithPasswd(name, passwd);
            if (employee == null)
            {
                MessageBox.Show("账号名或密码不正确");
                return;
            }
            string identity = employee.Identity;
            if (identity == "经理")
            {
                MainManager manager = new MainManager(employee);
                manager.Show();
            }
            else if (identity == "采购员")
            {
                BuyerWindow buyer = new BuyerWindow(employee);
                buyer.Show();
            }
            else if (identity == "收银员")
            {
                SellerWindow sell = new SellerWindow(employee);
                sell.Show();
            }
            else if (identity == "移货员")
            {
                MoverWindow mover = new MoverWindow(employee);
                mover.Show();
            }

            this.Close();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
