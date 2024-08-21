using Devart.Data.PostgreSql;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary.EHService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EHService" in both code and config file together.
    public class EHService : IEHService
    {
        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];

        public DataSet GetEnteringHistoryFromObject(int objectId, DateTime period)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlDataAdapter pgSqlDataAdapter;
                if (period != null)
                {
                    PgSqlCommand pgSqlCommand = new PgSqlCommand(
                        "select dynamic_params.*, metadata.property_name from dynamic_params " +
                        "join personal_account on personal_account.id = personal_account_id " +
                        "join metadata on property_id = metadata.id " +
                        "join objects on objects.id = personal_account.object_id " +
                        "where period = @period and dynamic_params.object_id = @objectId", conn);
                    pgSqlCommand.Parameters.Add("@objectId", objectId);
                    pgSqlCommand.Parameters.Add("@period", period);
                    pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                }
                else
                {
                    PgSqlCommand pgSqlCommand = new PgSqlCommand(
                        "select dynamic_params.*, metadata.property_name from dynamic_params " +
                        "join personal_account on personal_account.id = personal_account_id " +
                        "join metadata on property_id = metadata.id " +
                        "join objects on objects.id = personal_account.object_id " +
                        "where dynamic_params.object_id = @objectId", conn);
                    pgSqlCommand.Parameters.Add("@objectId", objectId);
                    pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                }
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetEnteringHistoryFromPA(int paId, DateTime period)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlDataAdapter pgSqlDataAdapter;
                if (period != null)
                {
                    PgSqlCommand pgSqlCommand = new PgSqlCommand(
                        "select dynamic_params.*, metadata.property_name from dynamic_params " +
                        "join personal_account on personal_account.id = personal_account_id " +
                        "join metadata on property_id = metadata.id " +
                        "join objects on objects.id = personal_account.object_id " +
                        "where period = @period and personal_account_id = @paId", conn);
                    pgSqlCommand.Parameters.Add("@paId", paId);
                    pgSqlCommand.Parameters.Add("@period", period);
                    pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                }
                else
                {
                    PgSqlCommand pgSqlCommand = new PgSqlCommand(
                        "select dynamic_params.*, metadata.property_name from dynamic_params " +
                        "join personal_account on personal_account.id = personal_account_id " +
                        "join metadata on property_id = metadata.id " +
                        "join objects on objects.id = personal_account.object_id " +
                        "where personal_account_id = @paId", conn);
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
