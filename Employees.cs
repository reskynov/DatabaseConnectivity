using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection.PortableExecutable;

namespace DatabaseConnectivity
{
    public class Employees
    {
        private static SqlConnection _connection = DatabaseConnection.Connection();

        //Get All
        public static void GetEmployees()
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = _connection.CreateCommand();
                cmd.Connection = _connection;
                cmd.CommandText = "select * from employees";
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.Write("ID : " + reader.GetInt32(0));
                    Console.Write(", First Name : " + reader.GetString(1));

                    string lastName = reader.IsDBNull(2) ? "EMPTY" : reader.GetString(2);
                    Console.Write(", Last Name : " + lastName);

                    Console.Write(", Email : " + reader.GetString(3));

                    string phone = reader.IsDBNull(4) ? "EMPTY" : reader.GetString(4);
                    Console.Write(", Phone Number : " + phone);

                    Console.Write(", Hire Date : " + reader.GetDateTime(5));

                    int salary = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                    Console.Write(", Salary : " + salary);

                    var commis = reader.IsDBNull(7) ? 0 : reader.GetDecimal(7);
                    Console.Write(", Commission : " + commis);

                    int managerId = reader.IsDBNull(8) ? 0 : reader.GetInt32(8);
                    string managerIdString = "";
                    if (managerId == 0)
                    {
                        managerIdString = "EMPTY";
                    }
                    else
                    {
                        managerIdString = managerId.ToString();
                    }
                    Console.WriteLine(", Manager ID : " + managerIdString);

                    Console.Write("Job ID : " + reader.GetString(9));
                    Console.WriteLine("Department ID : " + reader.GetInt32(10));
                    Console.WriteLine();
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
        public static void SetEmployees(int id, string fName, string lName, string email, string phone, int salary, double commis, int managerId, string jobId, int departmentId)
        {
            _connection.Open();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "insert into employees (id, first_name, last_name, email, phone_number, hire_date, salary, comission_pct, manager_id, job_id, department_id) " +
                "values (@id, @fname, @lname, @email, @phone, @hire, @salary, @commis, @managerId, @jobId, @departmentId);";

            SqlTransaction transaction = _connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Int;
                pId.Value = id;
                cmd.Parameters.Add(pId);

                SqlParameter pFName = new SqlParameter();
                pFName.ParameterName = "@fname";
                pFName.SqlDbType = System.Data.SqlDbType.VarChar;
                pFName.Value = fName;
                cmd.Parameters.Add(pFName);

                SqlParameter pLName = new SqlParameter();
                pLName.ParameterName = "@lname";
                pLName.SqlDbType = System.Data.SqlDbType.VarChar;
                pLName.Value = lName;
                cmd.Parameters.Add(pLName);

                SqlParameter pEmail = new SqlParameter();
                pEmail.ParameterName = "@email";
                pEmail.SqlDbType = System.Data.SqlDbType.VarChar;
                pEmail.Value = email;
                cmd.Parameters.Add(pEmail);

                SqlParameter pPhone = new SqlParameter();
                pPhone.ParameterName = "@phone";
                pPhone.SqlDbType = System.Data.SqlDbType.VarChar;
                pPhone.Value = phone;
                cmd.Parameters.Add(pPhone);

                SqlParameter pHire = new SqlParameter();
                pHire.ParameterName = "@hire";
                pHire.SqlDbType = System.Data.SqlDbType.DateTime;
                pHire.Value = DateTime.Now;
                cmd.Parameters.Add(pHire);

                SqlParameter pSalary = new SqlParameter();
                pSalary.ParameterName = "@salary";
                pSalary.SqlDbType = System.Data.SqlDbType.Int;
                pSalary.Value = salary;
                cmd.Parameters.Add(pSalary);

                SqlParameter pCommis = new SqlParameter();
                pCommis.ParameterName = "@commis";
                pCommis.SqlDbType = System.Data.SqlDbType.Decimal;
                pCommis.Value = commis;
                cmd.Parameters.Add(pCommis);

                SqlParameter pManId = new SqlParameter();
                pManId.ParameterName = "@managerId";
                pManId.SqlDbType = System.Data.SqlDbType.Int;
                pManId.Value = managerId;
                cmd.Parameters.Add(pManId);

                SqlParameter pJobId = new SqlParameter();
                pJobId.ParameterName = "@jobId";
                pJobId.SqlDbType = System.Data.SqlDbType.Int;
                pJobId.Value = jobId;
                cmd.Parameters.Add(pJobId);

                SqlParameter pDeId = new SqlParameter();
                pDeId.ParameterName = "@departmentId";
                pDeId.SqlDbType = System.Data.SqlDbType.Int;
                pDeId.Value = departmentId;
                cmd.Parameters.Add(pDeId);

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
        public static void UpdateEmployees(int id, string fName, string lName, string email, string phone, int salary, double commis, int managerId, string jobId, int departmentId)
        {
            _connection.Open();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "update employees set first_name = @fname, last_name = @lname, email = @email, phone_number = @phone, salary = @salary, comission_pct = @commis, manager_id = @managerId, job_id = @jobId, department_id = @departmentId " +
                "where id = @id;";

            SqlTransaction transaction = _connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Int;
                pId.Value = id;
                cmd.Parameters.Add(pId);

                SqlParameter pFName = new SqlParameter();
                pFName.ParameterName = "@fname";
                pFName.SqlDbType = System.Data.SqlDbType.VarChar;
                pFName.Value = fName;
                cmd.Parameters.Add(pFName);

                SqlParameter pLName = new SqlParameter();
                pLName.ParameterName = "@lname";
                pLName.SqlDbType = System.Data.SqlDbType.VarChar;
                pLName.Value = lName;
                cmd.Parameters.Add(pLName);

                SqlParameter pEmail = new SqlParameter();
                pEmail.ParameterName = "@email";
                pEmail.SqlDbType = System.Data.SqlDbType.VarChar;
                pEmail.Value = email;
                cmd.Parameters.Add(pEmail);

                SqlParameter pPhone = new SqlParameter();
                pPhone.ParameterName = "@phone";
                pPhone.SqlDbType = System.Data.SqlDbType.VarChar;
                pPhone.Value = phone;
                cmd.Parameters.Add(pPhone);

                SqlParameter pSalary = new SqlParameter();
                pSalary.ParameterName = "@salary";
                pSalary.SqlDbType = System.Data.SqlDbType.Int;
                pSalary.Value = salary;
                cmd.Parameters.Add(pSalary);

                SqlParameter pCommis = new SqlParameter();
                pCommis.ParameterName = "@commis";
                pCommis.SqlDbType = System.Data.SqlDbType.Decimal;
                pCommis.Value = commis;
                cmd.Parameters.Add(pCommis);

                SqlParameter pManId = new SqlParameter();
                pManId.ParameterName = "@managerId";
                pManId.SqlDbType = System.Data.SqlDbType.Int;
                pManId.Value = managerId;
                cmd.Parameters.Add(pManId);

                SqlParameter pJobId = new SqlParameter();
                pJobId.ParameterName = "@jobId";
                pJobId.SqlDbType = System.Data.SqlDbType.Int;
                pJobId.Value = jobId;
                cmd.Parameters.Add(pJobId);

                SqlParameter pDeId = new SqlParameter();
                pDeId.ParameterName = "@departmentId";
                pDeId.SqlDbType = System.Data.SqlDbType.Int;
                pDeId.Value = departmentId;
                cmd.Parameters.Add(pDeId);


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
        public static void DeleteEmployees(int idEmployees)
        {
            _connection.Open();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "delete from employees where id = @id;";

            SqlTransaction transaction = _connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Int;
                pId.Value = idEmployees;
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
        public static void GetByIdEmployees(int idEmployees)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = _connection.CreateCommand();
                cmd.Connection = _connection;
                cmd.CommandText = "select * from employees where id = @id";

                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Int;
                pId.Value = idEmployees;
                cmd.Parameters.Add(pId);

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.Write("ID : " + reader.GetInt32(0));
                    Console.Write(", First Name : " + reader.GetString(1));

                    string lastName = reader.IsDBNull(2) ? "EMPTY" : reader.GetString(2);
                    Console.Write(", Last Name : " + lastName);

                    Console.Write(", Email : " + reader.GetString(3));

                    string phone = reader.IsDBNull(4) ? "EMPTY" : reader.GetString(4);
                    Console.Write(", Phone Number : " + phone);

                    Console.Write(", Hire Date : " + reader.GetDateTime(5));

                    int salary = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                    Console.Write(", Salary : " + salary);

                    var commis = reader.IsDBNull(7) ? 0 : reader.GetDecimal(7);
                    Console.Write(", Commission : " + commis);

                    int managerId = reader.IsDBNull(8) ? 0 : reader.GetInt32(8);
                    string managerIdString = "";
                    if (managerId == 0)
                    {
                        managerIdString = "EMPTY";
                    }
                    else
                    {
                        managerIdString = managerId.ToString();
                    }
                    Console.WriteLine(", Manager ID : " + managerIdString);

                    Console.Write("Job ID : " + reader.GetString(9));
                    Console.WriteLine("Department ID : " + reader.GetInt32(10));
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
