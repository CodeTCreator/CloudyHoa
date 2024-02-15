using Devart.Data.PostgreSql;
using DevExpress.XtraExport.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StaticParamsService" in both code and config file together.
    public class StaticParamsService : IStaticParamsService
    {
        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];
        
        public void AddStaticParam(string value, int typeParams, int propertyId, int objectId)
        {
            DateTime startPeriod = DateTime.Now;
            DateTime changingDate = DateTime.Now;
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO static_params (property_id, " +
                    "string_value, number_value, date_value, changing_date, object_id, start_period) " +
                    "VALUES (@propertyId,@stringValue,@numberValue,@dateValue,@changingDate,@objectId,@startPeriod)"
                    , conn);

                switch (typeParams)
                {
                    case 1:
                        pgSqlCommand.Parameters.Add("@stringValue", value);
                        pgSqlCommand.Parameters.Add("@numberValue", null);
                        pgSqlCommand.Parameters.Add("@dateValue", null);
                        break;
                    case 2:
                        pgSqlCommand.Parameters.Add("@stringValue", null);
                        pgSqlCommand.Parameters.Add("@numberValue", value);
                        pgSqlCommand.Parameters.Add("@dateValue", null);
                        break;
                    case 3:
                        pgSqlCommand.Parameters.Add("@stringValue", null);
                        pgSqlCommand.Parameters.Add("@numberValue", null);
                        pgSqlCommand.Parameters.Add("@dateValue", value);
                        break;

                    default:
                        pgSqlCommand.Parameters.Add("@stringValue", null);
                        pgSqlCommand.Parameters.Add("@numberValue", null);
                        pgSqlCommand.Parameters.Add("@dateValue", null);
                        break;
                }
                pgSqlCommand.Parameters.Add("@propertyId", propertyId);
                pgSqlCommand.Parameters.Add("@startPeriod", startPeriod);
                pgSqlCommand.Parameters.Add("@changingDate", changingDate);
                pgSqlCommand.Parameters.Add("@objectId", objectId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void DeleteStaticParam(int paramId)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from static_params where id = @value1 and start_period > current_date"
                    , conn);
                pgSqlCommand.Parameters.Add("@value1", paramId);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }
        
        public DataSet GetStaticParam(int paramId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select * from static_params where id = @paramId", conn);
                pgSqlCommand.Parameters.Add("@paramId", paramId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetCurrentStaticParam(int propId)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("SELECT static_params.* FROM static_params " +
                    "WHERE  (property_id = @propId) " +
                    "ORDER BY changing_date DESC " +
                    "Limit 1", conn);
                pgSqlCommand.Parameters.Add("@propId", propId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }
        public DataSet GetCurrentStaticParams(int object_id)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select static_params.id,object_id,metadata.property_name,changing_date,start_period, " +
                    "case type_property " +
                    "when 2 then static_params.string_value " +
                    "when 1 then static_params.number_value::text " +
                    "when 3 then static_params.date_value::text " +
                    "ELSE 'some_default_value' " +
                    "end " +
                    "from static_params " +
                    "join metadata on metadata.id = static_params.property_id " +
                    "where static_params.id in (SELECT Distinct on (property_id,object_id) id " +
                    "FROM static_params " +
                    "where start_period < current_date and  object_id = @objectId " +
                    "order by property_id,object_id,start_period desc)  or static_params.id in " +
                    "(SELECT Distinct on (property_id,object_id) id " +
                    "FROM static_params " +
                    "where start_period > current_date and  object_id = @objectId " +
                    "order by property_id,object_id,start_period desc) order by property_id ", conn);
                pgSqlCommand.Parameters.Add("@objectId", object_id);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetOldStaticParams(int object_id)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select static_params.id,object_id,metadata.property_name,changing_date,start_period, " +
                    "case type_property " +
                    "when 2 then static_params.string_value " +
                    "when 1 then static_params.number_value::text " +
                    "when 3 then static_params.date_value::text " +
                    "ELSE 'some_default_value' " +
                    "end " +
                    "from static_params " +
                    "join metadata on metadata.id = static_params.property_id " +
                    "where (static_params.id not in (SELECT Distinct on (property_id,object_id) id " +
                    "FROM static_params " +
                    "where start_period < current_date and  object_id = @objectId " +
                    "order by property_id,object_id,start_period desc) " +
                    ") and  object_id = 56 and start_period < current_date " +
                    "order by property_id ", conn);
                pgSqlCommand.Parameters.Add("@objectId", object_id);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }

        public DataSet GetSchemeStaticParams(int hoaId, int typeObject)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("SELECT types_prop.name, metadata.type_object, metadata.id, metadata.formula, metadata.system_name, " +
                    "metadata.property_name, metadata.type_property, metadata.type_parent, metadata.hoa_id " +
                    "FROM metadata " +
                    "JOIN types_prop ON metadata.type_property = types_prop.id " +
                    "where hoa_id = @hoaId and type_object = @typeObject and static = true", conn);
                pgSqlCommand.Parameters.Add("@typeObject", typeObject);
                pgSqlCommand.Parameters.Add("@hoaId", hoaId);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }
    }
}
