using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary.EHService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEHService" in both code and config file together.
    [ServiceContract]
    public interface IEHService
    {
        [OperationContract]
        DataSet GetEnteringHistoryFromObject(int objectId, DateTime period);

        [OperationContract]
        DataSet GetEnteringHistoryFromPA(int paId, DateTime period);

    }
}
