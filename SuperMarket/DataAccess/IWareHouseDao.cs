using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SuperMarket.Entity;
namespace SuperMarket.DataAccess
{
    interface IWareHouseDao
    {   
        //得到仓库的所有信息
        DataSet getAllWareHouseInfo();
        
        //更新仓库
        bool updateWareHouse(WareHouse warehouse);
       
        //添加仓库
        bool addWareHouse(WareHouse WareHouse);
       
        //得到仓库的列表
        List<WareHouse> getWareHouseList();
        
        //得到仓库的提醒信息
        DataSet getWarehouseNoticeInfo(int employeeID);

        //得到所有仓库列表
  //      List<WareHouse> getWarehouseNoticeList(int employeeID);

        //得到某个商品在仓库的信息
        DataSet getGoodsInWareHouse(int warehouseID);


    }
}
