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

namespace SuperMarket.UIWpf.Mover
{
    /// <summary>
    /// MoverWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MoverWindow : Window
    {
        private Employee employee;
        public MoverWindow()
        {
            InitializeComponent();
        }

        public MoverWindow(Employee e)
        {
            InitializeComponent();
            employee = e;
        }

        private void startToMove_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new StartMove();
        }

        private void MoveInfo_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new MoveRecord();
        }

        private void goodLackInfo_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ShelfLackGood();
        }

        private void personInfo_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new PersonalInfo(employee);
        }
    }
}
