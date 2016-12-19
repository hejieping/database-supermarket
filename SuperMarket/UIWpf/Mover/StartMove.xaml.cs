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

namespace SuperMarket.UIWpf.Mover
{
    /// <summary>
    /// StartMove.xaml 的交互逻辑
    /// </summary>
    public partial class StartMove : UserControl
    {
        DataTable dt;
        public StartMove()
        {
            InitializeComponent();
            initDataGrid();
        }
        private void initDataGrid()
        {
            dt = new DataTable();
            DataColumn cgoodId = new DataColumn("商品编号", typeof(int));
            DataColumn cgoodName = new DataColumn("商品名称", typeof(string));
            DataColumn cgoodNumber = new DataColumn("商品数量", typeof(int));
            DataColumn wareHouseID = new DataColumn("仓库编号", typeof(int));
            dt.Columns.Add(cgoodId);
            dt.Columns.Add(cgoodName);
            dt.Columns.Add(cgoodNumber);
            dt.Columns.Add(wareHouseID);
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
                int number = 1;
                int warehouseId = 1;

                DataRow dr = dt.NewRow();
                dr[0] = GoodID;
                dr[1] = Name;
                dr[2] = number;
                dr[3] = warehouseId;

                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    if (i == Convert.ToInt32(dt.Rows[m][0]))
                    {
                        dr[2] = Convert.ToInt32(dt.Rows[m][2]) + number;
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

        private void deleteGoodButton_Click(object sender, RoutedEventArgs e)
        {
            IGoodDao pd = new GoodDao();
            DataRowView mySelectedElement = (DataRowView)dataGridDetail.SelectedItem;  //获取datagrid选择的对象
            dt.Rows.RemoveAt(dataGridDetail.SelectedIndex);
        }
        private void click_deleteOne(object sender, RoutedEventArgs e)
        {
            IGoodDao pd = new GoodDao();
            DataRow mySelectedElement = (DataRow)dataGridDetail.SelectedItem;  //获取datagrid选择的对象

            dt.Rows.Remove(mySelectedElement);//移除选中行
        }

        private void editGoodButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void confirmGoodButton_Click(object sender, RoutedEventArgs e)
        {
            IWareHouseDao whd = new WareHouseDao();
            IMoveRecordDao mrd = new MoveRecordDao();
            IGoodDao igd = new GoodDao();
            MoveRecord moverecord = new MoveRecord();
            List<WareHouse> warehouseList = whd.getWareHouseList();
            Good good = new Good();
            bool find = false;
            for (int j = 0; j < warehouseList.Count; j++)
            {
                if (Convert.ToInt32(warehouseIdBox.Text) == warehouseList[j].WareHouseID)
                {
                    find = true;
                    moverecord.WareHouse = warehouseList[j];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //遍历查询仓库
                        good = igd.getSingleGood(Convert.ToInt32(dt.Rows[i][0]));
                        moverecord.addGood(good, Convert.ToInt32(dt.Rows[i][2]));
                    }

                }
            }
            if (!find)
                MessageBox.Show("仓库不存在!");
            else
            {
                DateTime now = DateTime.Now;
                moverecord.RemoveTime = now;
                mrd.addmove(moverecord);
            }
            //提交采购单后清空
            dt.Clear();
            dataGridDetail.ItemsSource = dt.DefaultView;


        }

        private void cancleGoodButton_Click(object sender, RoutedEventArgs e)
        {
            dt.Clear();
            dataGridDetail.ItemsSource = dt.DefaultView;
        }
    }
}
