using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary.CHService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICHService" in both code and config file together.
    [ServiceContract]
    public interface ICHService
    {
        [OperationContract]
        DataSet GetCalculationGistoryFromObject(int objectId, DateTime period);

        [OperationContract]
        DataSet GetCalculationGistoryFromPA(int paId, DateTime period);
    }
}
