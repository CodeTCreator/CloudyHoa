using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICalculationsService" in both code and config file together.
    [ServiceContract]
    public interface ICalculationsService
    {
        [OperationContract]
        DataSet GetCalculations(int objectId, DateTime period);

        [OperationContract]
        DataSet GetTemplate(int propId, DateTime period);

        [OperationContract]
        void AddCalculation(int propId, DateTime period, float value, int paId);

        [OperationContract]
        void EditCalculation(int propId, DateTime period, float value, int paId, int id);

        [OperationContract]
        void DeleteCalcuation(int id);
    }
}
