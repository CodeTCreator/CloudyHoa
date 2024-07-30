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
using UHoaAdmin.Classes;
using UniversalHoa_WF.Classes;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CalculationsService" in both code and config file together.
    public class CalculationsService : ICalculationsService
    {
        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];
        public DataSet GetCalculations(int paId, DateTime period)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand(" select \"calculations\".*, " +
                    " \"metadata\".\"property_name\" " +
                    "from (\"calculations\" \"calculations\"\r\n  inner join \"metadata\" \"metadata\" " +
                    "   on (\"metadata\".\"id\" = \"calculations\".\"metadata_id\")) " +
                    "where pa_id = @paId and  " +
                    "EXTRACT(MONTH FROM period) = EXTRACT(MONTH FROM @period::DATE)  " +
                    "AND EXTRACT(YEAR FROM period) = EXTRACT(YEAR FROM @period::DATE)", conn);
                pgSqlCommand.Parameters.Add("@paId", paId);
                pgSqlCommand.Parameters.Add("@period", period);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetTemplate(int propId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select types_tariffs.id as tariff_id, types_tariffs.name as tariff_name," +
                    "types_tariffs.value as tariff_value, types_tariffs.metadata_id as metadata_id, " +
                    "personal_account.id as personal_account_id, " +
                    "personal_account.account," +
                    " personal_account.object_id as object_id, types_objects.name || ' ' || objects.identificator AS object_name  " +
                    "from types_tariffs " +
                    " cross join personal_account " +
                    " join objects on objects.id = object_id " +
                    " join types_objects on types_objects.id = objects.type_object " +
                    " where metadata_id = @propId  ", conn);
                pgSqlCommand.Parameters.Add("@propId", propId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public void AddCalculation(int propId, DateTime period, float value, int paId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO calculations (metadata_id, value, period,pa_id) " +
                    "VALUES (@metadata_id,@value,@period, @pa_id)"
                    , conn);
                pgSqlCommand.Parameters.Add("@metadata_id", propId);
                pgSqlCommand.Parameters.Add("@value", period);
                pgSqlCommand.Parameters.Add("@period", value);
                pgSqlCommand.Parameters.Add("@pa_id", paId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void EditCalculation(int propId, DateTime period, float value, int paId, int id)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("UPDATE calculations SET metadata_id = @propId, " +
                    "period = @period," +
                    "value = @value, paId = @paId where id = @id", conn);
                pgSqlCommand.Parameters.Add("@id", id);
                pgSqlCommand.Parameters.Add("@propId", propId);
                pgSqlCommand.Parameters.Add("@period", period);
                pgSqlCommand.Parameters.Add("@value", value);
                pgSqlCommand.Parameters.Add("@paId", paId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void DeleteCalcuation(int id)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from calculations where id = @id_value", conn);
                pgSqlCommand.Parameters.Add("@id_value", id);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public double CalculateResultValue(int propId,int objectId, int paId, DateTime period)
        {
            double result = 0;
            DatabaseManager databaseManager = new DatabaseManager(new PgSqlConnection(connectionString));
            databaseManager.ConnectToDatabase();
            result = CalculateProperty.Calculate(propId, objectId, paId, period, databaseManager);
            databaseManager.DisconnectToDatabase();
            return result;
        }
    }
}
