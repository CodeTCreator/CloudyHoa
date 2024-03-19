using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPersonalAccountService" in both code and config file together.
    [ServiceContract]
    public interface IPersonalAccountService
    {
        [OperationContract]
        void AddPersonalAccount(string account, int objectId);
        [OperationContract]
        void DeletePersonalAccount(int id);
        [OperationContract]
        void EditPersonalAccount(int id, string number, int objectId);

        [OperationContract]
        DataSet GetPersonalAccount(int hoaId);

    }
}
