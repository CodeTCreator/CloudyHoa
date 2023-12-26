using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Devart;
using Devart.Data.PostgreSql;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        DataTable GetData(string nameTable);

        [OperationContract]
        bool CheckConnection();

        [OperationContract]
        int GetNumRow();

        // TODO: Add your service operations here
    }
}
