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
using System.Data;

namespace SuperMarket.UIWpf.Manager
{
    /// <summary>
    /// GoodInfo.xaml 的交互逻辑
    /// </summary>
    public partial class GoodInfo : UserControl
    {
        private IGoodDao dao;
        int gid = -1;
        public GoodInfo()
        {
            InitializeComponent();
            dao = new GoodDao();
            dataGrid.SelectionMode = DataGridSelectionMode.Single;
            dataGridDetail.SelectionMode = DataGridSelectionMode.Single;
            dataGridDetail.IsReadOnly = true;
            dataGrid.IsReadOnly = true;
            getAllGoodInfo();
        }

        //在商品简略信息点击后，在详细信息框中显示商品详细信息
 /*       private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            DataRowView mySelectedElement = (DataRowView)dataGrid.SelectedItem;  //获取datagrid选择的对象
            string result = mySelectedElement.Row[0].ToString();
            int i;
            try 
            {
                i = int.Parse(result);
                //需要自己在数据接口里面添加getSingleGoodDataSet(i)函数
               
            }
            catch(Exception ex)
            {
                //      MessageBox.Show("无法解析为数");
                return;
            }
            dataGridDetail.ItemsSource = pd.getSingleGoodInfoDataSet(i).Tables[0].DefaultView;
        }*/
        private void getAllGoodInfo()
        {
            dataGrid.ItemsSource = dao.getAllGoodBriefInfo().Tables[0].DefaultView;
        }
        private void Search_Copy1_Click(object sender, RoutedEventArgs e)
        {
            string s = goodInfo.Text;
            int i;
            try
            {
              i =   int.Parse(s);
            }
            catch(Exception ex)
            {
                MessageBox.Show("请输入数字!");
                return;
            }
            dataGrid.ItemsSource = dao.getSingleGoodDataSet(i).Tables[0].DefaultView;
        }

        private void Search_Copy2_Click(object sender, RoutedEventArgs e)
        {
            string s = goodInfo1.Text;

            MessageBox.Show("请输入数字!");

            DataSet ds = dao.vagueGetGood(s);
            dataGrid.ItemsSource = ds.Tables[0].DefaultView;
        }

        private void dataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string result = "0";
            try
            {
                var a = this.dataGrid.SelectedItem;
                var b = a as DataRowView;
                result = b.Row[0].ToString();
            }
            catch(Exception exx)
            {
                return;
            }
            int i;
            try
            {
                i = int.Parse(result);
                //需要自己在数据接口里面添加getSingleGoodDataSet(i)函数

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR!");
                return;
            }
            if (i == gid) return;
            gid = i;
            dataGridDetail.ItemsSource = dao.getSingleGoodInfoDataSet(i).Tables[0].DefaultView;
        }
    }
}
