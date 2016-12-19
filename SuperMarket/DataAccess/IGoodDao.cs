using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarket.Entity;
using System.Data;
namespace SuperMarket.DataAccess
{
    interface IGoodDao
    {
        //不删除商品
        bool deleteGood(int GoodID);
        bool deleteGood(Good GoodID);

        //得到所有商品的信息
        DataSet getAllGoodsInfo();
        
        //以列表的形式返回所有商品，可能不怎么需要
        List<Good> getAllGoodList();
        
        //通过商品id得到单个商品，包含货架负责人，包括商品类别
        Good getSingleGood(int goodID);
        
        //添加一件商品，负责初始化商品的所有信息，包含货架id，类别id
        bool addGood(Good good);

        //得到商品的简要信息
        DataSet getAllGoodBriefInfo();

        //通过商品名称模糊查询商品
        DataSet vagueGetGood(string goodname);//模糊查询

        DataSet getSingleGoodDataSet(int GoodID);

        DataSet getSingleGoodInfoDataSet(int GoodID);

        DataSet getAllWareHouseStorage(int goodiID);
    }
}
