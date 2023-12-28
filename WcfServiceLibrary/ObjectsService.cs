using Devart.Data.PostgreSql;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ObjectsService" in both code and config file together.
    public class ObjectsService : IObjectsService
    {
        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettingss.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];
        public void AddObject(int typeObject, string objectNumber, int parentId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from objects where id = @id_value", conn);
                pgSqlCommand.Parameters.Add("@id_value", Id);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void DeleteObject(int Id)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from objects where id = @id_value", conn);
                pgSqlCommand.Parameters.Add("@id_value", Id);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void EditObject(int Id, string objectNumber, int parentId)
        {
            throw new NotImplementedException();
        }
    }
}
