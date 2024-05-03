using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary.MDService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMDService" in both code and config file together.
    [ServiceContract]
    public interface IMDService
    {
        [OperationContract]
        void AddMeteringDevice(string number, DateTime verification_date, DateTime installation_date, int service_id, int personal_account_id);

        [OperationContract]
        void EditMeteringDevice(int deviceId, string number, DateTime verification_date, DateTime installation_date, int service_id, int personal_account_id);

        [OperationContract]
        void DeleteMeteringDevice(int deviceId);

        [OperationContract]
        DataSet GetMeteringDevicesO(int objectId);

        [OperationContract]
        DataSet GetMeteringDevicesP(int personalAccountId);
    }
}
