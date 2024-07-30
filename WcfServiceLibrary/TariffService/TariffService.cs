using Devart.Data.PostgreSql;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

namespace WcfServiceLibrary.TariffService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TariffService" in both code and config file together.
    public class TariffService : ITariffService
    {
        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];
        public void AddTariff(string name, float value, int metadataId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO types_tariffs (name, value, metadata_id) VALUES (@name,@value,@metadataId)"
                    , conn);
                pgSqlCommand.Parameters.Add("@name", name);
                pgSqlCommand.Parameters.Add("@value", value);
                pgSqlCommand.Parameters.Add("@metadataId", metadataId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void DeleteTariff(int tariffId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from types_tariffs where id = @tariffId"
                    , conn);
                pgSqlCommand.Parameters.Add("@tariffId", tariffId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void EditTariff(int tariffId, string name, float value, int metadataId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("UPDATE types_tariffs SET name = @name, value = @value," +
                    "metadata_id = @metadataId where id = @tariffId", conn);
                pgSqlCommand.Parameters.Add("@tariffId", tariffId);
                pgSqlCommand.Parameters.Add("@name", name);
                pgSqlCommand.Parameters.Add("@value", value);
                pgSqlCommand.Parameters.Add("@metadataId", metadataId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataSet GetServices(int hoaId, int type_object)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select * from metadata " +
                    "where hoa_id = @hoa_id and type_object = @type_object and static = false and calculated = true", conn);
                pgSqlCommand.Parameters.Add("@hoa_id", hoaId);
                pgSqlCommand.Parameters.Add("@type_object", type_object);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetTariffs(int hoaId, int? type_object)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("SELECT types_tariffs.*, metadata.property_name " +
                    "FROM types_tariffs " +
                    "JOIN metadata ON types_tariffs.metadata_id = metadata.id " +
                    "WHERE hoa_id = @hoaId " +
                    "AND (CASE WHEN @type_object::integer IS NOT NULL THEN type_object = @type_object ELSE 1=1 END)", conn);
                pgSqlCommand.Parameters.Add("@hoaId", hoaId);
                pgSqlCommand.Parameters.Add("@type_object", type_object);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetTariffsFromMetadata(int metadataId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand(" select * from types_tariffs" +
                    " where metadata_id = @propId", conn);
                pgSqlCommand.Parameters.Add("@propId", metadataId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }
    }
}
