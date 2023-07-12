using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection.PortableExecutable;

namespace DatabaseConnectivity
{
    public class Histories
    {
        private static SqlConnection _connection = DatabaseConnection.Connection();

        //Get All
        public static void GetHistories()
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = _connection.CreateCommand();
                cmd.Connection = _connection;
                cmd.CommandText = "select * from histories";
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.Write("Start Date : " + reader.GetDateTime(0));

                    Console.Write(", Employee ID : " + reader.GetInt32(1));

                    DateTime endDate = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    Console.Write(", End Date : " + endDate);

                    Console.Write(", Department ID : "+ reader.GetInt32(3));
                    Console.WriteLine(", Job ID : " + reader.GetString(4));
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
        public static void SetHistories(int employeeId, int departmentId, string jobId)
        {
            _connection.Open();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "insert into histories (start_date, employee_id, end_date, department_id, job_id) " +
                "values (@startDate, @employeeId, @endDate, @departmentId, @jobId);";

            SqlTransaction transaction = _connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                DateTime startDate = DateTime.Now;
                SqlParameter pStart = new SqlParameter();
                pStart.ParameterName = "@startDate";
                pStart.SqlDbType = System.Data.SqlDbType.DateTime;
                pStart.Value = startDate;
                cmd.Parameters.Add(pStart);

                SqlParameter pEmId = new SqlParameter();
                pEmId.ParameterName = "@employeeId";
                pEmId.SqlDbType = System.Data.SqlDbType.Int;
                pEmId.Value = employeeId;
                cmd.Parameters.Add(pEmId);

                SqlParameter pEnd = new SqlParameter();
                pEnd.ParameterName = "@endDate";
                pEnd.SqlDbType = System.Data.SqlDbType.DateTime;
                bool wrongInput = true;
                //validasi input date
                while (wrongInput)
                {
                    wrongInput = false;
                    Console.Write("Input End Date (mm/dd/yyyy) : ");
                    string inputDate = Console.ReadLine();
                    //jika isian kosong maka buat value menjadi null
                    if (inputDate == "" || inputDate == " ")
                    {
                        pEnd.Value = DBNull.Value;
                    }
                    else
                    {
                        if(DateValidation(inputDate))
                        {
                            pEnd.Value = DateTime.ParseExact(inputDate, "d", new CultureInfo("en-US"));
                        }
                        else
                        {
                            wrongInput = true;
                        }
                    }
                }
                cmd.Parameters.Add(pEnd);

                SqlParameter pDeId = new SqlParameter();
                pDeId.ParameterName = "@departmentId";
                pDeId.SqlDbType = System.Data.SqlDbType.Int;
                pDeId.Value = departmentId;
                cmd.Parameters.Add(pDeId);

                SqlParameter pJobId = new SqlParameter();
                pJobId.ParameterName = "@jobId";
                pJobId.SqlDbType = System.Data.SqlDbType.Char;
                pJobId.Value = jobId;
                cmd.Parameters.Add(pJobId);

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
        public static void UpdateHistories(int employeeId, int departmentId, string jobId)
        {
            _connection.Open();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "update histories set end_date = @endDate " +
                "where employee_id = @employeeId and job_id = @jobId and department_id = @departmentId;";

            SqlTransaction transaction = _connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                SqlParameter pEmId = new SqlParameter();
                pEmId.ParameterName = "@employeeId";
                pEmId.SqlDbType = System.Data.SqlDbType.Int;
                pEmId.Value = employeeId;
                cmd.Parameters.Add(pEmId);

                SqlParameter pEnd = new SqlParameter();
                pEnd.ParameterName = "@endDate";
                pEnd.SqlDbType = System.Data.SqlDbType.DateTime;
                bool wrongInput = true;
                //validasi input date
                while (wrongInput)
                {
                    wrongInput = false;
                    Console.Write("Input End Date (mm/dd/yyyy) : ");
                    string inputDate = Console.ReadLine();
                    //jika isian kosong maka buat value menjadi null
                    if (inputDate == "" || inputDate == " ")
                    {
                        pEnd.Value = DBNull.Value;
                    }
                    else
                    {
                        if (DateValidation(inputDate))
                        {
                            pEnd.Value = DateTime.ParseExact(inputDate, "d", new CultureInfo("en-US"));
                        }
                        else
                        {
                            wrongInput = true;
                        }
                    }
                }
                cmd.Parameters.Add(pEnd);

                SqlParameter pDeId = new SqlParameter();
                pDeId.ParameterName = "@departmentId";
                pDeId.SqlDbType = System.Data.SqlDbType.Int;
                pDeId.Value = departmentId;
                cmd.Parameters.Add(pDeId);

                SqlParameter pJobId = new SqlParameter();
                pJobId.ParameterName = "@jobId";
                pJobId.SqlDbType = System.Data.SqlDbType.Char;
                pJobId.Value = jobId;
                cmd.Parameters.Add(pJobId);

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
        public static void DeleteHistories(int employeeId, int departmentId, string jobId)
        {
            _connection.Open();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "delete from histories where employee_id = @employeeId and job_id = @jobId and department_id = @departmentId;";

            SqlTransaction transaction = _connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                SqlParameter pEmId = new SqlParameter();
                pEmId.ParameterName = "@employeeId";
                pEmId.SqlDbType = System.Data.SqlDbType.Int;
                pEmId.Value = employeeId;
                cmd.Parameters.Add(pEmId);

                SqlParameter pDeId = new SqlParameter();
                pDeId.ParameterName = "@departmentId";
                pDeId.SqlDbType = System.Data.SqlDbType.Int;
                pDeId.Value = departmentId;
                cmd.Parameters.Add(pDeId);

                SqlParameter pJobId = new SqlParameter();
                pJobId.ParameterName = "@jobId";
                pJobId.SqlDbType = System.Data.SqlDbType.Char;
                pJobId.Value = jobId;
                cmd.Parameters.Add(pJobId);

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
        public static void GetByIdHistories(int idEmployee)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = _connection.CreateCommand();
                cmd.Connection = _connection;
                cmd.CommandText = "select * from histories where employee_id = @id";

                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Int;
                pId.Value = idEmployee;
                cmd.Parameters.Add(pId);

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.Write("Start Date : " + reader.GetDateTime(0));

                    Console.Write(", Employee ID : " + reader.GetInt32(1));

                    DateTime endDate = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    Console.Write(", End Date : " + endDate);

                    Console.Write(", Department ID : " + reader.GetInt32(3));
                    Console.WriteLine(", Job ID : " + reader.GetString(4));
                }
                reader.Close();
                _connection.Close();
            }
            catch
            {
                Console.WriteLine("Error connection to database");
            }
        }

        public static bool DateValidation(string inputDate)
        {
            try
            {
                var userDate = DateTime.ParseExact(inputDate, "d", new CultureInfo("en-US"));

                if (userDate > DateTime.Now)
                {
                    return true;
                }
                else
                {
                    Console.Write("Invalid Date. Please enter your date of birth (mm/dd/yyyy) and   ");
                    return false;
                }
            }
            catch
            {
                Console.Write("Invalid Date.  Please enter your date of birth (mm/dd/yyyy):  ");
                return false;
            }

        }
    }
}
