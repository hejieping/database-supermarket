using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarket.DataAccess;
using SuperMarket.Entity;
using Oracle.DataAccess;
using Oracle.DataAccess.Client;
using System.Data;
using System.Windows.Forms;

using SuperMarket.MyConnection;

namespace SuperMarket.DataAccessLayer
{
    class ShelfDao : IShelfDao
    {
        private OracleConnection con;
        public ShelfDao()
        {
            con = DBConnection.getConnection();
        }


        bool IShelfDao.addShelf(Shelf shelf)
        {
            //throw new NotImplementedException();
            string sql = "insert  into Shelf values(:selfID,:name,:location,:employeeID)";
            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(":selfID", OracleDbType.Int32);
            cmd.Parameters.Add(":name", OracleDbType.Varchar2);
            cmd.Parameters.Add(":location", OracleDbType.Varchar2);
            cmd.Parameters.Add(":employeeID", OracleDbType.Int32);
            cmd.Parameters[0].Value = shelf.SelfID;
            cmd.Parameters[1].Value = shelf.ShelfName;
            cmd.Parameters[2].Value = shelf.Location;
            cmd.Parameters[3].Value = shelf.Employee.EmployeeID;
            int result = -1;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                con.Close();
            }
            if (result != -1)
                return true;
            return false;
        }

        List<Shelf> IShelfDao.getAllShelfList()
        {
            //throw new NotImplementedException();
            List<Shelf> ShelfList = new List<Shelf>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select * from shelf";
                OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataReader DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {
                    Shelf shelf = new Shelf();
                    int s_id = int.Parse(DataReader["shelfid"].ToString());
                    string name = DataReader["name"].ToString();
                    string location = DataReader["location"].ToString();
                    int e_id = int.Parse(DataReader["employeeid"].ToString());
                    Employee e = new Employee();
                    e.EmployeeID = e_id;
                    shelf.SelfID = s_id;
                    shelf.ShelfName = name;
                    shelf.Location = location;
                    shelf.Employee = e;
                    ShelfList.Add(shelf);
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
            return ShelfList;
        }

        DataSet IShelfDao.getAllShelvesInfo()
        {
            string sql = "select shelf.shelfid as 货架编号 ,shelf.name  as 货架名称 ,shelf.location as  货架位置, employee.name 负责员工 from shelf ,employee where shelf.employeeid = employee.employeeid ";
            DataSet set = new DataSet();
  //        OracleCommand cmd = new OracleCommand(sql, con);
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleDataAdapter da = new OracleDataAdapter(sql, con);
                da.Fill(set);
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

        Shelf IShelfDao.getShelf(Shelf shelf)                          //???
        {
            //throw new NotImplementedException();
            Shelf s = new Shelf();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select * from shelf where shelfid=" + shelf.SelfID;
                OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataReader DataReader = cmd.ExecuteReader();
                if (DataReader.Read())
                {
                    //Shelf shelf = new Shelf();
                    int s_id = int.Parse(DataReader["shelfid"].ToString());
                    string name = DataReader["name"].ToString();
                    string location = DataReader["location"].ToString();
                    int e_id = int.Parse(DataReader["employeeid"].ToString());
              //      Employee e = new Employee();
              //      e.EmployeeID = e_id;
                    s.SelfID = s_id;
                    s.ShelfName = name;
                    s.Location = location;
                    IEmployeeDao edao = new EmployeeDao();
                    s.Employee = edao.getSingleEmployee(e_id);                                //int型还是employee型?
                }
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
            return s;
        }

        Shelf IShelfDao.getShelf(int ShelfID)
        {
            //throw new NotImplementedException();
            Shelf s = new Shelf();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select * from shelf where shelfid=" + ShelfID;
                OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataReader DataReader = cmd.ExecuteReader();
                if (DataReader.Read())
                {
                    //Shelf shelf = new Shelf();
                    int s_id = int.Parse(DataReader["shelfid"].ToString());
                    string name = DataReader["name"].ToString();
                    string location = DataReader["location"].ToString();
                    int e_id = int.Parse(DataReader["employeeid"].ToString());
                    //      Employee e = new Employee();
                    //      e.EmployeeID = e_id;
                    s.SelfID = s_id;
                    s.ShelfName = name;
                    s.Location = location;
                    IEmployeeDao edao = new EmployeeDao();
                    s.Employee = edao.getSingleEmployee(e_id);                                //int型还是employee型?
                }
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
            return s;
        }

        DataSet IShelfDao.getShelfByEmployee(int EmployeeID)
        {
            string sql = "select shelf.shelfid as 货架编号 ,shelf.name  as 货架名称 ,shelf.location as  货架位置, employee.name 负责员工 from shelf ,employee where shelf.employeeid = employee.employeeid and employeeid.employeeid = :id";
            DataSet set = new DataSet();
            //        OracleCommand cmd = new OracleCommand(sql, con);
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleDataAdapter da = new OracleDataAdapter(sql, con);
                da.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                da.SelectCommand.Parameters[0].Value = EmployeeID;
                da.Fill(set);
            }
            catch (Exception e)
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

        DataSet IShelfDao.getShelfByShelfID(int ShelfID)
        {
            string sql = "select shelf.shelfid as 货架编号 ,shelf.name  as 货架名称 ,shelf.location as  货架位置, employee.name 负责员工 from shelf ,employee where shelf.employeeid = employee.employeeid and shelf.shelfid = :id";
            DataSet set = new DataSet();
            //        OracleCommand cmd = new OracleCommand(sql, con);
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleDataAdapter da = new OracleDataAdapter(sql, con);
                da.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                da.SelectCommand.Parameters[0].Value = ShelfID;
                da.Fill(set);
            }
            catch (Exception e)
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

        DataSet IShelfDao.getShelfNoticeInfo(int EmployeeID)
        {
            //throw new NotImplementedException();
                 string sql = "select goodid as 商品编号 ,name as 商品名称 ,amount as 商品数量 ,shelfmin   as 商品在货架的最低数量  ,shelid as 货架编号 from shelfnotice natural join good where employeeid = :id";
   /*       string sql = "select good.goodid as 商品编号 ,good.name as 商品名称 ,good.amount as 商品数量 ,good.shelfmin   as 商品在货架的最低数量 ,shelf.name as 货架名称 ,shelf.location as 货架地址 from shelf,shelfnotice ，good  where good.shelid = shelf.shelfid " +
              "   and shelfnotice.goodid = good.goodid and shelfnotice.employeeid =:id";*/
            DataSet set = new DataSet(); 
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleDataAdapter da = new OracleDataAdapter(sql, con);
                da.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                da.SelectCommand.Parameters[0].Value = EmployeeID;
                da.Fill(set);
            }
            catch (Exception e)
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

        List<Shelf> IShelfDao.getShelfNoticeList(int EmployeeID)
        {
            throw new NotImplementedException();
        /*    List<Shelf> ShelfList = new List<Shelf>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select * from shelf where employeeid=" + EmployeeID;
                OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataReader DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {
                    Shelf shelf = new Shelf();
                    int s_id = int.Parse(DataReader["shelfid"].ToString());
                    string name = DataReader["name"].ToString();
                    string location = DataReader["location"].ToString();
                    int e_id = int.Parse(DataReader["employeeid"].ToString());
                    Employee e = new Employee();
                    e.EmployeeID = e_id;
                    shelf.SelfID = s_id;
                    shelf.ShelfName = name;
                    shelf.Location = location;
                    shelf.Employee = e;
                    ShelfList.Add(shelf);
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
            return ShelfList;*/
        }

        bool IShelfDao.updateShelf(Shelf shelf)
        {
            //throw new NotImplementedException();
            if (con.State == ConnectionState.Closed)
                con.Open();
            string sql = "update shelf set name=:shelfname,location=:location,employeeid=:employeeID where shelfid=" + shelf.SelfID;
            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(":shelfname", OracleDbType.Varchar2);
            cmd.Parameters.Add(":location", OracleDbType.Varchar2);
            cmd.Parameters.Add(":employeeID", OracleDbType.Int32);
            cmd.Parameters[0].Value = shelf.ShelfName;
            cmd.Parameters[1].Value = shelf.Location;
            cmd.Parameters[2].Value = shelf.Employee.EmployeeID;
            int result = -1;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            if (result != -1)
                return true;
            return false;
        }
    }
}
