using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection.PortableExecutable;

namespace DatabaseConnectivity
{
    public class Departments
    {
        private static SqlConnection _connection = DatabaseConnection.Connection();

        //Get All
        public static void GetJobs()
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = _connection.CreateCommand();
                cmd.Connection = _connection;
                cmd.CommandText = "select * from jobs order by cast(id as int) ASC";
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.Write("ID : " + reader.GetString(0));

                    Console.Write(", Title : " + reader.GetString(1));

                    int minSalary = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                    int maxSalary = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                    Console.WriteLine(", Salary Range : $" + minSalary + " - $" + maxSalary);
                }
                reader.Close();
                _connection.Close();
            }
            catch
            {
                Console.WriteLine("Error connection to database");
            }
        }

        //Insert
        public static void SetJobs(string inputId, string inputTitle, int minSalary, int maxSalary)
        {
            _connection.Open();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "insert into jobs (id, title, min_salary, max_salary) " +
                "values (@id, @title, @min, @max);";

            SqlTransaction transaction = _connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Char;
                pId.Value = inputId;
                cmd.Parameters.Add(pId);

                SqlParameter pTitle = new SqlParameter();
                pTitle.ParameterName = "@title";
                pTitle.SqlDbType = System.Data.SqlDbType.VarChar;
                pTitle.Value = inputTitle;
                cmd.Parameters.Add(pTitle);

                SqlParameter pMin = new SqlParameter();
                pMin.ParameterName = "@min";
                pMin.SqlDbType = System.Data.SqlDbType.Int;
                pMin.Value = minSalary;
                cmd.Parameters.Add(pMin);

                SqlParameter pMax = new SqlParameter();
                pMax.ParameterName = "@max";
                pMax.SqlDbType = System.Data.SqlDbType.Int;
                pMax.Value = maxSalary;
                cmd.Parameters.Add(pMax);

                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Insert Success");
                    transaction.Commit();
                }
                else
                {
                    Console.WriteLine("Insert Failed");
                }
                _connection.Close();
            }
            catch
            {
                //mengembalikan ke status awal sebelum diinsert
                transaction.Rollback();
                Console.WriteLine("Error connection to database");
            }
        }

        //Update
        public static void UpdateJobs(string inputId, string inputTitle, int minSalary, int maxSalary)
        {
            _connection.Open();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "update jobs set title = @title, min_salary = @min, max_salary = @max " +
                "where id = @id;";

            SqlTransaction transaction = _connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Char;
                pId.Value = inputId;
                cmd.Parameters.Add(pId);

                SqlParameter pTitle = new SqlParameter();
                pTitle.ParameterName = "@title";
                pTitle.SqlDbType = System.Data.SqlDbType.VarChar;
                pTitle.Value = inputTitle;
                cmd.Parameters.Add(pTitle);

                SqlParameter pMin = new SqlParameter();
                pMin.ParameterName = "@min";
                pMin.SqlDbType = System.Data.SqlDbType.Int;
                pMin.Value = minSalary;
                cmd.Parameters.Add(pMin);

                SqlParameter pMax = new SqlParameter();
                pMax.ParameterName = "@max";
                pMax.SqlDbType = System.Data.SqlDbType.Int;
                pMax.Value = maxSalary;
                cmd.Parameters.Add(pMax);

                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Update Success");
                    transaction.Commit();
                }
                else
                {
                    Console.WriteLine("Update Failed");
                }
                _connection.Close();
            }
            catch
            {
                //mengembalikan ke status awal sebelum diupdate
                transaction.Rollback();
                Console.WriteLine("Error connection to database");
            }
        }

        //Delete
        public static void DeleteJobs(string idJobs)
        {
            _connection.Open();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "delete from jobs where id = @id;";

            SqlTransaction transaction = _connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Int;
                pId.Value = idJobs;
                cmd.Parameters.Add(pId);

                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Delete Success");
                    transaction.Commit();
                }
                else
                {
                    Console.WriteLine("Delete Failed");
                }
                _connection.Close();
            }
            catch
            {
                //mengembalikan ke status awal sebelum didelete
                transaction.Rollback();
                Console.WriteLine("Error connection to database");
            }
        }

        //Get By ID
        public static void GetByIdJobs(string idJobs)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = _connection.CreateCommand();
                cmd.Connection = _connection;
                cmd.CommandText = "select * from jobs where id = @id";

                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Int;
                pId.Value = idJobs;
                cmd.Parameters.Add(pId);

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("FOUND");
                    Console.Write("ID : " + reader.GetString(0));

                    Console.Write(", Title : " + reader.GetString(1));

                    int minSalary = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                    int maxSalary = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                    Console.WriteLine(", Salary Range : $" + minSalary + " - $" + maxSalary);
                }
                reader.Close();
                _connection.Close();
            }
            catch
            {
                Console.WriteLine("Error connection to database");
            }
        }
    }
}
