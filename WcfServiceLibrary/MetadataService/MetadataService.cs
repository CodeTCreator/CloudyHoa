using Devart.Data.PostgreSql;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace WcfServiceLibrary.MetadataService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MetadataService" in both code and config file together.
    public class MetadataService : IMetadataService
    {
        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();
        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];

        public void AddParameter(int hoaId, int typeObject, string propertyName, int typeProperty, string systemName, string formula, bool staticParam, bool calculated)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO metadata " +
                    " (hoa_id, type_object, property_name, type_property, system_name, formula, \"static\", calculated)" +
                    " VALUES (@hoa_id,@type_object,@property_name,@type_property,@system_name,@formula,@static,@calculated)", conn);
                pgSqlCommand.Parameters.Add("@hoa_id", hoaId);
                pgSqlCommand.Parameters.Add("@type_object", typeObject);
                pgSqlCommand.Parameters.Add("@property_name", propertyName);
                pgSqlCommand.Parameters.Add("@type_property", typeProperty);
                pgSqlCommand.Parameters.Add("@system_name", systemName);
                pgSqlCommand.Parameters.Add("@formula", formula);
                pgSqlCommand.Parameters.Add("@static", staticParam);
                pgSqlCommand.Parameters.Add("@calculated", calculated);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void AddTypeObject(string nameType, int? parentType, int hoaId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Insert into types_objects (hoa_id,name,parent_type) values" +
                    "(@hoa_id,@nameType,@parentType)", conn);
                pgSqlCommand.Parameters.Add("@hoa_id", hoaId);
                pgSqlCommand.Parameters.Add("@nameType", nameType);
                pgSqlCommand.Parameters.Add("@parentType", parentType);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void DeleteParameter(int parameterId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from metadata where id = @id_value", conn);
                pgSqlCommand.Parameters.Add("@id_value", parameterId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void DeleteTypeObject(int typeId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from types_objects where id = @id_value", conn);
                pgSqlCommand.Parameters.Add("@id_value", typeId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void EditParameter(int parameterId, string propertyName, int typeProperty, string systemName, string formula, bool staticParam, bool calculated)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("UPDATE metadata SET property_name = @propertyName, " +
                    "type_property = @typeProperty, system_name = @systemName, formula = @formula, static = @staticParam," +
                    "calculated = @calculated where id = @id", conn);
                pgSqlCommand.Parameters.Add("@id", parameterId);
                pgSqlCommand.Parameters.Add("@propertyName", propertyName);
                pgSqlCommand.Parameters.Add("@typeProperty", typeProperty);
                pgSqlCommand.Parameters.Add("@systemName", systemName);
                pgSqlCommand.Parameters.Add("@formula", formula);
                pgSqlCommand.Parameters.Add("@staticParam", staticParam);
                pgSqlCommand.Parameters.Add("@calculated", calculated);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void EditTypeObject(int typeId, string nameType, int? parentType)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("UPDATE types_objects SET name = @nameType, " +
                    "parent_type = @parentType where id = @id", conn);
                pgSqlCommand.Parameters.Add("@id", typeId);
                pgSqlCommand.Parameters.Add("@nameType", nameType);
                pgSqlCommand.Parameters.Add("@parentType", parentType);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }
        public DataSet GetParameters(int typeObject)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select * from metadata where type_object = @typeObject", conn);
                pgSqlCommand.Parameters.Add("@typeObject", typeObject);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }
        public DataSet GetAllTypesObjects()
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("SELECT types_objects.*, hoa.\"name\" AS hoa_name " +
                    "FROM types_objects " +
                    "INNER JOIN  hoa ON types_objects.hoa_id = hoa.id", conn);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }
        public DataSet GetTypesObjects(int hoaId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("SELECT types_objects.*, hoa.\"name\" AS hoa_name " +
                    "FROM types_objects " +
                    "INNER JOIN  hoa ON types_objects.hoa_id = hoa.id where hoa.id = @hoaId", conn);
                pgSqlCommand.Parameters.Add("@hoaId", hoaId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetTypesParameters()
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("SELECT * from types_prop ", conn);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetServices(int typeObject)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("SELECT * from metadata " +
                    "where calculated = false and static = false and type_object = @typeObject ", conn);
                pgSqlCommand.Parameters.Add("@typeObject", typeObject);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetCalculationServices(int typeObject)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("SELECT * from metadata " +
                    "where calculated = true and static = false and type_object = @typeObject ", conn);
                pgSqlCommand.Parameters.Add("@typeObject", typeObject);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }
    }
}
