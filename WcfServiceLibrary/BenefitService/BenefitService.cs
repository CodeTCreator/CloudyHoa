using Devart.Data.PostgreSql;
using DevExpress.XtraEditors.Filtering.Templates;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WcfServiceLibrary.BenefitService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BenefitService" in both code and config file together.
    public class BenefitService : IBenefitService
    {

        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];

        public void AddBenefit(int typeBenefit, int objectId, int metadataId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO benefits (benefit_id, object_id, metadata_id) " +
                    "VALUES (@typeBenefit" +
                    ", @objectId, @metadataId)"
                    , conn);
                pgSqlCommand.Parameters.Add("@typeBenefit", typeBenefit);
                pgSqlCommand.Parameters.Add("@objectId", objectId);
                pgSqlCommand.Parameters.Add("@metadataId", metadataId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void AddTypeBenefit(string name, float value, int hoaId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO types_benefits (name, value, hoa_id) " +
                    "VALUES (@name, @value, @hoaId)"
                    , conn);
                pgSqlCommand.Parameters.Add("@name", name);
                pgSqlCommand.Parameters.Add("@value", value);
                pgSqlCommand.Parameters.Add("@hoaId", hoaId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void DeleteBenefit(int benefitId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from benefits where id = @benefitId", conn);
                pgSqlCommand.Parameters.Add("@benefitId", benefitId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void DeleteTypeBenefit(int typeBenefitId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from types_benefits where id = @typeId", conn);
                pgSqlCommand.Parameters.Add("@typeId", typeBenefitId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void EditBenefit(int benefitId, int typeBenefit, int objectId, int metadataId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("UPDATE benefits SET " +
                    " benefit_id = @typeBenefit, object_id = @objectId, metadata_id = @metadataId  " +
                    " where id = @benefitId", conn);
                pgSqlCommand.Parameters.Add("@benefitId", benefitId);
                pgSqlCommand.Parameters.Add("@typeBenefit", typeBenefit);
                pgSqlCommand.Parameters.Add("@objectId", objectId);
                pgSqlCommand.Parameters.Add("@metadataId", metadataId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void EditTypeBenefit(int typeBenefitId, string name, float value, int hoaId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("UPDATE types_benefits SET " +
                    " name = @name, value = @value, hoa_id = @hoaId" +
                    " where id = @typeBenefitId", conn);
                pgSqlCommand.Parameters.Add("@name", name);
                pgSqlCommand.Parameters.Add("@value", name);
                pgSqlCommand.Parameters.Add("@hoaId", hoaId);
                pgSqlCommand.Parameters.Add("@typeBenefitId", typeBenefitId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataSet GetBenefitsForMetadata(int metadataId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select * from benefits " +
                    "where metadata_id = @metadata_id"
                    , conn);
                pgSqlCommand.Parameters.Add("@metadata_id", metadataId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetBenefitsForObject(int objectId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select * from benefits " +
                    "where object_id = @objectId"
                    , conn);
                pgSqlCommand.Parameters.Add("@objectId", objectId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetTypeBenefits(int hoaId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select * from types_benefits " +
                    "where hoa_id = @hoa_id"
                    , conn);
                pgSqlCommand.Parameters.Add("@hoa_id", hoaId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetBenefits(int objectId, int metadataId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select benefits.*,name,value from benefits " +
                    " join types_benefits on types_benefits.id = benefits.benefit_id " +
                    "where object_id = @objectId and metadata_id = @metadataId"
                    , conn);
                pgSqlCommand.Parameters.Add("@objectId", objectId);
                pgSqlCommand.Parameters.Add("@metadataId", metadataId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }
    }
}
