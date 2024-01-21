using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary.DPService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDynamicParamsService" in both code and config file together.
    [ServiceContract]
    public interface IDynamicParamsService
    {
        [OperationContract]
        DataSet OldDynamicParams(int hoaId);

        [OperationContract]
        DataSet SchemeDPTable();

        [OperationContract]
        DataSet BoneDynamicParams(int typeObject, int hoaId);
    }
}
