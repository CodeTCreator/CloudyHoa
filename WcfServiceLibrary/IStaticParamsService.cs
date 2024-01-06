using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStaticParamsService" in both code and config file together.
    [ServiceContract]
    public interface IStaticParamsService
    {
        [OperationContract]
        void addStaticParam(string value, int typeParams, int propertyId, DateTime startPeriod, DateTime changingDate, int objectId);

        [OperationContract]
        void deleteStaticParam(int paramId);

        [OperationContract]
        DataSet getStaticParam(int paramId);

        // Получить актуальный параметр (последний по дате обновления)
        [OperationContract]
        DataSet getCurrentStaticParam(int paramId);

        // Получить все актуальные параметры
        [OperationContract]
        DataSet getCurrentStaticParams(int objectId);

        // Получить старые параметры
        [OperationContract]
        DataSet getOldStaticParams(int objectId);
    }
}
