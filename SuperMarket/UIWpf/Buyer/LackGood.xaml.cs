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

namespace SuperMarket.UIWpf.Buyer
{
    /// <summary>
    /// LackGood.xaml 的交互逻辑
    /// </summary>
    public partial class LackGood : UserControl
    {
        private int ID;
        IWareHouseDao whd = new WareHouseDao();
        public LackGood(int eid)
        {
            this.InitializeComponent();
            //该函数需传入员工id
            ID = eid;
            getNotice();
            datagird.IsReadOnly = true;
            datagird.SelectionMode = DataGridSelectionMode.Single;
           
        }
        private void getNotice()
        {
            DataSet set = whd.getWarehouseNoticeInfo(ID);
            datagird.ItemsSource = set.Tables[0].DefaultView;
        }
    }
}
