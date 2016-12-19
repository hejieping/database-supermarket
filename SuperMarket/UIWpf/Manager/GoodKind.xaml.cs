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
    /// GoodKind.xaml 的交互逻辑
    /// </summary>
    public partial class GoodKind : UserControl
    {
        private int id = -1;
        private ITypeDao dao;
        public GoodKind()
        {
            InitializeComponent();
            init();
            dataGrid.SelectionMode = DataGridSelectionMode.Single;
            dataGridDetail.SelectionMode = DataGridSelectionMode.Single;
            dataGrid.IsReadOnly = true;
            dataGridDetail.IsReadOnly = true;
        }
        private void init()
        {
            dao = new TypeDao();
            getAllTypes();
        }
        private void getAllTypes()
        {
            DataSet set = dao.getAllTypes();
            dataGrid.ItemsSource = set.Tables[0].DefaultView;
        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
   //         getAllTypes();
        }

        private void dataGridDetail_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = this.dataGrid.SelectedItem;
            var b = a as DataRowView;
            string result = b.Row[0].ToString();
            int index = 0;
            try
            {
                index = int.Parse(result);
            }
            catch (Exception)
            { return; }
            DataSet set = dao.getGoodbytype(index);
            dataGridDetail.ItemsSource = set.Tables[0].DefaultView;
        }
    }
}
