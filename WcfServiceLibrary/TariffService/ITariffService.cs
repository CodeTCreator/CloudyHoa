using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary.TariffService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITariffService" in both code and config file together.
    [ServiceContract]
    public interface ITariffService
    {
        [OperationContract]
        void AddTariff(string name, float value, int metadataId);

        [OperationContract]
        void EditTariff(int tariffId, string name, float value, int metadataId);

        [OperationContract]
        void DeleteTariff(int tariffId);

        [OperationContract]
        DataSet GetTariffs(int hoaId);
    }
}
