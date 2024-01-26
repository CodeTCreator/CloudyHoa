using Devart.Data.PostgreSql;
using System;
using UniversalHoa_WF.Classes;

namespace UHoaAdmin.ParametrWindows
{
    public class DBRequest
    {

        DatabaseManager databaseManager;
        DateTime period;
        public DBRequest() 
        {

        }
        public DBRequest(DatabaseManager databaseManager_new,DateTime dateTime)
        {
            //databaseManager = new DatabaseManager(pgSqlConnection);
            databaseManager = databaseManager_new;
            databaseManager.ConnectToDatabase();
            period = dateTime;
        }

        /// <summary>
        /// Первым должен быть запрос в метадата для получения id свойства/параметра
        /// Вторым должен быть запрос в объекты_платежа,чтобы получить конкретный объект (id в таблице po)
        /// Третий запрос для получения значения из таблицы динамических параметров
        /// </summary>
        /// <param name="SystemName"></param>
        /// <param name="date"></param>
        /// <param name="hoa_id"></param>
        /// <param name="type_object"></param>
        /// <param name="identificator"></param>
        /// <returns></returns>
        public float GetCurrentValue(string SystemName,int object_id)
        {
            float result = 0;
            PgSqlCommand pgSqlCommand;
            PgSqlDataReader pgSqlDataReader;

            // Проверка свойства: static / dynamic
            pgSqlCommand = new PgSqlCommand("select static,metadata.id from metadata " +
                "inner join objects on objects.type_object = metadata.type_object " +
                "where system_name = @value1 and objects.id = @value2",
                databaseManager.Connection);
            pgSqlCommand.Parameters.Add("@value1",SystemName);
            pgSqlCommand.Parameters.Add("@value2",object_id); 
            pgSqlDataReader = pgSqlCommand.ExecuteReader();
            pgSqlDataReader.Read();
            if (pgSqlDataReader.HasRows)
            {
                int property_id = pgSqlDataReader.GetInt32(1);
                if (pgSqlDataReader.GetBoolean(0))
                {
                    pgSqlCommand = new PgSqlCommand("select number_value from static_params " +
                        "where property_id = @value1 and object_id = @value2 order by changing_date desc",
                    databaseManager.Connection);
                    pgSqlCommand.Parameters.Add("@value1", property_id);
                    pgSqlCommand.Parameters.Add("@value2", object_id);
                    pgSqlDataReader = pgSqlCommand.ExecuteReader();
                    pgSqlDataReader.Read();
                    if (pgSqlDataReader.HasRows)
                    {
                        result = pgSqlDataReader.GetFloat(0);
                    }
                }
                else
                {
                    pgSqlCommand = new PgSqlCommand("select value from dynamic_params " +
                        "where property_id = @value1 and object_id = @value2 " +
                        "and extract(MONTH FROM period) = extract(MONTH FROM @value3::DATE) " +
                        "and extract(YEAR FROM period) = extract(YEAR FROM @value3::DATE) order by period desc",
                    databaseManager.Connection);
                    pgSqlCommand.Parameters.Add("@value1", property_id);
                    pgSqlCommand.Parameters.Add("@value2", object_id);
                    pgSqlCommand.Parameters.Add("@value3", period);
                    pgSqlDataReader = pgSqlCommand.ExecuteReader();
                    pgSqlDataReader.Read();
                    if (pgSqlDataReader.HasRows)
                    {
                        result = pgSqlDataReader.GetFloat(0);
                    }
                }
            }
            return result;
        }

        public float GetPreviousValue(string SystemName,int object_id)
        {
            float result = 0;
            PgSqlCommand pgSqlCommand;
            PgSqlDataReader pgSqlDataReader;

            // Проверка свойства: static / dynamic
            pgSqlCommand = new PgSqlCommand("select static,metadata.id from metadata " +
                "inner join objects on objects.type_object = metadata.type_object " +
                "where system_name = @value1 and objects.id = @value2",
                databaseManager.Connection);
            pgSqlCommand.Parameters.Add("@value1", SystemName);
            pgSqlCommand.Parameters.Add("@value2", object_id);
            pgSqlDataReader = pgSqlCommand.ExecuteReader();
            pgSqlDataReader.Read();
            if (pgSqlDataReader.HasRows)
            {
                int property_id = pgSqlDataReader.GetInt32(1);
                if (pgSqlDataReader.GetBoolean(0))
                {
                    pgSqlCommand = new PgSqlCommand("select number_value from static_params " +
                        "where property_id = @value1 and object_id = @value2 order by changing_date desc",
                    databaseManager.Connection);
                    pgSqlCommand.Parameters.Add("@value1", property_id);
                    pgSqlCommand.Parameters.Add("@value2", object_id);
                    pgSqlDataReader = pgSqlCommand.ExecuteReader();
                    pgSqlDataReader.Read(); pgSqlDataReader.Read();
                    if(pgSqlDataReader.HasRows)
                    {
                        result = pgSqlDataReader.GetFloat(0);
                    }
                    
                }
                else
                {
                    pgSqlCommand = new PgSqlCommand("select value from dynamic_params " +
                        "where property_id = @value1 and object_id = @value2 " +
                        "and extract(MONTH FROM period) = extract(MONTH FROM '@value3'::DATE) " +
                        "and extract(YEAR FROM period) = extract(YEAR FROM '@value3'::DATE) order by period desc",
                    databaseManager.Connection);
                    pgSqlCommand.Parameters.Add("@value1", property_id);
                    pgSqlCommand.Parameters.Add("@value2", object_id);
                    pgSqlCommand.Parameters.Add("@value3", period);
                    pgSqlDataReader = pgSqlCommand.ExecuteReader();
                    pgSqlDataReader.Read(); pgSqlDataReader.Read();
                    if (pgSqlDataReader.HasRows)
                    {
                        result = pgSqlDataReader.GetFloat(0);
                    }
                }
            }
            return result;
        }
    }
}
