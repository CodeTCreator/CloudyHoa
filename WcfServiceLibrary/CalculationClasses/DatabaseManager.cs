using Devart.Data.PostgreSql;
using DevExpress.CodeParser;
using DevExpress.Data.Entity;
using DevExpress.Xpo.DB.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace UniversalHoa_WF.Classes
{
    /// <summary>
    /// Класс, использующийся для подключения к БД. Используется для выполения команд к БД / требует Devart
    /// </summary>
    public class DatabaseManager
    {
        
        private PgSqlConnection _conn;
        bool _enabledConnection = false;
        public DatabaseManager(PgSqlConnection pgSqlConnection) 
        {
            Connection = pgSqlConnection;
        }

        public PgSqlConnection Connection
        {
            set { _conn = value; }
            get { return _conn; }
        }
        public void ConnectToDatabase()
        {
            _conn.Open();
            _enabledConnection = true;
        }
        public void ExecuteQuery(string query)
        {
            if (_enabledConnection)
            {
                PgSqlCommand cmd = new PgSqlCommand();
                cmd.CommandText = query;
                cmd.Connection = _conn;
                int aff = cmd.ExecuteNonQuery();
            }
        }
        public void DisconnectToDatabase()
        {
            _conn.Close(); 
        }

        public bool CheckExistingCalculations(DateTime dateTime)
        {
            PgSqlCommand pgSqlCommand;

            string query = "select from dynamic_params  join metadata on metadata.id = dynamic_params.property_id" +
                " where period = @dateValue and calculated = true";

            pgSqlCommand = new PgSqlCommand(query, Connection);
            pgSqlCommand.Parameters.Add("@dateValue", dateTime);
            PgSqlDataReader pgSqlDataReader = pgSqlCommand.ExecuteReader();
            if(pgSqlDataReader.HasRows) 
            {
                return true;
            }
            return false;
        }


    }
}
