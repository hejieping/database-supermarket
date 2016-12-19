using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SuperMarket.Entity;
namespace SuperMarket.DataAccess
{
    interface IRemoveRecordDao
    {
        bool addmove(MoveRecord moveRecord);
        DataSet getMyRemoveRecord(int EmployeeID);
        DataSet getMyRemoveRecord(int employeeID, DateTime starttime, DateTime endtime);
  //      DataSet getMoveInfo(int RemoveID);
        MoveRecord getSingleMoveRecord(int RemoveID);
        List<MoveRecord> getAllMoveRecords();
        DataSet getAllMoveRecordList();
        List<MoveRecord> getAllMoveRecords(DateTime starttime, DateTime endtime);
        DataSet getAllMoveRecordList(DateTime starttime, DateTime endtime);
    }
}
