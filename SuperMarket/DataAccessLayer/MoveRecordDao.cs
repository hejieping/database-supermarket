using System;
using System.Collections.Generic;
using SuperMarket.DataAccess;
using Oracle.DataAccess.Client;
using System.Windows.Forms;
using SuperMarket.Entity;
using System.Data;
using SuperMarket.MyConnection;



namespace SuperMarket.DataAccessLayer
{
    class MoveRecordDao : IMoveRecordDao
    {
        private OracleConnection con = null;
        public MoveRecordDao()
        {
            con = DBConnection.getConnection();
        }

        //负责添加一条记录，初始化记录的移出时间，移出仓库，移出员工（只要id）就行，然后添加，移出商品以及总数，
        bool IMoveRecordDao.addmove(MoveRecord moveRecord)
        {
            bool flag = true;
            if (con.State == ConnectionState.Closed)
                con.Open();
            // OracleTransaction transaction = con.BeginTransaction();
            try
            {
          //      string max = "select max(moveid) from move";
            //    OracleCommand cmd = new OracleCommand(max, con);
             //   string next = cmd.ExecuteScalar().ToString();
             //   int moveid = 0;
              //  if (next == "") moveid = 1;
              //  else moveid = int.Parse(next)+1;
                string sql = "insert into Move values(null,:numbers,:time,:warehouseid,:employeeid)";
                OracleCommand  cmd = new OracleCommand(sql, con);
        //        cmd.Parameters.Add(":moveid", OracleDbType.Int32);
                cmd.Parameters.Add(":numbers", OracleDbType.Int32);
                cmd.Parameters.Add(":time", OracleDbType.TimeStamp);
                cmd.Parameters.Add(":warehouseid", OracleDbType.Int32);
                cmd.Parameters.Add(":employeeid", OracleDbType.Int32);
          //      cmd.Parameters[0].Value = moveRecord.RemoveID;
                cmd.Parameters[0].Value = moveRecord.RemoveTotal;
                cmd.Parameters[1].Value = moveRecord.RemoveTime;
                cmd.Parameters[2].Value = moveRecord.WareHouse.WareHouseID;
                cmd.Parameters[3].Value = moveRecord.Employee.EmployeeID;
                cmd.ExecuteNonQuery();
                string max = "select max(moveid) from move";
                cmd = new OracleCommand(max, con);
                string next = cmd.ExecuteScalar().ToString();
                int moveid = 0;
                if (next == "") moveid = 1;
                else moveid = int.Parse(next);
                Dictionary<Good, int> temp = moveRecord.Good_Remove;
                foreach (Good good in temp.Keys)
                {
                    int goodid = good.GoodID;
                    int amount = temp[good];
                    string insert = "insert into move_good values(:goodid,:moveid,:amount)";
                    cmd = new OracleCommand(insert, con);
                    cmd.Parameters.Add(":goodid", OracleDbType.Int32);
                    cmd.Parameters.Add(":moveid",OracleDbType.Int32);
                    cmd.Parameters.Add(":amount", OracleDbType.Int32);
                    cmd.Parameters[0].Value = goodid; 
                    cmd.Parameters[1].Value = moveid;
                    cmd.Parameters[2].Value = amount;
                    cmd.ExecuteNonQuery();
                    OracleCommand cmd1 = new OracleCommand();
                    cmd1.Connection = con;
                    //存储过程
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "MYSUPERMAKET.MOVE_GOOD_FROM_WAREHOUSE";
                    cmd1.Parameters.Add("good_id", OracleDbType.Int32);
                    cmd1.Parameters.Add("warehouseid", OracleDbType.Int32);
                    cmd1.Parameters.Add("quantity", OracleDbType.Int32);
                    cmd1.Parameters[0].Direction = ParameterDirection.Input;
                    cmd1.Parameters[1].Direction = ParameterDirection.Input;
                    cmd1.Parameters[2].Direction = ParameterDirection.Input;
                    cmd1.Parameters[0].Value = goodid;
                    cmd1.Parameters[1].Value = moveRecord.WareHouse.WareHouseID;
                    cmd1.Parameters[2].Value = amount;
                    cmd1.ExecuteNonQuery();
                }
            //    transaction.Commit();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
             //   transaction.Rollback();
                flag = false;
            }
            return flag;
        }

        DataSet IMoveRecordDao.getAllMoveRecords()
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select moveid as 移出编号,numbers as 移出商品总数,time as 移出时间,warehouseid as 仓库编号,employeeid as 员工编号 from move";
                OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.Fill(set);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return set;
        }

        DataSet IMoveRecordDao.getAllMoveRecords(DateTime starttime, DateTime endtime)
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select moveid as 移出编号,numbers as 移出商品总数,time as 移出时间,warehouseid as 仓库编号,employeeid as 员工编号 from move where time between :S_time and :E_time";
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
             
                DataAdapter.SelectCommand.Parameters.Add(":S_time", OracleDbType.TimeStamp);
                DataAdapter.SelectCommand.Parameters.Add("E_time", OracleDbType.TimeStamp);

                DataAdapter.SelectCommand.Parameters[0].Value = starttime;
                DataAdapter.SelectCommand.Parameters[1].Value = endtime;
                DataAdapter.Fill(set);
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

        //只负责初始化员工的id，仓库的id
        List<MoveRecord> IMoveRecordDao.getAllMoveRecordList()
        {
            List<MoveRecord> MoveRecordList = new List<MoveRecord>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select * from move";
                OracleCommand cmd = new OracleCommand(sql, con);

                OracleDataReader DataReader = cmd.ExecuteReader();
                while(DataReader.Read())
                {
                    MoveRecord moverecord = new MoveRecord();
                    int id = int.Parse(DataReader["moveid"].ToString());
                    int numbers = int.Parse(DataReader["numbers"].ToString());
                    DateTime time = (DateTime)DataReader["time"];
                    int warehouseid = int.Parse(DataReader["warehouseid"].ToString());
                    int employeeid = int.Parse(DataReader["employeeid"].ToString());

                    moverecord.RemoveID = id;
                    moverecord.RemoveTotal = numbers;
                    moverecord.RemoveTime = time;
                    moverecord.WareHouse.WareHouseID = warehouseid;
                    moverecord.Employee.EmployeeID = employeeid;

                    MoveRecordList.Add(moverecord);
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
            return MoveRecordList;
        }

        //只负责初始化员工的id，仓库的id
        List<MoveRecord> IMoveRecordDao.getAllMoveRecordList(DateTime starttime, DateTime endtime)
        {
            List<MoveRecord> MoveRecordList = new List<MoveRecord>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select * from move where time between :S_time and :E_time";
                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(":S_time", OracleDbType.TimeStamp);
                cmd.Parameters.Add(":E_time", OracleDbType.TimeStamp);
                cmd.Parameters[0].Value = starttime;
                cmd.Parameters[1].Value = endtime;
                OracleDataReader DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {
                    MoveRecord moverecord = new MoveRecord();
                    int id = int.Parse(DataReader["moveid"].ToString());
                    int numbers = int.Parse(DataReader["numbers"].ToString());
                    DateTime time = (DateTime)DataReader["time"];
                    int warehouseid = int.Parse(DataReader["warehouseid"].ToString());
                    int employeeid = int.Parse(DataReader["employeeid"].ToString());

                    moverecord.RemoveID = id;
                    moverecord.RemoveTotal = numbers;
                    moverecord.RemoveTime = time;
                    moverecord.WareHouse.WareHouseID = warehouseid;
                    moverecord.Employee.EmployeeID = employeeid;

                    MoveRecordList.Add(moverecord);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return MoveRecordList;
        }

        DataSet IMoveRecordDao.getMyRemoveRecord(int EmployeeID)
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select moveid as 移出编号,numbers as 移出商品总数,time as 移出时间,warehouseid as 仓库编号 from move where employeeid=:id";
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);

                DataAdapter.SelectCommand.Parameters[0].Value = EmployeeID;

                
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
        DataSet IMoveRecordDao.getMyRemoveRecord(int employeeID, DateTime starttime, DateTime endtime)
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select moveid as 移出编号,numbers as 移出商品总数,time as 移出时间,warehouseid as 仓库编号 from move where employeeid=:id and time between :S_time and :E_time";
               
                
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                DataAdapter.SelectCommand.Parameters.Add(":S_time", OracleDbType.TimeStamp);
                DataAdapter.SelectCommand.Parameters.Add("E_time", OracleDbType.TimeStamp);
                DataAdapter.SelectCommand.Parameters[0].Value = employeeID;
                DataAdapter.SelectCommand.Parameters[1].Value = starttime;
                DataAdapter.SelectCommand.Parameters[2].Value = endtime;
                DataAdapter.Fill(set);
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

        MoveRecord IMoveRecordDao.getSingleMoveRecord(int RemoveID)
        {
            MoveRecord MoveRecord = new MoveRecord();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select * from move where moveid = :moveid";
                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(":moveid", OracleDbType.Int32);

                cmd.Parameters[":moveid"].Value = RemoveID;

                OracleDataReader DataReader = cmd.ExecuteReader();
               if (DataReader.Read())
                {

                    int id = int.Parse(DataReader["moveid"].ToString());
                    int numbers = int.Parse(DataReader["numbers"].ToString());
                    DateTime time = (DateTime)DataReader["time"];
                    int warehouseid = int.Parse(DataReader["warehouseid"].ToString());
                    int employeeid = int.Parse(DataReader["employeeid"].ToString());

                    MoveRecord.RemoveID = id;
                    MoveRecord.RemoveTotal = numbers;
                    MoveRecord.RemoveTime = time;
              //    MoveRecord.WareHouse.WareHouseID = warehouseid;
                    IEmployeeDao eDao = new EmployeeDao();
                    //     MoveRecord.Employee = eDao.getSingleEmployee(employeeid);
                    MoveRecord.Employee.EmployeeID = employeeid;
                }
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
            return MoveRecord;
        }

        DataSet IMoveRecordDao.getDetailedMoveInfo(int MoveRecordid)
        {
            string sql = "select  move.moveid   as 移货编号 ,move_good.amount as 移出数量 ,good.goodid as 商品编号 ,good.name as 商品名称 from good,move,move_good where good.goodid = move_good.goodid and move.moveid = move_good.moveid "
                +" and move.moveid = :id ";
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                DataAdapter.SelectCommand.Parameters[0].Value = MoveRecordid;
                //   DataAdapter.SelectCommand.Parameters.Add(":")
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

        DataSet IMoveRecordDao.getMove(int MoveRecordid)
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select moveid as 移出编号,numbers as 移出商品总数,time as 移出时间,warehouseid as 仓库编号,employeeid as 员工编号 from move where moveid = :id";
         //       OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                DataAdapter.SelectCommand.Parameters[0].Value = MoveRecordid;
                DataAdapter.Fill(set);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return set;
        }
    }
}
