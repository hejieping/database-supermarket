using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using SuperMarket.DataAccess;
using SuperMarket.Entity;
using SuperMarket.MyConnection;
namespace SuperMarket.DataAccessLayer
{
    class GoodDao : IGoodDao
    {
        private OracleConnection con;
        public GoodDao()
        {
            con = DBConnection.getConnection();
        }

        public DataSet getAllWareHouseStorage(int goodiID)
        {
            DataSet set = new DataSet();
            string sql = "select good.goodid as 商品编号 ,good.name as 商品名 ,warehouse.warehouseid as 仓库id ,warehouse.name as 仓库名 ,warehouse.address as 仓库地址, warehouse_good.amount  as 仓库库存 from " +
                "warehouse,warehouse_good,good where good.goodid = warehouse_good.goodid and warehouse_good.warehouseid = warehouse.warehouseid and good.goodid = :id";
            try
            {
                OracleDataAdapter DataAdapter = new OracleDataAdapter(sql, con);
                DataAdapter.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                DataAdapter.SelectCommand.Parameters[0].Value = goodiID;
                DataAdapter.Fill(set);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return set;
        }

        public DataSet getSingleGoodDataSet(int GoodID)
        {
            DataSet set = new DataSet();
            string sql = "select goodid as 商品编号,name as 商品名称 ,price as 商品价格 ,discount as 商品折扣 from good where goodid =:id";
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleDataAdapter da = new OracleDataAdapter(sql, con);
                da.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                da.SelectCommand.Parameters[0].Value = GoodID;
                da.Fill(set);
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

        public DataSet getSingleGoodInfoDataSet(int GoodID)
        {
            DataSet set = new DataSet();
            string sql = "select shelf.shelfid as 所在货架编号,shelf.name as 所在货架名称 ,type.typeid as 商品类别编号 ,type.name as 商品类别名 from good,shelf,type where good.typeid = type.typeid and goodid =:id";
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleDataAdapter da = new OracleDataAdapter(sql, con);
                da.SelectCommand.Parameters.Add(":id", OracleDbType.Int32);
                da.SelectCommand.Parameters[0].Value = GoodID;
                da.Fill(set);
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

        bool IGoodDao.addGood(Good good)
        {
            string sql = "insert into good values(:id,:name,:price,:discount" +
                ",:shelid ,:amount,:typeid,:shelfmin,:warehousemin)";
            OracleCommand cmd = new OracleCommand(sql, con);
            int result = -1;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd.Parameters.Add(":id", OracleDbType.Int32);
                cmd.Parameters.Add(":name", OracleDbType.Varchar2);
                cmd.Parameters.Add(":price", OracleDbType.Decimal);
                cmd.Parameters.Add(":discount", OracleDbType.Decimal);
                cmd.Parameters.Add(":shelid", OracleDbType.Int32);
                cmd.Parameters.Add(":amount", OracleDbType.Int32);
                cmd.Parameters.Add(":typeid", OracleDbType.Int32);
                cmd.Parameters.Add("shelfmin", OracleDbType.Int32);
                cmd.Parameters.Add("warehousemin", OracleDbType.Int32);
                cmd.Parameters[0].Value = good.GoodID;
                cmd.Parameters[1].Value = good.Name;
                cmd.Parameters[2].Value = good.Price;
                cmd.Parameters[3].Value = good.Discount;
                cmd.Parameters[4].Value = good.Shelf.SelfID;
                cmd.Parameters[5].Value = good.Amount_in_shelf;
                cmd.Parameters[6].Value = good.Type.TypeID;
                cmd.Parameters[7].Value = good.ShelfMin;
                cmd.Parameters[8].Value = good.WareHouseMin;
                result = cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
            }
            if (result != -1) return true;
            return false;     
        }

        bool IGoodDao.deleteGood(Good GoodID)
        {
            throw new NotImplementedException();
        }

        bool IGoodDao.deleteGood(int GoodID)
        {
           throw new NotImplementedException();
        }

        DataSet IGoodDao.getAllGoodBriefInfo()
        {
            DataSet set = new DataSet();
            string sql = "select goodid as 商品编号,name as 商品名称 ,price as 商品价格 from good";
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleDataAdapter da = new OracleDataAdapter(sql, con);
                da.Fill(set);
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

        List<Good> IGoodDao.getAllGoodList()
        {
            throw new NotImplementedException();
            /*      DataSet set = new DataSet();
                  string sql = "select goodid as 商品编号,name as 商品名称 ,price as 商品价格 from good";
                  try
                  {
                      if (con.State == ConnectionState.Closed)
                          con.Open();
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
                  return set;*/
        }

        DataSet IGoodDao.getAllGoodsInfo()
        {
            DataSet set = new DataSet();
            string sql = "select goodid as 商品编号,good.name as 商品名称 ,price as 商品价格 ,discount as 商品折扣,amount as 商品货架余量, type.name as 商品类别,Mview_good_storage.quantity" +
                " from good,type,mview_good_storage,where good.typeid = type.typeid and Mview_good_storage.goodid = good.goodid";
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
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

        Good IGoodDao.getSingleGood(int goodID)
        {
            Good good = null;
            string sql = "select * from good where goodid = :id";
            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(":id", OracleDbType.Int32);
            cmd.Parameters[0].Value = goodID;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleDataReader datareader = cmd.ExecuteReader();
                while(datareader.Read())
                {
                    good = new Good();
                    good.GoodID = int.Parse(datareader["goodid"].ToString());
                    good.Name = datareader["name"].ToString();
                    good.Price = decimal.Parse(datareader["price"].ToString());
                    good.ShelfMin = int.Parse(datareader["shelfmin"].ToString());
                     good.Shelf.SelfID = int.Parse(datareader["shelid"].ToString());
             /*       using (IShelfDao sdao = new ShelfDao())
                    {
                        good.Shelf = sdao.getShelf(s)
                            }*/
                        good.Type.TypeID = int.Parse(datareader["typeid"].ToString());
                    good.WareHouseMin = int.Parse(datareader["warehousemin"].ToString());
                }

        /*        if(good!=null)
                {
                    con.Close();
                    con.Open();
                    string str = "select quantity from mview_good_storage where goodid = :id";
                    OracleCommand cmdd = new OracleCommand(str, con);
                    cmdd.Parameters.Add(":id", OracleDbType.Int32);
                    cmdd.Parameters[0].Value = goodID;
                    datareader = cmdd.ExecuteReader();
                    good.Total_in_warehouse = int.Parse(datareader["quantity"].ToString());
                }*/
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return good;
        }

        DataSet IGoodDao.vagueGetGood(string goodname)
        {
            DataSet set = new DataSet();
            string sql = "select goodid as 商品编号,name as 商品名称 ,price as 商品价格 ,discount as 商品折扣 from good where name like '%"+goodname+"%'";
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OracleDataAdapter da = new OracleDataAdapter(sql, con);
                da.Fill(set);
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
    }
}
