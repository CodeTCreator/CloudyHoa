using Devart.Data.PostgreSql;
using DevExpress.Xpo.DB.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;

namespace WcfServiceLibrary.DPService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DynamicParamsService" in both code and config file together.
    public class DynamicParamsService : IDynamicParamsService
    {
        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];

        public void AddDynamicParam(int objectId, float value, DateTime period, int propertyId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO dynamic_params (object_id, value, period, property_id) " +
                    "VALUES (@object_id, @value, @period, @property_id)"
                    , conn);
                pgSqlCommand.Parameters.Add("@object_id", objectId);
                pgSqlCommand.Parameters.Add("@value", value);
                pgSqlCommand.Parameters.Add("@period", period);
                pgSqlCommand.Parameters.Add("@property_id", propertyId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataSet BoneDynamicParams(int typeObject, int hoaId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select metadata.id, property_name from metadata " +
                    "where hoa_id = :hoa_id and type_object = :type_object and static = 'false' and calculated = 'false'", conn);
                pgSqlCommand.Parameters.Add(":hoa_id", hoaId);
                pgSqlCommand.Parameters.Add(":type_object", typeObject);
                using (PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand))
                {
                    pgSqlDataAdapter.Fill(dataSet);
                }
                conn.Close();
            } 
            return dataSet;
        }

        public DataSet DynamicParametersTable(int hoaId,int typeObject)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand(
                    " select " +
                    "objects.id , objects.hoa_id , objects.type_object," +
                    "objects.parent_id, objects.identificator, types_objects.name  " +
                    "from (objects inner join types_objects  on (types_objects.id = objects.type_object)) " +
                    "where (objects.type_object = :type_object and objects.hoa_id = :hoa_id)", conn);
                pgSqlCommand.Parameters.Add(":hoa_id", hoaId);
                pgSqlCommand.Parameters.Add(":type_object", typeObject);
                using (PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand))
                {
                    pgSqlDataAdapter.Fill(dataSet);
                }
                dataSet.Tables[0].Columns.Add("curr_period", typeof(DateTime));
                dataSet.Tables[0].Columns.Add("curr_value", typeof(float));
                conn.Close();
            }
            return dataSet;
        }

        public DataSet OldAllDynamicParams(int hoaId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select distinct on (property_id,object_id) property_id,period, value,object_id, property_name " +
                    "from dynamic_params " +
                    "join metadata on metadata.id = property_id " +
                    "where hoa_id = :hoa_id " +
                    "order by property_id,object_id,period desc", conn);
                pgSqlCommand.Parameters.Add(":hoa_id", hoaId);
                using (PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand))
                {
                    pgSqlDataAdapter.Fill(dataSet);

                }
                conn.Close();
            }
            return dataSet;
        }
        public DataSet OldDynamicParams(int typeObject)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select distinct on (property_id,object_id) property_id,period, value,object_id, property_name " +
                    "from dynamic_params " +
                    "join metadata on metadata.id = property_id " +
                    "where type_object = @typeObject " +
                    "order by property_id,object_id,period desc", conn);
                pgSqlCommand.Parameters.Add("@typeObject", typeObject);
                using (PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand))
                {
                    pgSqlDataAdapter.Fill(dataSet);

                }
                conn.Close();
            }
            return dataSet;
        }

        public DataSet SchemeDPTable()
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select * from dynamic_params where 1 = 0", conn);
                using (PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand))
                {
                    pgSqlDataAdapter.Fill(dataSet);
                }
                conn.Close();
            }
            return dataSet;
        }
    }
}
