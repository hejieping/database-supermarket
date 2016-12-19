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
using SuperMarket.Entity;
using SuperMarket.DataAccess;
using SuperMarket.DataAccessLayer;

namespace SuperMarket.UIWpf.Buyer
{
    /// <summary>
    /// StartBuy.xaml 的交互逻辑
    /// </summary>
    public partial class StartBuy : UserControl
    {
        DataTable dt;
        Procurement pro;
        public StartBuy()
        {

            InitializeComponent();
            initDataGrid();
        }
        private void initDataGrid()
        {
            pro = new Procurement();
            dt = new DataTable();
            DataColumn cgoodId = new DataColumn("商品编号", typeof(int));
            DataColumn cgoodName = new DataColumn("商品名称", typeof(string));
            DataColumn cgoodPrice = new DataColumn("商品进价", typeof(decimal));
            DataColumn cgoodNumber = new DataColumn("商品数量", typeof(int));
            //DataColumn wareHouseID = new DataColumn("仓库编号", typeof(int));
            dt.Columns.Add(cgoodId);
            dt.Columns.Add(cgoodName);
            dt.Columns.Add(cgoodPrice);
            dt.Columns.Add(cgoodNumber);
        }


        private void idSearch_Click(object sender, RoutedEventArgs e)
        {
            IGoodDao pd = new GoodDao();
            string s = idBox.Text;
            int i;
            if (int.TryParse(s, out i))
            {
                //需调用返回dataset 的getSingleGoodDataSet函数
                DataSet ds = pd.getSingleGoodDataSet(i);
                dataGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            else
            {
                MessageBox.Show("请输入数字。");
            }
        }

        private void nameSearch_Click(object sender, RoutedEventArgs e)
        {
            IGoodDao pd = new GoodDao();
            string s = nameBox.Text;
            //此处是调用商品名称模糊搜索，把前端的商品类别label改成商品名称
            DataSet ds = pd.vagueGetGood(s);
            dataGrid.ItemsSource = ds.Tables[0].DefaultView;
        }

        private void addGoodButton_Click(object sender, RoutedEventArgs e)
        {
            IGoodDao pd = new GoodDao();
            DataRowView mySelectedElement = (DataRowView)dataGrid.SelectedItem;  //获取datagrid选择的对象
            string s = mySelectedElement.Row[0].ToString();
            int i;
             if (int.TryParse(s, out i))
             {

                //第二个datagrid添加一行数据，
                Good good = pd.getSingleGood(i);
                 Good temp = new Good();
                 int GoodID = good.GoodID;
                 string Name = good.Name;
                 decimal Price = good.Price;
                 int number = 1;

                 DataRow dr = dt.NewRow();
                 dr[0] = GoodID;
                 dr[1] = Name;
                 dr[2] = Price;
                 dr[3] = number;

                for (int m = 0; m < dt.Rows.Count; i++)
                {
                    if (i == Convert.ToInt32(dt.Rows[m][0]))
                    {
                        dr[3] = Convert.ToInt32(dt.Rows[m][3]) + number;
                        dt.Rows.RemoveAt(m);
                        break;
                    }
                }
                dt.Rows.Add(dr);
                dataGridDetail.ItemsSource = dt.DefaultView;
            }
             else
             {
                 MessageBox.Show("请输入数字。");
             }

        }

        private void deleteGoodButton_Click(object sender, RoutedEventArgs e)
        {
            IGoodDao pd = new GoodDao();
           // DataRowView mySelectedElement = (DataRowView)dataGridDetail.SelectedItem;  //获取datagrid选择的对象
            int idSelected = dataGrid.SelectedIndex;
            dt.Rows.RemoveAt(idSelected);//移除选中行

        }

        private void editGoodButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancelGoodButton_Click(object sender, RoutedEventArgs e)
        {
            dt.Clear();
            dataGridDetail.ItemsSource = dt.DefaultView;
        }

        private void confirmGoodButton_Click(object sender, RoutedEventArgs e)
        {
            IWareHouseDao whd = new WareHouseDao();
            IProcurementDao ipd = new ProcementDao();
            IGoodDao igd = new GoodDao();
            Procurement procurment = new Procurement();
            List<WareHouse> warehouseList = whd.getWareHouseList();
            int warehouseid = Convert.ToInt32(warehouseBox.Text);
            for (int i = 0; i < dt.Rows.Count; i++)
                for (int j = 0; j < warehouseList.Count; j++)
                {
                    if (warehouseid == warehouseList[j].WareHouseID)
                    {
                        procurment.WareHouse = warehouseList[j];
                        Good good = igd.getSingleGood(Convert.ToInt32(dt.Rows[i][0]));
                        Dictionary<Good, int> listgood_number = new Dictionary<Good, int>();
                        listgood_number.Add(good, Convert.ToInt32(dt.Rows[i][3]));
                        //procurment还需要自己添加一个自身employee信息
                        procurment.addGood(good, listgood_number[good]);
                    }
                }
            DateTime now = DateTime.Now;
            procurment.WareHouse.WareHouseID = warehouseid;
            procurment.Employee.EmployeeID = 0;
            procurment.StorageTime = now;
            ipd.addProcurement(procurment);
            dt.Clear();
            dataGridDetail.ItemsSource = dt.DefaultView;

        }
        private void click_deleteOne(object sender, RoutedEventArgs e)
        {
            IGoodDao pd = new GoodDao();
            DataRow mySelectedElement = (DataRow)dataGridDetail.SelectedItem;  //获取datagrid选择的对象

            dt.Rows.Remove(mySelectedElement);//移除选中行



        }
    }
}
