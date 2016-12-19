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
using SuperMarket.DataAccess;
using SuperMarket.DataAccessLayer;
using System.Data;

namespace SuperMarket.UIWpf.Seller
{
    /// <summary>
    /// SellerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SellerWindow : Window
    {
        Bill iBill;
        DataTable dt;
        private Employee employee;
        public SellerWindow()
        {
            InitializeComponent();
            iBill = new Bill();
            InitilDataGrid();
        }

        public SellerWindow(Employee e)
        {
            InitializeComponent();
            iBill = new Bill();
            InitilDataGrid();
            employee = e;
        }
        void InitilDataGrid()
        {
            dt = new DataTable();
            DataColumn cgoodID = new DataColumn("商品编号", typeof(int));
            DataColumn cgoodName = new DataColumn("商品名称", typeof(string));
            DataColumn cgoodPrice = new DataColumn("单价", typeof(decimal));
            DataColumn cgoodNumber = new DataColumn("数量", typeof(int));
            DataColumn cgoodTotalPrice = new DataColumn("总价", typeof(Decimal));
            dt.Columns.Add(cgoodID);
            dt.Columns.Add(cgoodName);
            dt.Columns.Add(cgoodPrice);
            dt.Columns.Add(cgoodNumber);
            dt.Columns.Add(cgoodTotalPrice);
            DataColumn[] dataCol = new DataColumn[1];
            dataCol[0] = dt.Columns[0];
            dt.PrimaryKey = dataCol;
            dataGrid.ItemsSource = dt.DefaultView;
        }

        private void idSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isIn = false;
                String idString = idBox.Text;
                int goodId = int.Parse(idString);              
                Good good = new Good();
                good = ((IGoodDao)new GoodDao()).getSingleGood(goodId);
                if (iBill.GoodSells.ContainsKey(good))
                    isIn = true;
                iBill.addGood(good, 1);
                iBill.Customer = ((ICustomerDao)new CustomerDao()).getNormalCustomer();

                //刷新DataGrid
                DataRow rgoodInfo = dt.NewRow();
                rgoodInfo[0] = good.GoodID;
                rgoodInfo[1] = good.Name;
                rgoodInfo[2] = good.Price;
                rgoodInfo[3] = iBill.GoodSells[good];
                rgoodInfo[4] = good.Price * iBill.GoodSells[good];

                if (!isIn)
                    dt.Rows.Add(rgoodInfo);
                else
                {
                    rgoodInfo[3] = iBill.GoodSells[good];
                    rgoodInfo[4] = good.Price * iBill.GoodSells[good];
                    dt.Rows.Find(good.GoodID).Delete();
                    dt.Rows.Add(rgoodInfo);                
                }

                dataGrid.ItemsSource = dt.DefaultView;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            totalPriceBox.Text = iBill.TotalPrice.ToString();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            iBill.PurchaseingDate = now;
            //提交购买单
            if (((IBillDao)new BillDao()).addBill(iBill))
                MessageBox.Show("恭喜，你的商品购买成功");
            else
                MessageBox.Show("非常抱歉，商品购买失败");
            iBill = new Bill();
            dt.Rows.Clear();
            dataGrid.ItemsSource = dt.DefaultView;
            idBox.Text = "商品编号";
            totalPriceBox.Text = "0.00";
        }

        private void cancleButton_Click(object sender, RoutedEventArgs e)
        {
            //清空购买单
            iBill = new Bill();
            dt.Rows.Clear();
            dataGrid.ItemsSource = dt.DefaultView;
            idBox.Text = "商品编号";
            totalPriceBox.Text = "0.00";
            MessageBox.Show("您的订单已成功取消");
        }
    }
}
