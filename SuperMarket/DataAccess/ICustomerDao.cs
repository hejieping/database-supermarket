using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SuperMarket.Entity;

namespace SuperMarket.DataAccess
{
    interface ICustomerDao
    {
        //通过顾客id得到单个顾客，结账的时候根据这个打折
        Customer getSingleCustomer(int CustomerID);
        
        //返回一个普通顾客，结账时如果不是会员的话，不打折
        Customer getNormalCustomer();//普通顾客，非会员
        
        //以数据集的形式返回所有顾客信息
        DataSet getAllCustomerInfo();
       
        //以列表的形式得到所有顾客
        List<Customer> getAllCustomerList();

        //添加一个会员，默认积分为0，传过来的Customer对象必须初始化好（其实只需要注册日期），默认积分为0
        bool addCustomer(Customer customer);
    }
}
