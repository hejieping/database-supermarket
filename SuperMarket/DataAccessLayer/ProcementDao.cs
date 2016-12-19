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
    class ProcementDao : IProcurementDao
    {
        private OracleConnection con = null;
        public ProcementDao()
        {
            con = DBConnection.getConnection();

        }

        public DataSet getSingleProDataSet(int procurmentID)
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select procurementID as 采购单编号 ,storageTime as 采购时间 , totalMoney as 采购总价 , totalQuality as 采购总数 , wareHouseID as 仓库ID " +
                    " from procurement where procurementID = :id ";

                //      OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataAdapter da = new OracleDataAdapter(sql, con);
                da.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                da.SelectCommand.Parameters[0].Value = procurmentID;
                da.Fill(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return set;
        }

        bool IProcurementDao.addProcurement(Procurement procurment)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            OracleTransaction Transaction = con.BeginTransaction();
            bool flag = true;
            try
            {
        //        string max = "select max(billid) from bill";
       //         OracleCommand cmd = new OracleCommand(max, con);
     //           int procurmentid = int.Parse(cmd.ExecuteScalar().ToString()) + 1;
                string sql = "insert into Procurement values(null,:totalMoney,:totalQuality,:employeeID,:wareHouseID,:storageTime)";
                 OracleCommand     cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(":totalMoney", OracleDbType.Decimal);
                cmd.Parameters.Add(":totalQuality", OracleDbType.Int32);
                cmd.Parameters.Add(":employeeID", OracleDbType.Int32);
                cmd.Parameters.Add(":wareHouseID", OracleDbType.Int32);
                cmd.Parameters.Add(":storageTime", OracleDbType.TimeStamp);
                cmd.Parameters[0].Value = procurment.totalMoney;
                cmd.Parameters[1].Value = procurment.PieceAmount;
                cmd.Parameters[2].Value = procurment.Employee.EmployeeID;
                cmd.Parameters[3].Value = procurment.WareHouse.WareHouseID;
                cmd.Parameters[4].Value = procurment.StorageTime;
                cmd.ExecuteNonQuery();
                string max = "select max(procurementid) from procurement";
                cmd = new OracleCommand(max, con);
                int procurmentid = int.Parse(cmd.ExecuteScalar().ToString());
                Dictionary<Good, int> temp = procurment.Good_Procurement;

                foreach (Good good in temp.Keys)
                {
                    int amount = temp[good];
                    string str = "insert into procurement_good values(:goodid,:procurmentid,:amount,:pieceprice)";
                    cmd = new OracleCommand(str, con);
                    cmd.Parameters.Add(":goodid", OracleDbType.Int32);
                    cmd.Parameters.Add(":procurmentid", OracleDbType.Int32);
                    cmd.Parameters.Add(":amount", OracleDbType.Int32);
                    cmd.Parameters.Add(":pieceprice", OracleDbType.Decimal);
                    cmd.Parameters[0].Value = good.GoodID;
                    cmd.Parameters[1].Value = procurmentid;
                    cmd.Parameters[2].Value = amount;
                    cmd.Parameters[3].Value = good.PiecePrice;
                    cmd.ExecuteNonQuery();
                    cmd = new OracleCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "MYSUPERMAKET.add_good_into_WAREHOUSE";
                    cmd.Parameters.Add("goodid", OracleDbType.Int32);
                    cmd.Parameters.Add("warehouseid", OracleDbType.Int32);
                    cmd.Parameters.Add("quantity", OracleDbType.Int32);
                    cmd.Parameters[0].Direction = ParameterDirection.Input;
                    cmd.Parameters[1].Direction = ParameterDirection.Input;
                    cmd.Parameters[2].Direction = ParameterDirection.Input;
                    cmd.Parameters[0].Value = good.GoodID;
                    cmd.Parameters[1].Value = procurment.WareHouse.WareHouseID;
                    cmd.Parameters[2].Value = amount;
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

        DataSet IProcurementDao.getAllProcurement()
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select procurementID as 采购单编号 ,storageTime as 采购时间 , totalMoney as 采购总价 , totalQuality as 采购总数 , wareHouseID as 仓库ID " +
                    " from procurement ";

                //      OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataAdapter da = new OracleDataAdapter(sql, con);
                da.Fill(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return set;
        }

        DataSet IProcurementDao.getAllProcurement( DateTime starttime, DateTime endtime)
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select procurmentID as 采购单编号 ,storageTime as 采购时间 , totalMoney as 采购总价 , totalQuality as 采购总数 , wareHouseID as 仓库ID " +
                    " from procurement " +
                    " where :endtime >= storageTime and storageTime >= :starttime  ";

                //         OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataAdapter cmd = new OracleDataAdapter(sql, con);
                cmd.SelectCommand.Parameters.Add(":endtime", OracleDbType.TimeStamp);
                cmd.SelectCommand.Parameters.Add(":starttime", OracleDbType.TimeStamp);
             //   cmd.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                cmd.SelectCommand.Parameters[0].Value = endtime;
                cmd.SelectCommand.Parameters[1].Value = starttime;
             //   cmd.SelectCommand.Parameters[2].Value = employeeID;
                //  OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                cmd.Fill(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return set;
        }

        List<Procurement> IProcurementDao.getAllProcurementList( DateTime starttime, DateTime endtime)
        {
            List<Procurement> ProcurementList = new List<Procurement>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select *" +
                     " from procurement natural join wareHouse " +
                     " where :endtime >= storageTime and storageTime >= :starttime  " ;

                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(":endtime", OracleDbType.TimeStamp);
                cmd.Parameters.Add(":starttime", OracleDbType.TimeStamp);
     //           cmd.Parameters.Add(":id", OracleDbType.Int32);
                cmd.Parameters[0].Value = endtime;
                cmd.Parameters[1].Value = starttime;
     //           cmd.Parameters[2].Value = employeeID;
                OracleDataReader DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {
                    Procurement Procurement = new Procurement();
                    int id = int.Parse(DataReader["ProcurementID"].ToString());
                    DateTime time = (DateTime)DataReader["storageTime"];
                    int wareHouseId = int.Parse(DataReader["wareHouseID"].ToString());
                    int employeeId = int.Parse(DataReader["employeeID"].ToString());
                    string wareHouseName = DataReader["name"].ToString();
                    string wareHouseAddress = DataReader["address"].ToString();
                    WareHouse wareHouse = new WareHouse();
                    wareHouse.Name = wareHouseName;
                    wareHouse.Address = wareHouseAddress;
                    wareHouse.Address = wareHouseAddress;

                    Employee employee = new Employee();
                    employee.EmployeeID = employeeId;
                    Procurement.Employee = employee;
                    Procurement.ProcurementID = id;
                    Procurement.StorageTime = time;
                    Procurement.WareHouse = wareHouse;
                    ProcurementList.Add(Procurement);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }

            //MessageBox.Show(ProcurementList[0].ToString());
            return ProcurementList;
        }

        DataSet IProcurementDao.getMyProcurement(int employeeID)
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select procurementID as 采购单编号 ,storageTime as 采购时间 , totalMoney as 采购总价 , totalQuality as 采购总数 , wareHouseID as 仓库ID" +
                    " from procurement " +
                    " where employeeID = " + employeeID;
       //         OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.Fill(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return set;
        }

        DataSet IProcurementDao.getMyProcurement(int employeeID, DateTime starttime  , DateTime endtime)
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select procurmentID as 采购单编号 ,storageTime as 采购时间 , totalMoney as 采购总价 , totalQuality as 采购总数 , wareHouseID as 仓库ID" +
                    " from procurement " +
                    " where :endtime >= storageTime and storageTime >= :starttime and employeeID = " +employeeID;

                OracleDataAdapter cmd = new OracleDataAdapter(sql,con);
                cmd.SelectCommand.Parameters.Add(":endtime", OracleDbType.TimeStamp);
                cmd.SelectCommand.Parameters.Add(":starttime", OracleDbType.TimeStamp);
                cmd.SelectCommand.Parameters[0].Value = endtime;
                cmd.SelectCommand.Parameters[1].Value = starttime;
                //     OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                cmd.Fill(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return set;
        }

        List<Procurement> IProcurementDao.getMyProcurementList(int employeeID, DateTime starttime, DateTime endtime)
        {
            List<Procurement> ProcurementList = new List<Procurement>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select * from procurement natural join wareHouse where :endtime >= storageTime and storageTime >= :starttime  and employeeID = " +employeeID;

                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(":endtime", OracleDbType.TimeStamp);
                cmd.Parameters.Add(":starttime", OracleDbType.TimeStamp);
                cmd.Parameters[0].Value = endtime;
                cmd.Parameters[1].Value = starttime;
                //cmd.ExecuteNonQuery();
                OracleDataReader DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {
                    Procurement Procurement = new Procurement();
                    int id = int.Parse(DataReader["ProcurementID"].ToString());
                    DateTime time = (DateTime)DataReader["storageTime"];
                    int wareHouseId = int.Parse(DataReader["wareHouseID"].ToString());
                    string wareHouseName = DataReader["name"].ToString();
                    string wareHouseAddress = DataReader["address"].ToString();
                    WareHouse wareHouse = new WareHouse();
                    wareHouse.Name = wareHouseName;
                    wareHouse.Address = wareHouseAddress;
                    wareHouse.Address = wareHouseAddress;

                    Employee employee = new Employee();
                    employee.EmployeeID = employeeID;
                    Procurement.Employee = employee;
                    Procurement.ProcurementID = id;
                    Procurement.StorageTime = time;
                    Procurement.WareHouse = wareHouse;
                    ProcurementList.Add(Procurement);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return ProcurementList;
        }


        DataSet IProcurementDao.getProcurementInfo(int procurementID)
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                /*      string sql = "select procurementID as 采购单编号 ,storageTime as 采购时间 , totalMoney as 采购总价 , totalQuality as 采购总数 , wareHouseID as 仓库ID,pieceprice as 进购单价 ,amount as 进购数量" +
                          " from procurement natural join procurement_good" +
                          " where procurementid=" + procurementID;
                      OracleCommand cmd = new OracleCommand(sql, con);*/
                string sql = " select good.goodid as ,good.name as ,procurement_good.amount as ,procurement_good.pieceprice as " +
                    " from procurement_good,good where good.goodid = procurement_good.goodid and procurement_good.procurementid = :id";
                OracleDataAdapter da = new OracleDataAdapter(sql, con);
                da.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                da.SelectCommand.Parameters[0].Value = procurementID;
                da.Fill(set);
         //       OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
         //     DataAdapter.Fill(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return set;
        }


        Procurement IProcurementDao.getSingleProcurement(int procurementID)
        {
            string sql = "select * from Procurement natural join wareHouse where procurementid = :id";
            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(":id", OracleDbType.Int32);
            cmd.Parameters[0].Value = procurementID;
            Procurement procurement = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                OracleDataReader DataReader = cmd.ExecuteReader();
                if (DataReader.Read())
                {                                     
                    int wareHouseId = int.Parse(DataReader["wareHouseID"].ToString());
                    string wareHouseName = DataReader["name"].ToString();
                    string wareHouseAddress = DataReader["address"].ToString();
                    WareHouse wareHouse = new WareHouse();
                    wareHouse.Name = wareHouseName;
                    wareHouse.Address = wareHouseAddress;
                    wareHouse.Address = wareHouseAddress;
                    
                    procurement = new Procurement();
                    procurement.ProcurementID = procurementID;
                    procurement.StorageTime = DateTime.Parse(DataReader["storageTime"].ToString());
                    procurement.WareHouse = wareHouse; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Closed)
                    con.Close();
            }
            return procurement;
        }
    }
}
