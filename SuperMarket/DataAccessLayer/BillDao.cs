using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarket.DataAccess;
using Oracle.DataAccess.Client;
using Oracle.DataAccess;
using SuperMarket.Entity;
using System.Data;
using SuperMarket.MyConnection;
using System.Windows.Forms;

namespace SuperMarket.DataAccessLayer
{
    class BillDao : IBillDao
    {
        private OracleConnection con = null;
        public BillDao()
        {
            con = DBConnection.getConnection();
        }

        //负责初始化账单负责人id，顾客对象（如果是非会员的话，就不初始化顾客）购买日期，调用bill的addgood方法添加商品，必须初始化good的id，购买时单价即这个sellprice字段，或者(price , discount)这两个属性
        bool IBillDao.addBill(Bill bill)
        {
            bool flag = true;
            if (con.State == ConnectionState.Closed)
                con.Open();
            OracleTransaction Transaction = con.BeginTransaction();
            try
            {
                string sql = "insert into Bill values(null,:totalprice,:purchaseingdate,:goods_total,:customerid,:employeeid)";
                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(":totalprice", OracleDbType.Decimal);
                cmd.Parameters.Add(":purchaseingdate", OracleDbType.TimeStamp);
                cmd.Parameters.Add(":goods_total", OracleDbType.Int32);
                cmd.Parameters.Add(":customerid", OracleDbType.Int32);
                cmd.Parameters.Add(":employeeid", OracleDbType.Int32);
                cmd.Parameters[0].Value = bill.TotalPrice;
                cmd.Parameters[1].Value = bill.PurchaseingDate;
                cmd.Parameters[2].Value = bill.Total_amount;
                cmd.Parameters[3].Value = bill.Customer.CustomerID;
                cmd.Parameters[4].Value = bill.Employee.EmployeeID;
                cmd.ExecuteNonQuery();
                string max = "select max(billid) as billid from bill";
                cmd = new OracleCommand(max, con);
                int billid = 0;
                string next = cmd.ExecuteScalar().ToString();
                if (next == "") billid = 1;
                else billid = int.Parse(next);
                Dictionary<Good, int> temp = bill.GoodSells;
               
                foreach (Good good in temp.Keys)
                {
                    int amount = temp[good];
                    string str = "insert into bill_good values(:goodid,:billid,:quanlity,:price)";
                    cmd = new OracleCommand(str, con);
                    cmd.Parameters.Add(":goodid", OracleDbType.Int32);
                    cmd.Parameters.Add(":billid", OracleDbType.Int32);
                    cmd.Parameters.Add(":quanlity", OracleDbType.Int32);
                    cmd.Parameters.Add(":price", OracleDbType.Decimal);
                    cmd.Parameters[0].Value = good.GoodID; 
                    cmd.Parameters[1].Value = billid;
                    cmd.Parameters[2].Value = amount;
                    cmd.Parameters[3].Value = good.Price;
                    cmd.ExecuteNonQuery();
                }
               Transaction.Commit();
            }
            catch (Exception e)
            {
              Transaction.Rollback();
                MessageBox.Show(e.ToString());
                flag = false;
            }
            finally
            {
                con.Close();
            }
            return flag;
        }

        List<Bill> IBillDao.getAllBillList()
        {
            List<Bill> m_Bill_List = new List<Bill>();

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select * from bill";
                OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataReader DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {
                    //
                    //所有账单情况下只提供顾客id和员工id
                    //
                    Bill bill = new Bill();

                    int billID = int.Parse(DataReader["billid"].ToString());
                    DateTime purchasingDate = (DateTime)DataReader["purchaseingDate"];
                    int customerID = int.Parse(DataReader["customerid"].ToString());
                    decimal total_money = decimal.Parse(DataReader["totalprice"].ToString());
                    int employeeID = int.Parse(DataReader["employeeid"].ToString());
                    int total_amout = int.Parse(DataReader["Goods_total"].ToString());
                    bill.BillID = billID;
                    bill.TotalPrice = total_money;
                    bill.PurchaseingDate = purchasingDate;
                    bill.Customer.CustomerID = customerID;
                    bill.Employee.EmployeeID = employeeID;
                    bill.Total_amount = total_amout;
                    m_Bill_List.Add(bill);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            return m_Bill_List;
        }

        DataSet IBillDao.getAllBills()
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select billID as 账单编号, totalPrice as 总价, purchaseingDate as 账单日期, goods_Total as 物品总数, employeeID as 售货员编号 from BILL";//暂时没有顾客编号
                OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.Fill(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if(con.State==ConnectionState.Open)
                    con.Close();
            }
            return set;


        }

        DataSet IBillDao.getAllBills(DateTime startTime, DateTime endTime)
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select billID as 账单编号, totalPrice as 总价, purchaseingDate as 账单日期, goods_Total as 物品总数, employeeID as 售货员编号"//暂时没有顾客编号
                    + " from BILL"
                    + " where purchaseingDate >= :starttime and purchaseingDate <:endtime";//datetime类型怎么比较
                OracleDataAdapter cmd = new OracleDataAdapter(sql, con);
                cmd.SelectCommand.Parameters.Add(":starttime", OracleDbType.TimeStamp);
                cmd.SelectCommand.Parameters.Add(":endtime", OracleDbType.TimeStamp);
                cmd.SelectCommand.Parameters[0].Value = startTime;
                cmd.SelectCommand.Parameters[1].Value = endTime;
               

                cmd.Fill(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if(con.State == ConnectionState.Open)
                con.Close();
            }
            return set;
        }

        DataSet IBillDao.getBillDetailedInfo(int BillID)
        {
            DataSet set = new DataSet();
            string sql = "select bill_good.billid as 账单编号,good.name as 商品名称,bill_good.price as 商品价格 ,bill_good.quality as 销售数量  from bill_good ,good where bill_good.goodid = good.goodid" +
                " and  billID = :billID";
  //          OracleCommand cmd = new OracleCommand(sql, con);
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.SelectCommand.Parameters.Add(":billId", OracleDbType.Int32);
                DataAdapter.SelectCommand.Parameters[0].Value = BillID;
                DataAdapter.Fill(set);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return set;
        }

        //不负责初始化一条订单的每条商品销售信息，暂时就这样
        Bill IBillDao.getSingleBill(int BillID)
        {
            Bill m_bill = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select * from bill where billid = :id";
                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(":id", OracleDbType.Int32);
                cmd.Parameters[0].Value = BillID;
                OracleDataReader DataReader = cmd.ExecuteReader();
                if (DataReader.Read())
                {
                    m_bill = new Bill();
                    m_bill.BillID = BillID;
                    m_bill.TotalPrice = decimal.Parse(DataReader["totalprice"].ToString());
                    m_bill.PurchaseingDate = DateTime.Parse(DataReader["purchaseingdate"].ToString());
                    m_bill.Total_amount = int.Parse(DataReader["goods_total"].ToString());
                    int employeeId = int.Parse(DataReader["employeeid"].ToString());
                    int customerid = int.Parse(DataReader["customerid"].ToString());
                    ICustomerDao cDao = new CustomerDao();
                    m_bill.Customer = cDao.getSingleCustomer(customerid);
                    IEmployeeDao eDao = new EmployeeDao();
                    m_bill.Employee = eDao.getSingleEmployee(employeeId);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return m_bill;
        }

        DataSet IBillDao.getAllBills(DateTime startTime, DateTime endTime, int employeeid)
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select billID as 账单编号, totalPrice as 总价, purchaseingDate as 账单日期, goods_Total as 物品总数, employeeID as 售货员编号"//暂时没有顾客编号
                    + " from BILL"
                    + " where purchaseingDate >= :starttime and purchaseingDate <:endtime and employeeid = :id";//datetime类型怎么比较
                                                                                                                //    Oracl cmd = new OracleCommand(sql, con);
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.SelectCommand.Parameters.Add(":starttime", OracleDbType.TimeStamp);
                DataAdapter.SelectCommand.Parameters.Add(":endtime", OracleDbType.TimeStamp);
                DataAdapter.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                DataAdapter.SelectCommand.Parameters[0].Value = startTime;
                DataAdapter.SelectCommand.Parameters[1].Value = endTime;
                DataAdapter.SelectCommand.Parameters[2].Value = employeeid;
         //       OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.Fill(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return set;
        }

        DataSet IBillDao.getBillInfo(int billid)
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select billID as 账单编号, totalPrice as 总价, purchaseingDate as 账单日期, goods_Total as 物品总数, employeeID as 售货员编号 from BILL where billid = :id";//暂时没有顾客编号
                                                                                                                                                                       //    OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                DataAdapter.SelectCommand.Parameters[0].Value = billid;
                DataAdapter.Fill(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return set;
        }
    }
}