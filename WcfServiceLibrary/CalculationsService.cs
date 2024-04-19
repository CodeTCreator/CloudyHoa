using Devart.Data.PostgreSql;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CalculationsService" in both code and config file together.
    public class CalculationsService : ICalculationsService
    {
        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();

        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];
        public DataSet GetCalculations(int objectId, DateTime period)
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand(" select \"calculations\".*, " +
                    " \"metadata\".\"property_name\" " +
                    "from (\"calculations\" \"calculations\"\r\n  inner join \"metadata\" \"metadata\" " +
                    "   on (\"metadata\".\"id\" = \"calculations\".\"metadata_id\")) " +
                    "where object_id = @objectId and  " +
                    "EXTRACT(MONTH FROM period) = EXTRACT(MONTH FROM @period::DATE)  " +
                    "AND EXTRACT(YEAR FROM period) = EXTRACT(YEAR FROM @period::DATE)", conn);
                pgSqlCommand.Parameters.Add("@objectId", objectId);
                pgSqlCommand.Parameters.Add("@period", period);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataSet);
                conn.Close();
            }
            return dataSet;
        }


    }
}
