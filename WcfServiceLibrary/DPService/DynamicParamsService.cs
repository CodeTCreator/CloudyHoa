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

        public void AddDynamicParam(int paId, float value, DateTime period, int propertyId, int tariffId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO dynamic_params (personal_account_id,tariff_id, value, period, property_id) " +
                    "VALUES (@personal_account_id,@tariff_id, @value, @period, @property_id)"
                    , conn);
                pgSqlCommand.Parameters.Add("@personal_account_id", paId);
                pgSqlCommand.Parameters.Add("@tariff_id", tariffId);
                pgSqlCommand.Parameters.Add("@value", value);
                pgSqlCommand.Parameters.Add("@period", period);
                pgSqlCommand.Parameters.Add("@property_id", propertyId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataSet BoneDynamicParams(int typeObject)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select metadata.id, property_name from metadata " +
                    "where type_object = :type_object and static = 'false' and calculated = 'false'", conn);
                pgSqlCommand.Parameters.Add(":type_object", typeObject);
                using (PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand))
                {
                    pgSqlDataAdapter.Fill(dataSet);
                }
                conn.Close();
            } 
            return dataSet;
        }

        public DataSet BoneDynamicParamsFromChildrens(int objectId, int typeObject)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("WITH RECURSIVE search_tree(id,type_object,identificator, parent_id) AS (  " +
                    "SELECT objects.id,objects.type_object,objects.identificator, objects.parent_id " +
                    "FROM objects " +
                    "where objects.id = :object_id " +
                    "UNION ALL " +
                    "SELECT t.id, t.type_object, t.identificator,t.parent_id" +
                    " FROM objects t, search_tree st " +
                    "WHERE st.id = t.parent_id )  " +
                    "SELECT search_tree.id as object_id,search_tree.type_object,parent_id,types_objects.name || ' ' || identificator as Name, " +
                    "account,metadata.property_name,metadata.id as metadata_id ,dynamicTable.period, dynamicTable.value,personal_account_id , " +
                    "types_tariffs.name as tariff_name,types_tariffs.id as tariff_id " +
                    "FROM search_tree " +
                    "join types_objects on types_objects.id = search_tree.type_object " +
                    "left join personal_account on personal_account.object_id = search_tree.id " +
                    "left join metadata on metadata.type_object = search_tree.type_object " +
                    "join types_tariffs on types_tariffs.metadata_id = metadata.id " +
                    "left join (" +
                    "select distinct on (property_id,personal_account_id) property_id,period, dynamic_params.value, " +
                    "object_id, property_name, personal_account_id, tariff_id " +
                    "from dynamic_params" +
                    " join metadata on metadata.id = property_id " +
                    "where type_object = :type_object " +
                    "order by property_id,personal_account_id,period desc) as dynamicTable " +
                    "on dynamicTable.personal_account_id = personal_account.id  and metadata.id = property_id and dynamicTable.tariff_id = types_tariffs.id" +
                    " where search_tree.type_object = :type_object and static = 'false' and calculated = 'false'", conn);
                pgSqlCommand.Parameters.Add(":object_id", objectId);
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
