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
using SuperMarket.UIWpf.Manager;
using SuperMarket.Entity;
using SuperMarket.DataAccessLayer;
using SuperMarket.DataAccess;
using System.Data;
namespace SuperMarket.UIWpf.Manager
{
    /// <summary>
    /// MainManager.xaml 的交互逻辑
    /// </summary>
    public partial class MainManager : Window
    {
        Employee person;
        //public mainmanager()
        //{
        //    initializecomponent();
        //    igooddao dao = new gooddao();
        //    dataset set = dao.vaguegetgood("");
        //}
        void initState()
        {
            nameLabel.Content = person.Name;
            DateTime now = new DateTime();
            now = DateTime.Now;
            MessageBox.Show(now.ToString());
        }
        public MainManager(Employee e)
        {
            InitializeComponent();
            person = e;
            initState();
        }

        private void employeeInfo_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("OK");
            contentControl.Content = new EmployeeInfo();
            
            //frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            //frame.Navigate(new Uri("/UIWpf/Manager/EmployeeInfo.xaml", UriKind.Relative));
        }

        private void goodKind_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new GoodKind();
        }

        private void goodDetail_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new GoodInfo();
        }

        private void moveRecord_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new MoveRecord();
        }

        private void buyRecord_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new BuyRecord();
        }

        private void sellRecord_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new SellRecord();
        }

        private void warehouseInfo_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new WareHouseInfo();
        }

        private void shelfInfo_Click(object sender, RoutedEventArgs e)
        {
           contentControl.Content = new ShelfInfo();
        }
    }
}
