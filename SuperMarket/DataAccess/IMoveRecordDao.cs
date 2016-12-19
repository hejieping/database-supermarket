using System;
using System.Collections.Generic;
using System.Data;
using SuperMarket.Entity;
namespace SuperMarket.DataAccess
{
    interface IMoveRecordDao
    {
        //添加一条移货记录，负责添加一条记录，初始化记录的移出时间，移出仓库，移出员工（只要id）就行，然后添加移出商品以及总数，
        bool addmove(MoveRecord moveRecord);
        
        //得到对应员工的移货记录
        DataSet getMyRemoveRecord(int EmployeeID);
        
        //得到对应员工一段时间内的移货记录
        DataSet getMyRemoveRecord(int employeeID, DateTime starttime, DateTime endtime);

        //    DataSet getMoveInfo(int RemoveID);

        //得到单个移货记录，可能没什么用，暂时还没初始化移货记录所有的商品
        MoveRecord getSingleMoveRecord(int RemoveID);
       
        //以列表的形式返回所有移货记录，可能没什么用
        List<MoveRecord> getAllMoveRecordList();
        
        //得到所有的移货记录，数据集的形式返回
        DataSet getAllMoveRecords();
        
        //得到一段时间内的移货记录，以列表的形式返回
        List<MoveRecord> getAllMoveRecordList(DateTime starttime, DateTime endtime);

        //得到一段时间内的所有移货记录
        DataSet getAllMoveRecords(DateTime starttime, DateTime endtime);

        //得到一条移货记录的详细信息
        DataSet getDetailedMoveInfo(int MoveRecordid);

        DataSet getMove(int MoveRecordid);

    }
}
