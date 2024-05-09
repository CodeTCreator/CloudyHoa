using Devart.Data.PostgreSql;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceHoaAccount" in both code and config file together.
    public class ServiceHoaAccount : IServiceHoaAccount
    {

        static IConfiguration configurationDB = new ConfigurationBuilder().AddJsonFile("AppSettings.json").Build();
        readonly string connectionString = configurationDB["AppSettings:DatabaseConnection"];

        public void AddAccount(string Name,string Login, string Password)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("INSERT INTO hoa  (Name, login, password) VALUES (@name,@login,@password)"
                    , conn);
                pgSqlCommand.Parameters.Add("@name", Name);
                pgSqlCommand.Parameters.Add("@login", Login);
                pgSqlCommand.Parameters.Add("@password", Password);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }
        public bool CheckAccout(string Name)
        {
            bool flag = true;
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Select * from hoa where name = @Name", conn);
                pgSqlCommand.Parameters.Add("@Name", Name);
                using (PgSqlDataReader reader = pgSqlCommand.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        flag = false;
                    }
                }
                conn.Close();
            }
            return flag;
        }
        public void DeleteAccount(int Id)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("Delete from hoa where id = @id_value", conn);
                pgSqlCommand.Parameters.Add("@id_value", Id);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void EditAccount(int Id, string Name, string Login, string Password)
        {
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("UPDATE hoa SET name = @name, login = @login," +
                    "password = @pass where id = @id", conn);
                pgSqlCommand.Parameters.Add("@id", Id);
                pgSqlCommand.Parameters.Add("@name", Name);
                pgSqlCommand.Parameters.Add("@login", Login);
                pgSqlCommand.Parameters.Add("@pass", Password);
                pgSqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }
        public DataSet GetTableOfHoa()
        {
            DataSet dataSet = new DataSet();
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select * from hoa", conn);
                using (PgSqlDataAdapter pgSqlDataAdapter = new PgSqlDataAdapter(pgSqlCommand))
                {
                    pgSqlDataAdapter.Fill(dataSet);
                }
                conn.Close();
            }
            return dataSet;
        }
        /// <summary>
        /// Функция проверяет введенные данные. В случае успеха возвращает id УК,в ином случае пустая 
        /// строка
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Authorization(string login, string password)
        {
            int hoaId = -1;
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select id from hoa where login = @login and " +
                    "password = @password", conn);
                pgSqlCommand.Parameters.Add("@login", login);
                pgSqlCommand.Parameters.Add("@password", password);
                using (PgSqlDataReader pgSqlDataAdapter = pgSqlCommand.ExecuteReader())
                {
                    pgSqlDataAdapter.Read();
                    if (pgSqlDataAdapter.HasRows) 
                    {
                        hoaId = pgSqlDataAdapter.GetInt32(0);
                    }
                }
                conn.Close();
            }
            return hoaId;
        }

        public string GetAccountName(int hoaId)
        {
            string name = null;
            using (PgSqlConnection conn = new PgSqlConnection(connectionString))
            {
                conn.Open();
                PgSqlCommand pgSqlCommand = new PgSqlCommand("select name from hoa where id = @hoaId ", conn);
                pgSqlCommand.Parameters.Add("@hoaId", hoaId);
                using (PgSqlDataReader pgSqlDataAdapter = pgSqlCommand.ExecuteReader())
                {
                    pgSqlDataAdapter.Read();
                    if (pgSqlDataAdapter.HasRows)
                    {
                        name = pgSqlDataAdapter.GetString(0);
                    }
                }
                conn.Close();
            }
            return name;
        }
    }
}
