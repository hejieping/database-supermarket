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
    class SellStatisticDao : ISellStatisticDao
    {
        private OracleConnection con = null;
        public SellStatisticDao()
        {
            con = DBConnection.getConnection();
        }

        List<SellStatistic> ISellStatisticDao.getAllTopFive(DateTime starttime, DateTime endtime)
        {
            //throw new NotImplementedException();
            List<SellStatistic> SellStatisticList = new List<SellStatistic>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
               string sql = "select * from ( select sum(quality) as amount,good.goodid as id ,good.name as name from bill , bill_good , good  where purchaseingDate >= :st and purchaseingDate <= :et and bill.billid = bill_good.billid and bill_good.goodid = good.goodid group by good.goodid,good.name order by amount desc ) where rownum<6  ";
        //        string sql = "select top 3 * from bill";
                OracleCommand cmd = new OracleCommand(sql,con);
               cmd.Parameters.Add(":st", OracleDbType.TimeStamp);
              cmd.Parameters.Add(":et", OracleDbType.TimeStamp);
                cmd.Parameters[0].Value = starttime;
                cmd.Parameters[1].Value = endtime;
                OracleDataReader DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {
                    SellStatistic sellstatistic = new SellStatistic();
               
                    Good good = new Good();
                    good.GoodID =int.Parse( DataReader["id"].ToString());
                    good.Name = DataReader["name"].ToString();
                    int number = int.Parse(DataReader["amount"].ToString());
                    sellstatistic.SellGood = good;
                    sellstatistic.SellQuantity = number;
                    SellStatisticList.Add(sellstatistic);
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
            return SellStatisticList;
        }

        List<SellStatistic> ISellStatisticDao.getTopFiveByType(DateTime starttime, DateTime endTime, int typeID)
        {
            //throw new NotImplementedException();
            List<SellStatistic> SellStatisticList = new List<SellStatistic>();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                //!!!
                string sql = "select * from ( select sum(quality) as amount,good.goodid as id ,good.name as name from bill , bill_good , good  where purchaseingDate >= :st and purchaseingDate <= :et and typeID = :tid and bill.billid = bill_good.billid and bill_good.goodid = good.goodid group by good.goodid,good.name order by amount desc ) where rownum<6  ";
                //string sql = "select top 5 sum(quality),name as amount from bill natural join bill_good natural join good where purchaseingDate >= :st and purchaseingDate <= :et and typeID = :id order by quality desc";
                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(":st", OracleDbType.TimeStamp);
                cmd.Parameters.Add(":et", OracleDbType.TimeStamp);
                cmd.Parameters.Add(":tid", OracleDbType.Int32);
                cmd.Parameters[0].Value = starttime;
                cmd.Parameters[1].Value = endTime;
                cmd.Parameters[2].Value = typeID;
                OracleDataReader DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {
                    SellStatistic sellstatistic = new SellStatistic();
                    Good good = new Good();
                    good.GoodID = int.Parse(DataReader["id"].ToString());
                    good.Name = DataReader["name"].ToString();
                    int number = int.Parse(DataReader["amount"].ToString());
                    sellstatistic.SellGood = good;
                    sellstatistic.SellQuantity = number;
                    SellStatisticList.Add(sellstatistic);
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
            return SellStatisticList;
        }
    }
}
