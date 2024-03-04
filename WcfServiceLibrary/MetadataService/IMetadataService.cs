using DevExpress.Data.TreeList;
using System.Data;
using System.ServiceModel;

namespace WcfServiceLibrary.MetadataService
{
    [ServiceContract]
    public interface IMetadataService
    {
        [OperationContract]
        void AddParameter(int hoaId, int typeObject, string propertyName, int typeProperty, string systemName, string formula,
            bool staticParam, bool calculated);

        [OperationContract]
        void DeleteParameter(int parameterId);

        [OperationContract]
        void EditParameter(int parameterId, string propertyName, int typeProperty, string systemName, string formula,
            bool staticParam, bool calculated);

        // Добавление и удаление типа объекта
        [OperationContract]
        void AddTypeObject(string nameType, int parentType, int hoaId);

        [OperationContract]
        void EditTypeObject(int typeId, string nameType, int parentType);

        [OperationContract]
        void DeleteTypeObject(int typeId);

        [OperationContract]
        DataSet GetTypesObjects();

        [OperationContract]
        DataSet GetParameters(int typeObject);

    }
}
