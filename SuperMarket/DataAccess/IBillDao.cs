using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarket.Entity;
using System.Data;
namespace SuperMarket.DataAccess
{
    interface IBillDao
    {
        //添加一条账单， 负责初始化账单负责人id，
        //顾客对象（如果是非会员的话，就不初始化顾客）购买日期，
        //调用bill的addgood方法添加商品，必须初始化good的id，购买时单价即这个sellprice字段，或者(price , discount)这两个属性
        bool addBill(Bill bill);
        //以数据集的形式返回所有订单
        DataSet getAllBills();

        DataSet getAllBills(DateTime startTime, DateTime endTime,int employeeid);
        //以数据集的形式返回一段时间内的订单
        DataSet getAllBills(DateTime startTime, DateTime endTime);
        
        //通过订单id得到员工实例订单，暂时还没初始化订单的中的商品
        Bill getSingleBill(int BillID);
        //以列表的形式返回所有订单，感觉这个用不太上
        List<Bill> getAllBillList();

        //通过账单id得到一条账单的详细信息，返回结果集
        DataSet getBillDetailedInfo(int BillID);

        DataSet getBillInfo(int billid);
    }
}
