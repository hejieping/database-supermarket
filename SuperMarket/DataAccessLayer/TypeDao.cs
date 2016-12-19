using System;
using System.Collections.Generic;
using SuperMarket.DataAccess;
using Oracle.DataAccess.Client;
using SuperMarket.Entity;
using System.Data;
using System.Windows.Forms;
using SuperMarket.MyConnection;

namespace SuperMarket.DataAccessLayer
{
    class TypeDao : ITypeDao
    {
        private OracleConnection con = null;
        public TypeDao()
        {
            con = DBConnection.getConnection();
        }

        bool ITypeDao.addType(Entity.Type type)
        {
            string sql = "insert  into type values(null,:name,:employeeid)";
            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(":name", OracleDbType.Varchar2);
            cmd.Parameters.Add(":employeeid", OracleDbType.Int32);
            cmd.Parameters[0].Value = type.Name;
            cmd.Parameters[1].Value = type.Employee.EmployeeID;
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
                con.Close();
            }
            if (result != -1)
                return true;
            return false;
        }

        bool ITypeDao.deleteType(Entity.Type type)
        {
            List<int> GoodList = new List<int>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select goodid from good where typeid="+type.TypeID;
                OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataReader DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {          
                    GoodList.Add(int.Parse(DataReader["goodid"].ToString()));
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
            if (GoodList.Count > 0)
                return false;
            else
            {
                string sql = "delete from type where typeid=:id";
                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(":id", OracleDbType.Int32);
                cmd.Parameters[0].Value = type.TypeID;
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
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                if (result != -1)
                    return true;
                return false;
            }
        }

        DataSet ITypeDao.getAllTypes()
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select typeid as 类别编号 ,type.name as 类别名 ,employee.name as 负责员工 from type ,employee where employee.employeeid = type.employeeid";
        //        OracleCommand cmd = new OracleCommand(sql, con);
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

        List<Entity.Type> ITypeDao.getTypeList()
        {
            List<Entity.Type> TypeList = new List<Entity.Type>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select * from type";
                OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataReader DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {
                    Entity.Type type = new Entity.Type();
                    int id = int.Parse(DataReader["typeid"].ToString());
                    string name = DataReader["name"].ToString();
                    int employeeid = int.Parse(DataReader["employeeid"].ToString());
                    Employee employee = new Employee();
                    employee.EmployeeID = employeeid;
                    type.TypeID = id;
                    type.Name = name;
                    type.Employee = employee ;
                    TypeList.Add(type);
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
            return TypeList;
        }

        bool ITypeDao.updateType(Entity.Type type)
        {
            string sql = "update type set employeeid = :id , name = :name where typeid ="+type.TypeID;
            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(":id", OracleDbType.Int32);
            cmd.Parameters.Add(":name", OracleDbType.Varchar2);
            cmd.Parameters[0].Value = type.Employee.EmployeeID;
            cmd.Parameters[1].Value = type.Name;
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

        Entity.Type ITypeDao.getSingleType(int typeid)
        {
            Entity.Type type = null;
            string sql = "select * from type where typeid = :id";
            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add("id", OracleDbType.Int32);
            cmd.Parameters[0].Value = typeid;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleDataReader DataReader = cmd.ExecuteReader();
                if (DataReader.Read())
                {
                    type = new Entity.Type();
                    int id = int.Parse(DataReader["typeid"].ToString());
                    string name = DataReader["name"].ToString();
                    int employeeid = int.Parse(DataReader["employeeid"].ToString());
                    //      Employee employee = new Employee();
                    // employee.EmployeeID = employeeid;
                    IEmployeeDao dao = new EmployeeDao();
                    type.Employee = dao.getSingleEmployee(employeeid);
                    type.TypeID = id;
                    type.Name = name;
                    //       type.Employee = employee;
                }
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
            return type;
        }

        DataSet ITypeDao.getGoodbytype(int typeid)
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select good.goodid  as 商品编号,good.name as 商品名 ,type.name  as 类别名称 from good ,type where good.typeid = type.typeid and type.typeid = :id ";

                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                DataAdapter.SelectCommand.Parameters[0].Value = typeid;
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
    }
}
