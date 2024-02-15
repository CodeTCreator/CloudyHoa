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
        void AddStaticParam(string value, int typeParams, int propertyId, int objectId);

        [OperationContract]
        void DeleteStaticParam(int paramId);

        [OperationContract]
        DataSet GetStaticParam(int paramId);

        // Получить актуальный параметр (последний по дате обновления)
        [OperationContract]
        DataSet GetCurrentStaticParam(int propId);

        // Получить все актуальные параметры
        [OperationContract]
        DataSet GetCurrentStaticParams(int objectId);

        // Получить старые параметры
        [OperationContract]
        DataSet GetOldStaticParams(int objectId);

        [OperationContract]
        DataSet GetSchemeStaticParams(int hoaId, int typeObject);

    }
}
