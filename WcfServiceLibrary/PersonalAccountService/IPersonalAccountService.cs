using System;
using System.Collections.Generic;
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
        void addPersonalAccount(string account, int object_id);
        [OperationContract]
        void deletePersonalAccount(int id);
        [OperationContract]
        void editPersonalAccount(int id, string number, int object_id);

    }
}
