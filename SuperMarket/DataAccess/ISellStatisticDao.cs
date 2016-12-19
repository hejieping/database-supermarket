using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarket.Entity;
namespace SuperMarket.DataAccess
{
    interface ISellStatisticDao
    {
        //得到销售排行前5的商品
        List<SellStatistic> getAllTopFive(DateTime starttime,DateTime endtime);
        
        //得到某一类销售排行前5的商品
        List<SellStatistic> getTopFiveByType(DateTime starttime, DateTime endTime, int typeID);
    }
}
