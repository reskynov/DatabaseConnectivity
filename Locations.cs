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
        public static void SetLocations(int inputId, string inputStreet, string inputPostal, string inputCity, string inputState, string inputCountryId)
        {
            _connection.Open();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "insert into locations (id, street_address, postal_code, city, state_province, country_id) " +
                "values (@id, @street, @postal, @city, @state, @countryId);";

            SqlTransaction transaction = _connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Int;
                pId.Value = inputId;
                cmd.Parameters.Add(pId);

                SqlParameter pStreet = new SqlParameter();
                pStreet.ParameterName = "@street";
                pStreet.SqlDbType = System.Data.SqlDbType.VarChar;
                pStreet.Value = inputStreet;
                cmd.Parameters.Add(pStreet);

                SqlParameter pPostal = new SqlParameter();
                pPostal.ParameterName = "@postal";
                pPostal.SqlDbType = System.Data.SqlDbType.VarChar;
                pPostal.Value = inputPostal;
                cmd.Parameters.Add(pPostal);

                SqlParameter pCity = new SqlParameter();
                pCity.ParameterName = "@city";
                pCity.SqlDbType = System.Data.SqlDbType.VarChar;
                pCity.Value = inputCity;
                cmd.Parameters.Add(pCity);

                SqlParameter pState = new SqlParameter();
                pState.ParameterName = "@state";
                pState.SqlDbType = System.Data.SqlDbType.VarChar;
                pState.Value = inputState;
                cmd.Parameters.Add(pState);

                SqlParameter pCountryId = new SqlParameter();
                pCountryId.ParameterName = "@countryId";
                pCountryId.SqlDbType = System.Data.SqlDbType.Char;
                pCountryId.Value = inputCountryId;
                cmd.Parameters.Add(pCountryId);

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
        public static void UpdateLocations(int inputId, string inputStreet, string inputPostal, string inputCity, string inputState)
        {
            _connection.Open();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "update locations set street_address = @street, postal_code = @postal, city = @city, state_province = @state " +
                "where id = @id;";

            SqlTransaction transaction = _connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Int;
                pId.Value = inputId;
                cmd.Parameters.Add(pId);

                SqlParameter pStreet = new SqlParameter();
                pStreet.ParameterName = "@street";
                pStreet.SqlDbType = System.Data.SqlDbType.VarChar;
                pStreet.Value = inputStreet;
                cmd.Parameters.Add(pStreet);

                SqlParameter pPostal = new SqlParameter();
                pPostal.ParameterName = "@postal";
                pPostal.SqlDbType = System.Data.SqlDbType.VarChar;
                pPostal.Value = inputPostal;
                cmd.Parameters.Add(pPostal);

                SqlParameter pCity = new SqlParameter();
                pCity.ParameterName = "@city";
                pCity.SqlDbType = System.Data.SqlDbType.VarChar;
                pCity.Value = inputCity;
                cmd.Parameters.Add(pCity);

                SqlParameter pState = new SqlParameter();
                pState.ParameterName = "@state";
                pState.SqlDbType = System.Data.SqlDbType.VarChar;
                pState.Value = inputState;
                cmd.Parameters.Add(pState);

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

        //Delete Region
        public static void DeleteLocations(int idLocation)
        {
            _connection.Open();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "delete from locations where id = @id;";

            SqlTransaction transaction = _connection.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Int;
                pId.Value = idLocation;
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

        //Get By ID Region
        public static void GetByIdLocations(int idLocations)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = _connection.CreateCommand();
                cmd.Connection = _connection;
                cmd.CommandText = "select * from locations where id = @id";

                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Int;
                pId.Value = idLocations;
                cmd.Parameters.Add(pId);

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
    }
}
