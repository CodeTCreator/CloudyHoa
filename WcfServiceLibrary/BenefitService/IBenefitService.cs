using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary.BenefitService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBenefitService" in both code and config file together.
    [ServiceContract]
    public interface IBenefitService
    {
        [OperationContract]
        void AddBenefit(int typeBenefit,int objectId, int metadataId);
        
        [OperationContract]
        void EditBenefit(int benefitId,int typeBenefit, int objectId, int metadataId);

        [OperationContract]
        void DeleteBenefit(int benefitId);

        [OperationContract]
        void AddTypeBenefit(string name, float value, int hoaId);

        [OperationContract]
        void EditTypeBenefit(int typeBenefitId, string name, float value, int hoaId);

        [OperationContract]
        void DeleteTypeBenefit(int typeBenefitId);

        [OperationContract]
        DataSet GetTypeBenefits(int hoaId);

        [OperationContract]
        DataSet GetBenefitsForObject(int objectId);

        [OperationContract]
        DataSet GetBenefitsForMetadata(int metadataId);

        [OperationContract]
        DataSet GetBenefits(int objectId, int metadataId);
    }
}
