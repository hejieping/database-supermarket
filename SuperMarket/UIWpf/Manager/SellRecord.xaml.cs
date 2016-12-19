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
    /// SellRecord.xaml 的交互逻辑
    /// </summary>
    public partial class SellRecord : UserControl
    {
        
        private IBillDao billdao;
        public SellRecord()
        {
            InitializeComponent();
            init();
        }
        private void init()
        {
            billdao = new BillDao();
            dataGrid2.SelectionMode = DataGridSelectionMode.Single;
            dataGrid2.IsReadOnly =true;
            getAllSell();
        }
        public void getAllSell()
        {
            DataSet set = billdao.getAllBills();
            dataGrid2.ItemsSource = set.Tables[0].DefaultView;
        }
        private void Search_Copy3_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            try
            {
                id = int.Parse(moveIDBoxSell.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("请输入正确的销售单号");
                return;
            }
            DataSet set = billdao.getBillInfo(id);
            dataGrid2.ItemsSource = set.Tables[0].DefaultView;
        }

        private void Search2_Click(object sender, RoutedEventArgs e)
        {
            DateTime StartTime, EndTime;
            try
            {
                StartTime = DateTime.Parse(starttime.SelectedDate.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("请输入开始日期");
                return;
            }

            try
            {
                EndTime = DateTime.Parse(endtime.SelectedDate.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("请输入结束日期");
                return;
            }
            dataGrid2.ItemsSource = billdao.getAllBills(StartTime, EndTime).Tables[0].DefaultView;
        }

        private void Search_CopySell_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            try
            {
                id = int.Parse(moveIDBox2.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("请输入正确的员工ID");
                return;
            }
            DataSet set = billdao.getAllBills(new DateTime(), DateTime.Now, id);
            dataGrid2.ItemsSource = set.Tables[0].DefaultView;
        }

        private void dataGrid2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

       private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
      //      getAllSell();
        }

        private void dataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = 0;
            try
            {
                var a = this.dataGrid2.SelectedItem;
                var b = a as DataRowView;
                string result = b.Row[0].ToString();
                index = int.Parse(result);
            }
            catch (Exception ex)
            { return; }
            DataSet set = billdao.getBillDetailedInfo(index);
            dataGridDetail2.ItemsSource = set.Tables[0].DefaultView;
        }
    }
}
