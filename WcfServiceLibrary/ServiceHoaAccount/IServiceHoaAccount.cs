using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceHoaAccount" in both code and config file together.
    [ServiceContract]
    public interface IServiceHoaAccount
    {
        [OperationContract]
        bool CheckAccout(string Name);

        [OperationContract]
        DataSet GetTableOfHoa();

        [OperationContract]
        void AddAccount(string Name,string Login, string Password);

        [OperationContract]
        void DeleteAccount(int Id);

        [OperationContract]
        void EditAccount(int Id, string Name, string Login, string Password);
    }
}
