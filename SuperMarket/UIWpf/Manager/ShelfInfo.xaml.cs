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
using SuperMarket.Entity;


namespace SuperMarket.UIWpf.Manager
{
    /// <summary>
    /// ShelfInfo.xaml 的交互逻辑
    /// </summary>
    public partial class ShelfInfo : UserControl
    {
        private int m_shelfID = 0;
        private int m_shelfEmployeeID = 0;

        private string get_shelfID = null;
        private string get_shelfEmployeeID = null;

        private IShelfDao shelfDao;
        public ShelfInfo()
        {
            InitializeComponent();
            dataGrid.IsReadOnly = true;
            dataGrid.SelectionMode = DataGridSelectionMode.Single;
            shelfDao = new ShelfDao();
            getAllShelfs();
        }
        private void getAllShelfs()
        {
            dataGrid.ItemsSource = shelfDao.getAllShelvesInfo().Tables[0].DefaultView;
        }

        private void Search_Copy1_Click(object sender, RoutedEventArgs e)
        {
            get_shelfID = shelfID.Text;

            try
            {
                m_shelfID = int.Parse(get_shelfID);
            }
            catch
            {
                MessageBox.Show("请输入整数型 货架ID");
                return;
            }
            // 使用了 新的接口 IShelfDao 的 getShelfByShelfID
            DataSet set = shelfDao.getShelfByShelfID(m_shelfID);

            dataGrid.ItemsSource = set.Tables[0].DefaultView;
        }

        private void Search_Copy_Click(object sender, RoutedEventArgs e)
        {
            get_shelfEmployeeID = shelfEmployeeID.Text;
            try
            {
                m_shelfEmployeeID = int.Parse(get_shelfEmployeeID);
            }
            catch
            {
                MessageBox.Show("请输入整数型 员工ID");
                return;
            }
            // 使用了 新的接口 IShelfDao 的 getShelfByEmployee
            DataSet set = shelfDao.getShelfByEmployee(m_shelfEmployeeID);

            dataGrid.ItemsSource = set.Tables[0].DefaultView;
        }
    }
}
