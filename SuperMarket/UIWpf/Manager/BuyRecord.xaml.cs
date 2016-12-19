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

namespace SuperMarket.UIWpf.Manager
{
    /// <summary>
    /// BuyRecord.xaml 的交互逻辑
    /// </summary>
    public partial class BuyRecord : UserControl
    {
        private IProcurementDao dao = new ProcementDao();

        public BuyRecord()
        {
            InitializeComponent();
            getAllProcurements();
        }

        private void Search_Copy1_Click(object sender, RoutedEventArgs e)
        {
            IProcurementDao pd = new ProcementDao();
            string s = textBox_Copy1.Text;
            DataSet ds = new DataSet();
            int i;
            if (int.TryParse(s, out i))
            {
                ds = pd.getSingleProDataSet(i);
                dataGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            else
            {
                MessageBox.Show("请输入数字");
            }
        }


        private void getAllProcurements()
        {
            dataGrid.ItemsSource = dao.getAllProcurement().Tables[0].DefaultView;
        }
        private void Search_Copy_Click(object sender, RoutedEventArgs e)
        {
            IProcurementDao pd = new ProcementDao();
            string s = textBox_Copy2.Text;
            DataSet ds = new DataSet();
            int i;
            if (int.TryParse(s, out i))
            {
                ds = pd.getMyProcurement(i);
                dataGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            else
            {
                MessageBox.Show("请输入数字");
            }
        }

        private void dataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                IProcurementDao pd = new ProcementDao();
                DataRowView mySelectedElement = (DataRowView)dataGrid.SelectedItem;  //获取datagrid选择的对象


                string result = mySelectedElement.Row[0].ToString();
                int i;
                if (int.TryParse(result, out i))
                {
                    dataGridDetail.ItemsSource = pd.getProcurementInfo(i).Tables[0].DefaultView;
                }
            }
            catch(Exception ex)
            {
                return;
            }
        }
    }
}
