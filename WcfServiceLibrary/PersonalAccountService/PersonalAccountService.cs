using Devart.Data.PostgreSql;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        public void addPersonalAccount(string account, int object_id)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO personal_account (account, object_id) VALUES (@account,@object_id)"
                    , conn);
                pgSqlCommand.Parameters.Add("@account", account);
                pgSqlCommand.Parameters.Add("@object_id", object_id);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void deletePersonalAccount(int id)
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

        public void editPersonalAccount(int id, string account, int object_id)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("UPDATE personal_account SET account = @account, object_id = @object_id" +
                    " where id = @id", conn);
                pgSqlCommand.Parameters.Add("@id", id);
                pgSqlCommand.Parameters.Add("@account", account);
                pgSqlCommand.Parameters.Add("@object_id", object_id);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
