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
    class WareHouseDao : IWareHouseDao
    {
        private OracleConnection con = null;

        public WareHouseDao()
        {
            con = DBConnection.getConnection();
        }

        bool IWareHouseDao.addWareHouse(Entity.WareHouse WareHouse)
        {
            int flag = -1;
            if (con.State == ConnectionState.Closed)
                con.Open();

            try
            {
                string sql = "insert  into Warehouse values(:warehouseid,:name,:address)";
                OracleCommand cmd = new OracleCommand(sql, con);

                cmd.Parameters.Add(":warehouseid", OracleDbType.Int32);
                cmd.Parameters.Add(":name", OracleDbType.Varchar2);
                cmd.Parameters.Add(":address", OracleDbType.Varchar2);

                cmd.Parameters[0].Value = WareHouse.WareHouseID;
                cmd.Parameters[1].Value = WareHouse.Name;
                cmd.Parameters[2].Value = WareHouse.Address;
                
                flag = cmd.ExecuteNonQuery();
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

            if (flag != -1) return true;
            else return false;
        }

        DataSet IWareHouseDao.getAllWareHouseInfo()
        {
            DataSet set = new DataSet();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string sql = "select warehouseID as 仓库编号 ,name as 仓库名称 ,address as 仓库地址 from warehouse";
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
                con.Close();
            }
            return set;
        }

        DataSet IWareHouseDao.getGoodsInWareHouse(int warehouseID)
        {
            DataSet goods_set = new DataSet();
            if (con.State == ConnectionState.Closed)
                con.Open();

            try
            {
                /*       string sql = "select goodID, name, price, discound, total_in_warehouse"
                           + " from good " 
                           + " where goodID in "
                           + " ( select goodid "
                           + " from warehouse_good "
                           + " where warehouseid = :warehouseID )";*/
                string sql = "select good.goodid as 商品编号 ,good.name as 商品名称 ,warehouse_good.amount as 商品库存量 from "
                    + " good ,warehouse_good where good.goodid = warehouse_good.goodid and warehouse_good.warehouseid = :id";
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                DataAdapter.SelectCommand.Parameters[0].Value = warehouseID;
                DataAdapter.Fill(goods_set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return goods_set;
        }


        List<Entity.WareHouse> IWareHouseDao.getWareHouseList()
        {
            List<WareHouse> WareHouseList = new List<WareHouse>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string sql = "select * from warehouse";
                OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataReader DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {
                    WareHouse warehouse = new WareHouse();

                    int m_wh_id = int.Parse(DataReader["warehouseid"].ToString());
                    string m_wh_name = DataReader["name"].ToString();
                    string m_wh_address = DataReader["address"].ToString();

                    warehouse.WareHouseID = m_wh_id;
                    warehouse.Name = m_wh_name;
                    warehouse.Address = m_wh_address;

                    WareHouseList.Add(warehouse);
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
            return WareHouseList;
        }

        DataSet IWareHouseDao.getWarehouseNoticeInfo(int employeeID)
        {
            DataSet warehouse_set = new DataSet();
            if (con.State == ConnectionState.Closed)
                con.Open();

            try
            {
                /*         string sql = "select warehouseID, name, address"
                             + "from warehouse"
                             + "where warehouseid in ( select warehouseid from warehousenotice nature join warehouse_good where employeeid = :employeeid)";*/
                //     OracleCommand cmd = new OracleCommand(sql, con);
                string sql = "select goodid as 商品编号 ,name as 商品名称 ,quantity as 商品总库存 ,warehousemin as 允许最低库存 from warehousenotice natural join good natural join mview_good_storage where employeeid = :id";
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                DataAdapter.SelectCommand.Parameters[0].Value = employeeID;
                DataAdapter.Fill(warehouse_set);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                con.Close();
            }
            return warehouse_set;
        }

   /*     List<Entity.WareHouse> IWareHouseDao.getWarehouseNoticeList(int employeeID)
        {
            List<WareHouse> m_wh = new List<WareHouse>();
            if (con.State == ConnectionState.Closed)
                con.Open();

            try
            {
                string sql = "select warehouseID, name, address"
                    + "from warehouse"
                    + "where warehouseid in ( select warehouseid from warehousenotice nature join warehouse_good where employeeid = :employeeid)";
                OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataReader DataReader = cmd.ExecuteReader();

                while (DataReader.Read())
                {
                    WareHouse warehouse = new WareHouse();

                    int m_wh_id = (int)DataReader["warehouseID"];
                    string m_wh_name = DataReader["name"].ToString();
                    string m_wh_address = DataReader["address"].ToString();

                    warehouse.WareHouseID = m_wh_id;
                    warehouse.Name = m_wh_name;
                    warehouse.Address = m_wh_address;

                    m_wh.Add(warehouse);
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
            return m_wh;
        }*/


        bool IWareHouseDao.updateWareHouse(Entity.WareHouse warehouse)
        {
            int result = -1;
            string sql = "update warehouse set  name = :name, address = :address  where warehouseid = :id";
            if (con.State == ConnectionState.Closed)
                con.Open();
            try
            {
                OracleCommand cmd = new OracleCommand(sql, con);

                cmd.Parameters.Add(":name", OracleDbType.Varchar2);
                cmd.Parameters.Add(":address", OracleDbType.Varchar2);
                cmd.Parameters[0].Value = warehouse.Name;
                cmd.Parameters[1].Value = warehouse.Address;

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
    }
}
