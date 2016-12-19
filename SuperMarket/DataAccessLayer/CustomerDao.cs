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
using System.Windows.Forms;
using SuperMarket.MyConnection;
namespace SuperMarket.DataAccessLayer
{
    class CustomerDao : ICustomerDao
    {
        private const int normal = 9999;
        private OracleConnection con = null;
        public CustomerDao()
        {
            con = DBConnection.getConnection();
        }

       
        bool ICustomerDao.addCustomer(Customer customer)
        {
            string sql = "insert  into Customer values(null,:regdate,:membershippoint)";
            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(":regdate",OracleDbType.TimeStamp);
            cmd.Parameters.Add(":membership", OracleDbType.Decimal);
            cmd.Parameters[0].Value = customer.RegDate;
            cmd.Parameters[1].Value = customer.MemberShipPoint;
            int result = -1;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            if (result != -1)
                return true;
            return false;
        }

        DataSet ICustomerDao.getAllCustomerInfo()
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select customerID as 顾客编号 ,regdate as 注册日期 ,membershippoint 顾客积分 from customer";
                OracleCommand cmd = new OracleCommand(sql,con);
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.Fill(set);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return set;
        }

        List<Customer> ICustomerDao.getAllCustomerList()
        {
            List<Customer> CustomerList = new List<Customer>();
            try
            {
                if(con.State == ConnectionState.Closed)
                      con.Open();
                string sql = "select * from customer";
                OracleCommand cmd = new OracleCommand(sql,con);
                OracleDataReader DataReader = cmd.ExecuteReader();
                while(DataReader.Read())
                {
                    Customer customer = new Customer();
                    int id = int.Parse( DataReader["customerid"].ToString());
                    DateTime time =(DateTime)DataReader["regdate"];
                    decimal point =decimal.Parse( DataReader["membershippoint"].ToString());
                    customer.CustomerID = id;
                    customer.RegDate = time;
                    customer.MemberShipPoint = point;
                    CustomerList.Add(customer);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return CustomerList;
        }



        Customer ICustomerDao.getSingleCustomer(int CustomerID)
        {
            string sql = "select * from customer where customerid = :id";
            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(":id", OracleDbType.Int32);
            cmd.Parameters[0].Value = CustomerID;
            Customer customer = null;
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                OracleDataReader DataReader = cmd.ExecuteReader();
                if(DataReader.Read())
                {
                    customer = new Customer();
                    customer.CustomerID = CustomerID;
                    customer.RegDate = DateTime.Parse(DataReader["regdate"].ToString());
                    customer.MemberShipPoint = decimal.Parse(DataReader["membershippoint"].ToString());
                    cmd = new OracleCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "select discount from  VIEW_CUSTOMER_DISCOUNT where customerid = :id";
                    cmd.Parameters.Add(":customerid", OracleDbType.Int32);
                    cmd.Parameters[0].Value = CustomerID;
                     OracleDataReader reader =  cmd.ExecuteReader();
                    if (reader.Read()) customer.Discount_deserve = decimal.Parse(reader["discount"].ToString());
       /*             cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "MYSUPERMAKET.get_cus_discount";
                    cmd.Parameters.Add("customer_id", OracleDbType.Int32);
                    cmd.Parameters.Add("discount", OracleDbType.Decimal);
                    cmd.Parameters[0].Direction = ParameterDirection.Input;
                    cmd.Parameters[1].Direction = ParameterDirection.Output;
                    cmd.Parameters[0].Value = CustomerID;
                    int k =(int)cmd.Parameters[0].Value;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    k =  cmd.ExecuteNonQuery();*/
             //       customer.Discount_deserve =decimal.Parse( cmd.Parameters[1].Value.ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Closed)
                    con.Close();
            }
            return customer;
        }
        Customer ICustomerDao.getNormalCustomer()
        {
            ICustomerDao dao = this;
            return dao.getSingleCustomer(normal);
        }
    }
}
