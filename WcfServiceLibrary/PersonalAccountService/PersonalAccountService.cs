using Devart.Data.PostgreSql;
using DevExpress.XtraEditors.SyntaxEditor;
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
    public class PersonalAccountService : IPersonalAccountService
    {

        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];

        public void AddPersonalAccount(string account, int objectId, int ownerId, int registered, int lives)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO personal_account (account, object_id, owner_id, lives,registered) " +
                    "VALUES (@account,@object_id,@ownerId,@lives,@registered)"
                    , conn);
                pgSqlCommand.Parameters.Add("@account", account);
                pgSqlCommand.Parameters.Add("@object_id", objectId);
                pgSqlCommand.Parameters.Add("@ownerId", ownerId);
                pgSqlCommand.Parameters.Add("@lives", registered);
                pgSqlCommand.Parameters.Add("@registered", lives);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void DeletePersonalAccount(int id)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from personal_account where id = @id_value", conn);
                pgSqlCommand.Parameters.Add("@id_value", id);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void EditPersonalAccount(int id, string account, int objectId, int ownerId, int registered, int lives)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("UPDATE personal_account SET account = @account, object_id = @object_id," +
                    "owner_id = @ownerId, lives = @lives,registered = @registered" +
                    " where id = @id", conn);
                pgSqlCommand.Parameters.Add("@id", id);
                pgSqlCommand.Parameters.Add("@account", account);
                pgSqlCommand.Parameters.Add("@object_id", objectId);
                pgSqlCommand.Parameters.Add("@ownerId", ownerId);
                pgSqlCommand.Parameters.Add("@lives", registered);
                pgSqlCommand.Parameters.Add("@registered", lives);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }
        public DataSet GetPersonalAccounts(int objectId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select personal_account.*,hoa_id,full_name from personal_account " +
                    "join objects on objects.id = personal_account.object_id " +
                    "join residents on owner_id = residents.id " +
                    "where personal_account.object_id = @objectId", conn);
                pgSqlCommand.Parameters.Add("@objectId", objectId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

    }
}
