using MySql.Data.MySqlClient;

namespace codep.DAL
{
    public class DbConfig
    {
        private static MySqlConnection? connection;
        public static MySqlConnection GetConnection()
        {
            if(connection == null)
            {
                connection = new MySqlConnection
                {
                    ConnectionString = "server=localhost;Database=MSHOP;userid=root;password=mongduong1;port=3306;"
                };
            }
            return connection;
        }
        public static MySqlConnection OpenConnection()
        {
            if(connection == null)
            { 
                GetConnection();
            }
            connection!.Open();
            return connection;
        }
        public static void CloseConnection()
        {
            if(connection!= null)
            {
                connection.Close();
            }
        }
    }
}