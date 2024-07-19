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

        public DataSet GetTemplate(int propId,DateTime period)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select dynamic_params.*, personal_account.account, types_objects.name" +
                    " from dynamic_params " +
                    "left join personal_account on personal_account_id = personal_account.id " +
                    "join objects on objects.id = dynamic_params.object_id " +
                    "join types_objects on objects.type_object = types_objects.id " +
                    "where period = @period and property_id = @propId  ", conn);
                pgSqlCommand.Parameters.Add("@propId", propId);
                pgSqlCommand.Parameters.Add("@period", period);
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
    }
}
