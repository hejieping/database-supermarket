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
using SuperMarket.DataAccessLayer;
using SuperMarket.DataAccess;

namespace SuperMarket.UIWpf.Mover
{
    /// <summary>
    /// ShelfLackGood.xaml 的交互逻辑
    /// </summary>
    public partial class ShelfLackGood : UserControl
    {
        private int ID;
        private IShelfDao dao;
        public ShelfLackGood()
        {
            InitializeComponent();

           
        }
        public ShelfLackGood(int id)
        {
            InitializeComponent();
            ID = id;
            dao = new ShelfDao();
            datagrid.SelectionMode = DataGridSelectionMode.Single;
            datagrid.IsReadOnly = true;
            warehouse_good.SelectionMode = DataGridSelectionMode.Single;
            warehouse_good.IsReadOnly = true;
            getNotice();  
        }
        private void getNotice()
        {
            datagrid.ItemsSource = dao.getShelfNoticeInfo(ID).Tables[0].DefaultView;
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int goodid = 0;
            try
            {
                var a = datagrid.SelectedItem;
                var b = a as DataRowView;
                string result = b.Row[0].ToString();
                goodid = int.Parse(result);
            }
            catch(Exception exx)
            {
                return;
            }
            GoodDao gdao = new GoodDao();
            warehouse_good.ItemsSource = gdao.getAllWareHouseStorage(goodid).Tables[0].DefaultView;

        }
    }
}
