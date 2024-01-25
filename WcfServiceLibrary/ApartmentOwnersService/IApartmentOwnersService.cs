using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary.ApartmentOwnersService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IApartmentOwnersService" in both code and config file together.
    [ServiceContract]
    public interface IApartmentOwnersService
    {
        [OperationContract]
        void addApartmentOwner(string fullName,int objectId,string ownersshipShare);

        [OperationContract]
        void editApartmentOwner(int Id,string fullName, int objectId, string ownersshipShare);

        [OperationContract]
        void deleteApartmentOwner(int Id);

    }
}
