using System;
using Oracle.DataAccess.Client;
using System.Configuration;
using System.Windows;
namespace SuperMarket.MyConnection
{
    static class DBConnection
    {
        
        static OracleConnection OrclCon = null;
        private static String connString = "Data Source=ORCL;User ID = MDDATA; Password=oracle12c";
        //static String connString = ConfigurationSettings.AppSettings["connectionString"];
        public static OracleConnection getConnection()
        {
            if (OrclCon != null) return OrclCon;
            try
            {
                OrclCon = new OracleConnection(connString);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return OrclCon;
        }
    }
}
