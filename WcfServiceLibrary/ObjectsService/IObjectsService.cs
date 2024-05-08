using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IObjectsService" in both code and config file together.
    [ServiceContract]
    public interface IObjectsService
    {
        [OperationContract]
        void DeleteObject(int id);

        [OperationContract]
        void EditObject(int id, string objectNumber, int parentId);

        [OperationContract]
        int AddObject(int hoa_id,int type_object, string objectNumber, int parentId);

        [OperationContract]
        DataSet GetAllObjects(int hoaId);

        [OperationContract]
        DataSet GetObjectsStructure(int hoa_id);

        [OperationContract]
        DataSet GetAllChilds(int objectId, int typeObject);

        [OperationContract]
        DataSet GetPathObject(int objectId);
    }
}
