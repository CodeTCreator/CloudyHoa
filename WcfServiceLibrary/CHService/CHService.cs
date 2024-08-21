using Devart.Data.PostgreSql;
using DevExpress.XtraPrinting.Native;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary.CHService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CHService" in both code and config file together.
    public class CHService : ICHService
    {

        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];

        public DataSet GetCalculationHistoryFromObject(int objectId, DateTime period)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlDataAdapter pgSqlDataAdapter;
                if (period != null)
                {
                    PgSqlCommand pgSqlCommand = new PgSqlCommand(" select calculations.*, metadata.property_name from calculations " +
                        "join personal_account on personal_account.id = pa_id " +
                        "join metadata on metadata_id = metadata.id " +
                        "join objects on objects.id = personal_account.object_id " +
                        "where period = @period and object_id = @objectId", conn);
                    pgSqlCommand.Parameters.Add("@objectId", objectId);
                    pgSqlCommand.Parameters.Add("@period", period);
                    pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                }
                else
                {
                    PgSqlCommand pgSqlCommand = new PgSqlCommand(" select calculations.*, metadata.property_name from calculations " +
                        "join personal_account on personal_account.id = pa_id " +
                        "join metadata on metadata_id = metadata.id " +
                        "join objects on objects.id = personal_account.object_id " +
                        "where object_id = @objectId", conn);
                    pgSqlCommand.Parameters.Add("@objectId", objectId);
                    pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                }
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetCalculationHistoryFromPA(int paId, DateTime period)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlDataAdapter pgSqlDataAdapter;
                if (period != null)
                {
                    PgSqlCommand pgSqlCommand = new PgSqlCommand(" select calculations.*, metadata.property_name from calculations " +
                        "join personal_account on personal_account.id = pa_id " +
                        "join metadata on metadata_id = metadata.id " +
                        "join objects on objects.id = personal_account.object_id " +
                        "where period = @period and pa_id = @paId", conn);
                    pgSqlCommand.Parameters.Add("@paId", paId);
                    pgSqlCommand.Parameters.Add("@period", period);
                    pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                }
                else
                {
                    PgSqlCommand pgSqlCommand = new PgSqlCommand(" select calculations.*, metadata.property_name from calculations " +
                        "join personal_account on personal_account.id = pa_id " +
                        "join metadata on metadata_id = metadata.id " +
                        "join objects on objects.id = personal_account.object_id " +
                        "where pa_id = @paId", conn);
                    pgSqlCommand.Parameters.Add("@paId", paId);
                    pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                }
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }
    }
}
