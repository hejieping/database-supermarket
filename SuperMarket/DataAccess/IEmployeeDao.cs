using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarket.Entity;
using System.Data;
namespace SuperMarket.DataAccess
{
    //像这里的Employee删除函数只需要初始化一个id字段就行了外，
    interface IEmployeeDao
    {
        //通过账号和密码登录系统，不成功的话返回null
        Employee getEmployeeWithPasswd(string account,string password);

        //更新员工的信息，认为除了id，一切信息都能更新
        bool updateEmployee(Employee employee);

        //添加一个员工，一定要把所有信息初始化
        bool addEmployee(Employee employee);

        //通过id删除一个员工
        bool deleteEmployee(int employeeID);

        //删除一个员工，其实只需要初始化employee的id字段
        bool deleteEmployee(Employee employee);
        
        //以列表的形式返回所有员工，不设置员工照片，可能用得到这个函数
        List<Employee> getAllEmployeesList();

        //以结果集的形式返回所有员工
          DataSet getAllEmployees();

         DataSet getEmployee(int employeeid);

         DataSet getEmployee(string name);
        
        //更新员工的具体信息
        bool updatePassword(int employeeID, string oldpassword, string newpassword);
        
        //根据员工id得到员工具体的员工对象，初始化一切员工信息，包括照片
        Employee getSingleEmployee(int employeeID);
    
    }
}
