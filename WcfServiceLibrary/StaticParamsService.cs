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
        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettingss.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];
        
        public void addStaticParam(string value, int typeParams, int propertyId, DateTime startPeriod, DateTime changingDate, int objectId)
        {
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

        public void deleteStaticParam(int paramId)
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
        
        public DataSet getStaticParam(int paramId)
        {
            throw new NotImplementedException();
        }

        public DataSet getCurrentStaticParam(int paramId)
        {
            throw new NotImplementedException();
        }

        public DataSet getCurrentStaticParams(int objectId)
        {
            throw new NotImplementedException();
        }

        public DataSet getOldStaticParams(int objectId)
        {
            throw new NotImplementedException();
        }
    }
}
