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
using SuperMarket.Entity;
using SuperMarket.DataAccessLayer;
using System.Data;


namespace SuperMarket.UIWpf.Manager
{
    /// <summary>
    /// WareHouseInfo.xaml 的交互逻辑
    /// </summary>
    public partial class WareHouseInfo : UserControl
    {
        private IWareHouseDao warehousedao;
        private string get_warehouse = null;
        private int m_warehouseID = -1;
        public WareHouseInfo()
        {
            InitializeComponent();
            dataGrid.IsReadOnly = true;
            dataGrid.SelectionMode = DataGridSelectionMode.Single;
            //初始化仓库信息
            warehousedao = new WareHouseDao();
            DataSet set = warehousedao.getAllWareHouseInfo();
            dataGrid.ItemsSource = set.Tables[0].DefaultView;
            
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
            catch (Exception)
            { return; }
            m_warehouseID = index;
            DataSet set = warehousedao.getGoodsInWareHouse(m_warehouseID);
            dataGridDetail.ItemsSource = set.Tables[0].DefaultView;
        }
    }
}
