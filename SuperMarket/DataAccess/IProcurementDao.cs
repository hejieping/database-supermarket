using System;
using System.Collections.Generic;
using System.Data;
using SuperMarket.Entity;

namespace SuperMarket.DataAccess
{
    interface IProcurementDao
    {
        DataSet getMyProcurement(int employeeID);

        DataSet getProcurementInfo(int procurementID);

        Procurement getSingleProcurement(int procurementID);

        DataSet getMyProcurement(int employeeID,DateTime starttime,DateTime endtime);

        List<Procurement> getMyProcurementList(int employeeID, DateTime starttime, DateTime endtime);

        DataSet getAllProcurement( DateTime starttime, DateTime endtime);

        List<Procurement> getAllProcurementList( DateTime starttime, DateTime endtime);

        DataSet getAllProcurement();

        bool addProcurement(Procurement procurment);

        DataSet getSingleProDataSet(int procurmentID);
    }
}
