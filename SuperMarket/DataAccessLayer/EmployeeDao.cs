using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess;
using SuperMarket.DataAccess;
using SuperMarket.Entity;
using SuperMarket.MyConnection;
using System.Windows.Media.Imaging;
namespace SuperMarket.DataAccessLayer
{
    class EmployeeDao : IEmployeeDao
    {
        private OracleConnection con;
        public EmployeeDao()
        {
            con = DBConnection.getConnection();
        }
        bool IEmployeeDao.addEmployee(Employee employee)
        {
            int result = -1;
            string sql = "insert into employee values(:id,:name,:contactmethod,:address,:salary,:identity,"
                 + ":account ,:password,:photo";
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(":id", OracleDbType.Int32);
                cmd.Parameters.Add(":name", OracleDbType.Varchar2);
                cmd.Parameters.Add(":contactmethod", OracleDbType.Varchar2);
                cmd.Parameters.Add(":address", OracleDbType.Varchar2);
                cmd.Parameters.Add(":identity", OracleDbType.Varchar2);
                cmd.Parameters.Add(":salary", OracleDbType.Decimal);
                cmd.Parameters.Add(":account", OracleDbType.Varchar2);
                cmd.Parameters.Add(":password", OracleDbType.Varchar2);
                cmd.Parameters.Add(":photo", OracleDbType.Blob);
                cmd.Parameters[0].Value = employee.EmployeeID;
                cmd.Parameters[1].Value = employee.Name;
                cmd.Parameters[2].Value = employee.ContactMethod;
                cmd.Parameters[3].Value = employee.Address;
                cmd.Parameters[4].Value = employee.Identity;
                cmd.Parameters[5].Value = employee.Salary;
                cmd.Parameters[6].Value = employee.Account;
                cmd.Parameters[7].Value = employee.Password;
                byte[] imagedata = new byte[employee.Photo.StreamSource.Length];
                employee.Photo.StreamSource.Seek(0, System.IO.SeekOrigin.Begin);
                employee.Photo.StreamSource.Read(imagedata, 0, imagedata.Length);
                cmd.Parameters[8].Value = imagedata;
                result = cmd.ExecuteNonQuery();
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
            if (result != -1) return true;
            return false;
        }

        bool IEmployeeDao.deleteEmployee(Employee employee)
        {
            int result = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "delete from employee where employeeid = :id";
                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(":id", OracleDbType.Int32);
                cmd.Parameters[0].Value = employee.EmployeeID;
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
            return result != 0 ? true : false;
        }

        bool IEmployeeDao.deleteEmployee(int employeeID)
        {
            int result = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "delete from employee where employeeid = :id";
                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(":id", OracleDbType.Int32);
                cmd.Parameters[0].Value = employeeID;
                result = cmd.ExecuteNonQuery();
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
            return result != 0 ? true : false;
        }
        //以数据集的方式得到员工简略信息
        DataSet IEmployeeDao.getAllEmployees()
        {
            string sql = "select employeeID as 员工编号 ,name as 员工姓名 ,identity as 员工职位 from employee";
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
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


        //为了提高速度，照片就不读取了

        List<Employee> IEmployeeDao.getAllEmployeesList()
        {
            List<Employee> employees = new List<Employee>();
            
            string sql = "select * from employee ";
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataReader DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {
                    Employee employee = new Employee();
                    employee.EmployeeID = int.Parse(DataReader["employeeid"].ToString());
                    employee.Name = DataReader["name"].ToString();
                    employee.Account = DataReader["Account"].ToString();
                    employee.Identity = DataReader["identity"].ToString();
                    employee.Password = DataReader["password"].ToString();
                    employee.Salary = decimal.Parse(DataReader["salary"].ToString());
                    employee.ContactMethod = DataReader["contactmethod"].ToString();
                    employee.Address = DataReader["address"].ToString();
                    /*                byte[] imagedata = (byte[])DataReader["photo"];

                                    employee.Photo = getImageByByte(imagedata);*/
                    employee.Photo = null;
                    employees.Add(employee);
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
            return employees;
        }

        Employee IEmployeeDao.getEmployeeWithPasswd(string account, string password)
        {
            Employee employee = null;
            string sql = "select * from employee where account = :account and password = :password";
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(":account", OracleDbType.Varchar2);
                cmd.Parameters.Add(":password", OracleDbType.Varchar2);
                cmd.Parameters[0].Value = account;
                cmd.Parameters[1].Value = password;
                OracleDataReader DataReader =  cmd.ExecuteReader();
                while(DataReader.Read())
                {
                    employee = new Employee();
                    employee.EmployeeID = int.Parse(DataReader["employeeid"].ToString());
                    employee.Name = DataReader["name"].ToString();
                    employee.Account = DataReader["Account"].ToString();
                    employee.Identity = DataReader["identity"].ToString();
                    employee.Password = DataReader["password"].ToString();
                    employee.Salary = decimal.Parse(DataReader["salary"].ToString());
                    employee.ContactMethod = DataReader["contactmethod"].ToString();
                    employee.Address = DataReader["address"].ToString();
                    byte[] imagedata = (byte[])DataReader["photo"];

                    employee.Photo = getImageByByte(imagedata);
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
            return employee;
        }

        Employee IEmployeeDao.getSingleEmployee(int employeeID)
        {
            Employee employee = null;
            string sql = "select * from employee where employeeID = :id";
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(":id", OracleDbType.Int32);
                cmd.Parameters[0].Value = employeeID;
                OracleDataReader DataReader = cmd.ExecuteReader();
                while(DataReader.Read())
                {
                    employee = new Employee();
                    employee.EmployeeID = int.Parse(DataReader["employeeid"].ToString());
                    employee.Name = DataReader["name"].ToString();
                    employee.Account = DataReader["Account"].ToString();
                    employee.Identity = DataReader["identity"].ToString();
                    employee.Password = DataReader["password"].ToString();
                    employee.Salary = decimal.Parse(DataReader["salary"].ToString());
                    employee.ContactMethod = DataReader["contactmethod"].ToString();
                    employee.Address = DataReader["address"].ToString();
                    byte[] imagedata =(byte[])DataReader["photo"];

                    employee.Photo = getImageByByte(imagedata);
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
            return employee;
        }

        bool IEmployeeDao.updateEmployee(Employee employee)
        {
            int result = -1;
            string sql = "update  employee set name = :name,contactmethod = :contactmethod,address = :address,salary = :salary,identity = :identity,"
                 + "account = :account ,password = :password,photo = :photo where employeeID = :id";
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(":id", OracleDbType.Int32);
                cmd.Parameters.Add(":name", OracleDbType.Varchar2);
                cmd.Parameters.Add(":contactmethod", OracleDbType.Varchar2);
                cmd.Parameters.Add(":address", OracleDbType.Varchar2);
                cmd.Parameters.Add(":identity", OracleDbType.Varchar2);
                cmd.Parameters.Add(":salary", OracleDbType.Decimal);
                cmd.Parameters.Add(":account", OracleDbType.Varchar2);
                cmd.Parameters.Add(":password", OracleDbType.Varchar2);
                cmd.Parameters.Add(":photo", OracleDbType.Blob);
                cmd.Parameters[0].Value = employee.EmployeeID;
                cmd.Parameters[1].Value = employee.Name;
                cmd.Parameters[2].Value = employee.ContactMethod;
                cmd.Parameters[3].Value = employee.Address;
                cmd.Parameters[4].Value = employee.Identity;
                cmd.Parameters[5].Value = employee.Salary;
                cmd.Parameters[6].Value = employee.Account;
                cmd.Parameters[7].Value = employee.Password;
                byte[] imagedata = new byte[employee.Photo.StreamSource.Length];
                employee.Photo.StreamSource.Seek(0, System.IO.SeekOrigin.Begin);
                employee.Photo.StreamSource.Read(imagedata, 0, imagedata.Length);
                cmd.Parameters[8].Value = imagedata;
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
            if (result != -1) return true;
            return false;
        }

        bool IEmployeeDao.updatePassword(int employeeID, string oldpassword, string newpassword)
        {
            int result = -1;
            string sql = "update employee set password = :newpassword where employeeId = :employeeID and password = :oldpassword";
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(":newpassword", OracleDbType.Varchar2);
                cmd.Parameters.Add(":employeeid", OracleDbType.Int32);
                cmd.Parameters.Add(":oldpassword", OracleDbType.Int32);
                cmd.Parameters[0].Value = newpassword;
                cmd.Parameters[1].Value = employeeID;
                cmd.Parameters[2].Value = oldpassword;
                result = cmd.ExecuteNonQuery();
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
            if (result == 0||result==-1) return false;
            return true;
        }
        private BitmapImage getImageByByte(byte[] imagedata)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imagedata);
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            BitmapImage PhotoImage = new BitmapImage();

            PhotoImage.BeginInit();

            PhotoImage.StreamSource = ms;

            PhotoImage.EndInit();
            return PhotoImage;
        }


        DataSet IEmployeeDao.getEmployee(int employeeid)
        {
            string sql = "select employeeID as 员工编号 ,name as 员工姓名 ,identity as 员工职位 ,salary as  员工薪水 ,contactmethod as 联系方式 ,address as 地址 from employee"
                + " where employeeid = :id";
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                DataAdapter.SelectCommand.Parameters[0].Value = employeeid;
                DataAdapter.Fill(set);
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

        DataSet IEmployeeDao.getEmployee(string name)
        {
            string sql = "select employeeID as 员工编号 ,name as 员工姓名 ,identity as 员工职位 ,salary as  员工薪水 ,contactmethod as 联系方式 ,address as 地址 from employee"
               + " where name like  '%" + name + "%'";
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.Fill(set);
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
    }
}
