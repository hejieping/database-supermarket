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

using System.Data;
using SuperMarket.DataAccess;
using SuperMarket.DataAccessLayer;
using SuperMarket.Entity;
namespace SuperMarket.UIWpf.Manager
{
    /// <summary>
    /// EmployeeInfo.xaml 的交互逻辑
    /// </summary>
    public partial class EmployeeInfo : UserControl
    {
        private IEmployeeDao dao = new EmployeeDao();
        public EmployeeInfo()
        {
            InitializeComponent();
            getAllEmployee();
        }

        private void Search_Copy1_Click(object sender, RoutedEventArgs e)
        {
            IEmployeeDao ed = new EmployeeDao();
            string s = textBox1.Text;
            int id;
            dataGrid.IsReadOnly = true;
            if (int.TryParse(s, out id))
            {
                DataSet ds = ed.getEmployee(id);
                dataGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            else
            {
                MessageBox.Show("请输入数字。");
            }
        }

        private void Search_Copy_Click(object sender, RoutedEventArgs e)
        {
            IEmployeeDao ed = new EmployeeDao();
            string s = textBox2.Text;

            DataSet ds = ed.getEmployee(s);
            dataGrid.ItemsSource = ds.Tables[0].DefaultView;
        }

        private void dataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void getAllEmployee()
        {
            dataGrid.ItemsSource = dao.getAllEmployees().Tables[0].DefaultView;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                IEmployeeDao ed = new EmployeeDao();
                DataRowView mySelectedElement = (DataRowView)dataGrid.SelectedItem;
                string str_id = mySelectedElement.Row[0].ToString();
                int id;
                if (int.TryParse(str_id, out id))
                {

                    Employee employee = ed.getSingleEmployee(id);
                    textname.Text= employee.Name;
                    contact.Text = employee.ContactMethod;
                    identity.Text = employee.Identity;
                    salary.Text = employee.Salary.ToString();
                    address.Text = employee.Address;
                    account.Text = employee.Account;
                    photo.Source = employee.Photo;
                }
                else
                {
                    MessageBox.Show("无法解析为数字。");
                    return;
                }
            }
            catch(Exception exx)
            {
                return;
            }
        }
    }
}
