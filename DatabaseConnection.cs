using System;
using System.Data.SqlClient;

namespace DatabaseConnectivity
{
    public class DatabaseConnection
    {
        public static SqlConnection Connection()
        {
            string _connectionString =
                "Data Source=RESKYEH;" +
                "Database=db_tugas;" +
                "Integrated Security=True;" +
                "Connect Timeout=30;";

            SqlConnection _connection = new SqlConnection(_connectionString);

            return _connection;
    }


    
    }
}
