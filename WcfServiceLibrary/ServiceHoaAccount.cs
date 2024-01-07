using Devart.Data.PostgreSql;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceHoaAccount" in both code and config file together.
    public class ServiceHoaAccount : IServiceHoaAccount
    {

        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];
        //string connectionString = "";

        public void AddAccount(string Name,string Login, string Password)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO hoa  (Name, login, password) VALUES (@name,@login,@password)"
                    , conn);
                pgSqlCommand.Parameters.Add("@name", Name);
                pgSqlCommand.Parameters.Add("@login", Login);
                pgSqlCommand.Parameters.Add("@password", Password);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public bool CheckAccout(string Name)
        {
            bool flag = true;
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Select * from hoa where name = @Name", conn);
                pgSqlCommand.Parameters.Add("@Name",Name );
                using (PgSqlDataReader reader = pgSqlCommand.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        flag = false;
                    }
                }
                conn.Close();
            }
            return flag;
        }

        public void DeleteAccount(int Id)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from hoa where id = @id_value", conn);
                pgSqlCommand.Parameters.Add("@id_value", Id);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void EditAccount(int Id, string Name, string Login, string Password)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("UPDATE hoa SET name = @name, login = @login," +
                    "password = @pass where id = @id", conn);
                pgSqlCommand.Parameters.Add("@id", Id);
                pgSqlCommand.Parameters.Add("@name", Name);
                pgSqlCommand.Parameters.Add("@login", Login);
                pgSqlCommand.Parameters.Add("@pass", Password);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataSet GetTableOfHoa()
        {
            DataSet dataSet = new DataSet("Hoa");
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select * from hoa", conn);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }
    }
}
