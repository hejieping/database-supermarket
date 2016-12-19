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
using SuperMarket.Entity;
namespace SuperMarket.UIWpf.Buyer
{
    /// <summary>
    /// BuyerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BuyerWindow : Window
    {
        Employee person;
        public BuyerWindow()
        {
            InitializeComponent();
         
        }
        public BuyerWindow(Employee e)
        {
            InitializeComponent();
            person = e;
        }
        private void startToBuy_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new StartBuy();
        }

        private void personInfo_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new PersonalInfo(person);
        }

        private void goodLackInfo_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new LackGood(person.EmployeeID);
        }
    }
}
