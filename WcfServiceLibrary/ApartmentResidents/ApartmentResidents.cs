using Devart.Data.PostgreSql;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;


namespace WcfServiceLibrary.ApartmentOwnersService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ApartmentOwnersService" in both code and config file together.
    public class ApartmentResidents : IApartmentResidents
    {
        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];

        public void AddResident(string fullName, int objectId, bool registered, DateTime? registration_date,
            bool residence, DateTime? checkInDate, bool owner, int? numenator, int? denominator)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand(" INSERT INTO residents " +
                    "(full_name, object_id, registered, registration_date, residence, check_in_date, \"owner\", owners_share_numerator," +
                    "owners_share_denominator) " +
                    "VALUES (@fullName,@objectId,@registered,@registration_date,@residence,@check_in_date,@owner,@numerator, @denominator)"
                    , conn);
                pgSqlCommand.Parameters.Add("@fullName", fullName);
                pgSqlCommand.Parameters.Add("@objectId", objectId);

                pgSqlCommand.Parameters.Add("@registered", registered);
                pgSqlCommand.Parameters.Add("@registration_date", registration_date);

                pgSqlCommand.Parameters.Add("@residence", residence);
                pgSqlCommand.Parameters.Add("@check_in_date", checkInDate);

                pgSqlCommand.Parameters.Add("@owner", owner);
                pgSqlCommand.Parameters.Add("@numerator", numenator);
                pgSqlCommand.Parameters.Add("@denominator", denominator);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void DeleteResident(int Id)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from residents where id = @id_value", conn);
                pgSqlCommand.Parameters.Add("@id_value", Id);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void EditResident(int Id, string fullName, int objectId, bool registered, DateTime? registration_date,
            bool residence, DateTime? checkInDate, bool owner, int? numenator, int? denominator)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("UPDATE residents SET full_name = @fullName, object_id = @objectId," +
                    "registered = @registered, registration_date = @registration_date, residence = @residence, " +
                    " check_in_date = @check_in_date, " +
                    " owner = @owner, owners_share_numerator = @numenator, " +
                    " owners_share_denominator = @denominator where id = @id", conn);
                pgSqlCommand.Parameters.Add("@id", Id);
                pgSqlCommand.Parameters.Add("@fullName", fullName);
                pgSqlCommand.Parameters.Add("@objectId", objectId);

                pgSqlCommand.Parameters.Add("@registered", registered);
                pgSqlCommand.Parameters.Add("@registration_date", registration_date);

                pgSqlCommand.Parameters.Add("@residence", residence);
                pgSqlCommand.Parameters.Add("@check_in_date", checkInDate);

                pgSqlCommand.Parameters.Add("@owner", owner);
                pgSqlCommand.Parameters.Add("@numenator", numenator);
                pgSqlCommand.Parameters.Add("@denominator", denominator);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataSet GetResidents(int objectId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select * from residents where object_id = @objectId", conn);
                pgSqlCommand.Parameters.Add("@objectId", objectId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }
    }
}
