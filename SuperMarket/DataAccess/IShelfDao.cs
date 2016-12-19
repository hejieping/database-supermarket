using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarket.Entity;
using System.Data;

namespace SuperMarket.DataAccess
{
    interface IShelfDao
    {

        //添加一个货架，初始化货架的所有信息，包括负责员工对象，只需初始化该对象的id字段
        bool addShelf(Shelf shelf);

        //得到某个货架
        Shelf getShelf(int ShelfID);

        //得到某个货架，shelf只需要初始化id字段即可
        Shelf getShelf(Shelf shelf);
       
        //更新所有货架
        bool updateShelf(Shelf sherlf);
        
        //一数据集的形式返回所有货架
        DataSet getAllShelvesInfo();

        //以列表的形式返回所有货架
        List<Shelf> getAllShelfList();

        //得到货架不足提醒信息
        DataSet getShelfNoticeInfo(int EmployeeID);

        
        List<Shelf> getShelfNoticeList(int EmployeeID);

        DataSet getShelfByEmployee(int EmployeeID);

        DataSet getShelfByShelfID(int ShelfID);
    }
}
