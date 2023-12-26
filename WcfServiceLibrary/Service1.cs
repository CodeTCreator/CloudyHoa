using Devart.Data.PostgreSql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        string ConnectionString = "User Id=postgres;password = admin;Host=localhost;Database=UHoa;Unicode=True;Character Set=UTF8;Initial Schema=public;SSLMode=Disable";
        public DataTable GetData(string nameTable)
        {
            DataTable dataTable = new DataTable("types_objects");
            using (PgSqlConnection conn = new PgSqlConnection(ConnectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select * from types_objects", conn);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        public bool CheckConnection()
        {
            
            PgSqlConnection conn = new PgSqlConnection(ConnectionString);
            conn.Open();
            if(conn.State != ConnectionState.Open)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int GetNumRow()
        {
            int number = -1;
            PgSqlDataTable dataTable = new PgSqlDataTable();
            using (PgSqlConnection conn = new PgSqlConnection(ConnectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select * from types_objects", conn);
                PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand);
                pgSqlDataAdapter.Fill(dataTable);
                number = dataTable.Rows.Count;
            }
            return number;
        }
    }
}
