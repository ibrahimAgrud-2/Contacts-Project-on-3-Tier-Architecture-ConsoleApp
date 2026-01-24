using System;
using System.Data;
using System.Data.SqlClient;


namespace ContactsDataAccessLayer
{
   public static class clsCountryData
    {
        public static bool findCountryByID(ref int countryID, ref string countryName,ref string code,ref string phoneCode)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsContactDataAccessSettings.connectionString);

            string query = "Select * from countries where countryID=@countryID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue(@"countryID", countryID);

            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    countryID = (int)read["countryID"];
                    countryName = (string)read["countryName"];
                    code = (string)read["Code"];
                    phoneCode = (string)read["phoneCode"];

                    isFound = true;

                }
            }
            catch (Exception)
            {
                isFound = false;
            }

            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool findCountryByName(ref int countryID, ref string countryName, ref string code, ref string phoneCode)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsContactDataAccessSettings.connectionString);

            string query = "Select * from countries where countryName=@countryName";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue(@"countryName", countryName);

            try
            {
                connection.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    countryID = (int)read["countryID"];
                    countryName = (string)read["countryName"];
                    code = (string)read["Code"];
                    phoneCode = (string)read["phoneCode"];

                    isFound = true;

                }
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }



        public static bool isCountryExistByID(int countryID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsContactDataAccessSettings.connectionString);

            string query = "Select found=1 from Countries where countryID=@countryID ";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue(@"countryID", countryID);


            try
            {
                connection.Open();

                object result = cmd.ExecuteScalar();

                if (int.TryParse(result.ToString(), out int myID))
                {

                    isFound = true;
                }
                else
                {
                    isFound = false;
                }
            }
            catch (Exception)
            {


            }
            finally
            {
                connection.Close();
            }


            return isFound;
        }

        public static bool isCountryExistByName(string countryName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsContactDataAccessSettings.connectionString);

            string query = "Select found=1 from Countries where countryName=@countryName ";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue(@"countryName", countryName);


            try
            {
                connection.Open();

                object result = cmd.ExecuteScalar();

                if (result!=null&&int.TryParse(result.ToString(), out int myID))
                {

                    isFound = true;
                }
                else
                {
                    isFound = false;
                }
            }
            catch (Exception)
            {

                //
            }
            finally
            {
                connection.Close();
            }


            return isFound;
        }


        public static int addNewCountry(string countryName,string code,string phoneCode)
        {
        

           

            using (SqlConnection connection = new SqlConnection(clsContactDataAccessSettings.connectionString))
            {
                string query = "insert into countries values(@countryName,@Code,@PhoneCode); select SCOPE_IDENTITY()";

              
                    try
                    {
                         using (SqlCommand cmd = new SqlCommand(query, connection))
                         {
                            cmd.Parameters.AddWithValue(@"countryName", countryName);
                            cmd.Parameters.AddWithValue(@"Code", (code == string.Empty) ? (Object)System.DBNull.Value : code);
                            cmd.Parameters.AddWithValue(@"PhoneCode", (phoneCode == string.Empty) ? (Object)System.DBNull.Value : phoneCode);

                            connection.Open();

                            object result = cmd.ExecuteScalar();

                            if (result != null && int.TryParse(result.ToString(), out int newValue))
                            {
                                return newValue;
                            }
                            else
                            {
                                return -1;
                            }
                          }
                    }
                    catch (Exception)
                    {

                        return -1;
                    }
                  
                

            }

        }
       

        public static bool updateCountry(int countryID,string countryName,string code,string phoneCode)
        {
            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(clsContactDataAccessSettings.connectionString);

            string query = "update countries set countryName =@countryName,code=@code,phoneCode=@phoneCode where countryID=@countryID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue(@"countryName", countryName);
            cmd.Parameters.AddWithValue(@"countryID", countryID);
            cmd.Parameters.AddWithValue(@"code", code);
            cmd.Parameters.AddWithValue(@"PhoneCode", phoneCode);



            try
            {
                connection.Open();

                affectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                
            }
            finally
            {
                connection.Close();
            }






            return (affectedRows>0);
        }
        public static bool deleteCountry(int countryID)
        {
            int affectedRows = 0;
            SqlConnection connection = new SqlConnection(clsContactDataAccessSettings.connectionString);

            string query = "delete countries where countryID=@countryID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue(@"countryID", countryID);

            try
            {
                connection.Open();

                affectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

               
            }
            finally
            {
                connection.Close();
            }

            return (affectedRows > 0);

        }

        public static DataTable getAllCountries()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsContactDataAccessSettings.connectionString);

            string query = "select * from countries";

            SqlCommand cmd = new SqlCommand(query,connection);

            try
            {
                connection.Open();

                SqlDataReader read = cmd.ExecuteReader();

                if (read.HasRows)
                {
                    dt.Load(read);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;


        }

   }

}
