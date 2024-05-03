using Devart.Data.PostgreSql;
using DevExpress.XtraEditors.SyntaxEditor;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary.MDService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MDService" in both code and config file together.
    public class MDService : IMDService
    {
        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];
        public void AddMeteringDevice(string number,DateTime verification_date,DateTime installation_date, int service_id,int personal_account_id)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO metering_device " +
                    "(\"number\", verification_date, installation_date, service_id, personal_account_id) " +
                    "VALUES (@number,@verification_date,@installation_date,@service_id,@personal_account_id)"
                    , conn);
                pgSqlCommand.Parameters.Add("@number", number);
                pgSqlCommand.Parameters.Add("@verification_date", verification_date);
                pgSqlCommand.Parameters.Add("@installation_date", installation_date);
                pgSqlCommand.Parameters.Add("@service_id", service_id);
                pgSqlCommand.Parameters.Add("@personal_account_id", personal_account_id);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void DeleteMeteringDevice(int deviceId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from metering_device where id = @deviceId", conn);
                pgSqlCommand.Parameters.Add("@deviceId", deviceId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void EditMeteringDevice(int deviceId, string number, DateTime verification_date, DateTime installation_date, int service_id, int personal_account_id)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("UPDATE metering_device " +
                    "SET \"number\" =@number, verification_date =@verification_date, installation_date = @installation_date, service_id = @service_id," +
                    " personal_account_id = @personal_account_id" +
                    " where id = @id", conn);
                pgSqlCommand.Parameters.Add("@id", deviceId);
                pgSqlCommand.Parameters.Add("@number", number);
                pgSqlCommand.Parameters.Add("@verification_date", verification_date);
                pgSqlCommand.Parameters.Add("@installation_date", installation_date);
                pgSqlCommand.Parameters.Add("@service_id", service_id);
                pgSqlCommand.Parameters.Add("@personal_account_id", personal_account_id);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataSet GetMeteringDevicesO(int objectId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select metering_device.* " +
                    "from metering_device " +
                    "join personal_account on metering_device.personal_account_id = personal_account.id " +
                    "where personal_account.object_id = @objectId", conn);
                pgSqlCommand.Parameters.Add("@objectId", objectId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetMeteringDevicesP(int personalAccountId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select * " +
                    "from metering_device " +
                    "where personal_account_id = @personalAccountId", conn);
                pgSqlCommand.Parameters.Add("@personalAccountId", personalAccountId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }
    }
}
