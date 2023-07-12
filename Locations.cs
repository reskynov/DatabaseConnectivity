using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace DatabaseConnectivity
{
    public class Locations
    {
        private static SqlConnection _connection = DatabaseConnection.Connection();

        public static void GetLocations()
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = _connection.CreateCommand();
                cmd.Connection = _connection;
                cmd.CommandText = "select * from locations";
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.Write("ID : " + reader.GetInt32(0));

                    string streetAddress = reader.IsDBNull(1) ? "EMPTY" : reader.GetString(1);
                    Console.Write(", Street Address : " + streetAddress);

                    string postalCode = reader.IsDBNull(2) ? "EMPTY" : reader.GetString(2);
                    Console.Write(", Postal Code : " + postalCode);

                    Console.Write(", City : " + reader.GetString(3));

                    string stateProvince = reader.IsDBNull(4) ? "EMPTY" : reader.GetString(4);
                    Console.Write(", State Province : " + stateProvince);

                    string countryId = reader.IsDBNull(5) ? "EMPTY" : reader.GetString(5);
                    Console.WriteLine(", Country ID : " + countryId);
                }
                reader.Close();
                _connection.Close();
            }
            catch
            {
                Console.WriteLine("Error connection to database");
            }
        }

        //Insert Countries
        public static void SetCountries(string inputId, string inputCountries, int inputRegionId)
        {
            _connection.Open();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "insert into countries (id, name, region_id) values (@id, @name, @region_id);";

            SqlTransaction transaction = _connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Char;
                pId.Value = inputId;
                cmd.Parameters.Add(pId);

                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.SqlDbType = System.Data.SqlDbType.VarChar;
                pName.Value = inputCountries;
                cmd.Parameters.Add(pName);

                SqlParameter pRegionId = new SqlParameter();
                pRegionId.ParameterName = "@region_id";
                pRegionId.SqlDbType = System.Data.SqlDbType.Int;
                pRegionId.Value = inputRegionId;
                cmd.Parameters.Add(pRegionId);

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

        //Update Region
        public static void UpdateCountries(string inputCountries, string idCountries)
        {
            _connection.Open();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "update countries set name = @name where id = @id;";

            SqlTransaction transaction = _connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.SqlDbType = System.Data.SqlDbType.VarChar;
                pName.Value = inputCountries;
                cmd.Parameters.Add(pName);

                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Char;
                pId.Value = idCountries;
                cmd.Parameters.Add(pId);

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
                //mengembalikan ke status awal sebelum diinsert
                transaction.Rollback();
                Console.WriteLine("Error connection to database");
            }
        }

        //Delete Region
        public static void DeleteCountries(string idCountries)
        {
            _connection.Open();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "delete from countries where id = @id;";

            SqlTransaction transaction = _connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Char;
                pId.Value = idCountries;
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
                //mengembalikan ke status awal sebelum diinsert
                transaction.Rollback();
                Console.WriteLine("Error connection to database");
            }
        }

        //Get By ID Region
        public static void GetByIdCountries(string idCountries)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = _connection.CreateCommand();
                cmd.Connection = _connection;
                cmd.CommandText = "select * from countries where id = @id";

                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Char;
                pId.Value = idCountries;
                cmd.Parameters.Add(pId);

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("FOUND");
                    Console.Write("ID : " + reader.GetString(0));
                    Console.Write(", Name : " + reader.GetString(1));
                    Console.WriteLine(", Region ID : " + reader.GetInt32(2));
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
