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
    /// MoveRecord.xaml 的交互逻辑
    /// </summary>
    public partial class MoveRecord : UserControl
    {
        private IMoveRecordDao dao;

        public MoveRecord()
        {
            InitializeComponent();
            init();
            dataGrid.SelectionMode = DataGridSelectionMode.Single;
            dataGrid.IsReadOnly = true;
        }
        private void init()
        {
            dao = new MoveRecordDao();
            getAllMoves();
        }
        private void Search_Copy2_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            try
            {
                id = int.Parse(moveIDBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("请输入正确的移货号");
                return;
            }
            DataSet set = dao.getMove(id);
            dataGrid.ItemsSource = set.Tables[0].DefaultView;
        }

        private void Search_Copy1_Click(object sender, RoutedEventArgs e)
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
            dataGrid.ItemsSource = dao.getAllMoveRecords(StartTime, EndTime).Tables[0].DefaultView;
        }

        private void Search_Copy_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            try
            {
                id = int.Parse(moveIDBox1.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("请输入正确的员工ID");
                return;
            }
            DataSet set = dao.getMyRemoveRecord(id);
            dataGrid.ItemsSource = set.Tables[0].DefaultView;
        }

        private void getAllMoves()
        {
            dataGrid.ItemsSource = dao.getAllMoveRecords().Tables[0].DefaultView;
        }

        private void dataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = 0;
            try
            {
             var a = this.dataGrid.SelectedItem;
            var b = a as DataRowView;
            string result = b.Row[0].ToString();
            
            index = int.Parse(result);
            }
            catch (Exception ex)
            { return; }
             DataSet set  = dao.getDetailedMoveInfo(index);
            dataGridDetail.ItemsSource = set.Tables[0].DefaultView;
        }
    }
}
