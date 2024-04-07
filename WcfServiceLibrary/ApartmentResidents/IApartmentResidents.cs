using Devart.Data.PostgreSql;
using System;
using System.Data;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel;

namespace WcfServiceLibrary.ApartmentOwnersService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IApartmentOwnersService" in both code and config file together.
    [ServiceContract]
    public interface IApartmentResidents
    {
        [OperationContract]
        void AddResident(string fullName,int objectId,bool registered, DateTime? registration_date,
            bool residence, DateTime? checkInDate, bool owner, int? numenator, int? denominator);

        [OperationContract]
        void EditResident(int Id, string fullName, int objectId, bool registered, DateTime? registration_date,
            bool residence, DateTime? checkInDate, bool owner, int? numenator, int? denominator);

        [OperationContract]
        void DeleteResident(int Id);

        [OperationContract]
        DataSet GetResidents(int objectId);

    }
}
