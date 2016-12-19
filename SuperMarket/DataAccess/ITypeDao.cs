using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarket.Entity;
using System.Data;
namespace SuperMarket.DataAccess
{
    interface ITypeDao
    {
       //得到所有的商品类别
       DataSet  getAllTypes();

        //修改商品类别的负责员工
        bool updateType(SuperMarket.Entity.Type type);
        //添加某一类商品
        bool addType(SuperMarket.Entity.Type type);

        //删除某一类商品
        bool deleteType(SuperMarket.Entity.Type type);
        
        //得到所有商品类别的列表
        List<SuperMarket.Entity.Type> getTypeList();

        Entity.Type getSingleType(int typeid);

        DataSet getGoodbytype(int typeid);
    }
}
