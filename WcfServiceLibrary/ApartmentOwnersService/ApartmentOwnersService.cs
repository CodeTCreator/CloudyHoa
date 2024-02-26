using Devart.Data.PostgreSql;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;

namespace WcfServiceLibrary.ApartmentOwnersService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ApartmentOwnersService" in both code and config file together.
    public class ApartmentOwnersService : IApartmentOwnersService
    {
        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];

        public void addApartmentOwner(string fullName, int objectId, string ownershipShare)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO owners  (full_name, object_id, ownership_share) " +
                    "VALUES (@fullName,@objectId,@ownershipShare)"
                    , conn);
                pgSqlCommand.Parameters.Add("@fullName", fullName);
                pgSqlCommand.Parameters.Add("@objectId", objectId);
                pgSqlCommand.Parameters.Add("@ownershipShare", ownershipShare);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void deleteApartmentOwner(int Id)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from owners where id = @id_value", conn);
                pgSqlCommand.Parameters.Add("@id_value", Id);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void editApartmentOwner(int Id, string fullName, int objectId, string ownershipShare)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("UPDATE owners SET full_name = @fullName, object_id = @objectId," +
                    "ownership_share = @ownershipShare where id = @id", conn);
                pgSqlCommand.Parameters.Add("@id", Id);
                pgSqlCommand.Parameters.Add("@fullName", fullName);
                pgSqlCommand.Parameters.Add("@objectId", objectId);
                pgSqlCommand.Parameters.Add("@ownershipShare", ownershipShare);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
