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
        void AddDynamicParam(float value, DateTime period, int propertyId, int personalAccountId);

        [OperationContract]
        DataSet OldDynamicParams(int typeObject);

        [OperationContract]
        DataSet OldAllDynamicParams(int hoaId);

        [OperationContract]
        DataSet SchemeDPTable();

        [OperationContract]
        DataSet BoneDynamicParams(int typeObject);

        // Сделать работу с DataSet, чтобы только отправлять с сервера и отображать
        [OperationContract]
        DataSet DynamicParametersTable(int hoaId,int typeObject);

        // Шаблон для дочерних объектов, родительского объекта
        [OperationContract]
        DataSet BoneDynamicParamsFromChildrens(int objectId, int typeObject);
    }
}
