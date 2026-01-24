using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;


namespace ContactsDataAccessLayer
{
    public static class clsContactDataAccess
    {
        

      public   static bool getContactInfoByID(ref int ID, ref string firstName, ref string lastName, ref string email, ref string phone, ref string address, DateTime dateOfBirth, ref string imagePath, ref int countryID)
        {
            SqlConnection connection = new SqlConnection(clsContactDataAccessSettings.connectionString);

            bool isFound = false;
            string query = "Select * from contacts where ContactID=@ContactID";

            SqlCommand cmd = new SqlCommand(query,connection);

            cmd.Parameters.AddWithValue(@"contactID", ID);

            try
            {
                connection.Open();

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    ID = (int)read["ContactID"];
                    firstName = (string)read["FirstName"];
                    lastName = (string)read["lastName"];
                    email = (string)read["email"];
                    phone = (string)read["phone"];
                    address = (string)read["address"];
                    countryID = (int)read["CountryID"];

                    //[TR]
                    // NUllable kolon değerlerini DB'den alırken ve DB'e eklerken kontrol etmelisin. 
                    //Özellikle DB'den boş olup olmadığını kontrol edip sonra cast yapıyoruz.
                    if (read["imagePath"]==System.DBNull.Value)
                    {
                        imagePath = "";
                    }
                    else
                    {
                        imagePath = read["imagePath"].ToString();
                    }


                        isFound = true;

                }

                read.Close();

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



        public static int addContactToDatabase(string FirstName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {

            SqlConnection connection = new SqlConnection(clsContactDataAccessSettings.connectionString);



            string query = "insert into contacts values(@firstName,@lastName,@email,@phone,@address,@DateOfBirth,@countryID,@ImagePath); Select Scope_Identity()";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue(@"firstName", FirstName);
            cmd.Parameters.AddWithValue(@"lastName", LastName);
            cmd.Parameters.AddWithValue(@"email", Email);
            cmd.Parameters.AddWithValue(@"phone", Phone);
            cmd.Parameters.AddWithValue(@"address", Address);
            cmd.Parameters.AddWithValue(@"DateOfBirth", DateOfBirth.ToString());
            cmd.Parameters.AddWithValue(@"countryID", CountryID);

            if (ImagePath == string.Empty)
            {

                //[TR]
                //Eğer image path boş ise DB'e boş string göndermek yerine *System.DBNull.Value*
                //komutu ile database'de hücreye NULL değeri kaydetmiş oluruz.
                cmd.Parameters.AddWithValue(@"ImagePath", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);

            }
            try
            {
                connection.Open();

                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int inserted))
                {
                    return inserted;
                }
                else
                {
                    return -1;
                }


            }
            catch (Exception)
            {

                //Log files will be here
                return -1;
            }
            finally
            {
                connection.Close();

            }

        }



        public static bool  updateContactInfo(int contactID, string FirstName, string LastName, string Email,
        string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            int affectedRows = 0;
            SqlConnection connection = new SqlConnection(clsContactDataAccessSettings.connectionString);



            string query =
                "update contacts set FirstName=@firstName,LastName=@lastName,Email=@email,Phone=@phone,Address=@address,DateOfBirth=@DateOfBirth,CountryID=@countryID,imagePath=@ImagePath where contactID = @contactID;";



            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue(@"firstName", FirstName);
            cmd.Parameters.AddWithValue(@"lastName", LastName);
            cmd.Parameters.AddWithValue(@"email", Email);
            cmd.Parameters.AddWithValue(@"phone", Phone);
            cmd.Parameters.AddWithValue(@"address", Address);
            cmd.Parameters.AddWithValue(@"DateOfBirth", DateOfBirth.ToString());
            cmd.Parameters.AddWithValue(@"countryID", CountryID);
            cmd.Parameters.AddWithValue(@"contactID", contactID);


            if (ImagePath == string.Empty)
            {

                //[TR]
                //Eğer image path boş ise DB'e boş string göndermek yerine *System.DBNull.Value*
                //komutu ile database'de hücreye NULL değeri kaydetmiş oluruz.
                cmd.Parameters.AddWithValue(@"ImagePath", System.DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);

            }
    

            try
            {
                connection.Open();

                affectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                return false;
            }
            finally
            {
                connection.Close();
            }

            return (affectedRows > 0);

        }




        public static bool deleteContact(int contactID)
        {
            int affectedRows = 0;
            SqlConnection connection = new SqlConnection(clsContactDataAccessSettings.connectionString);

            string query =  "delete contacts where  ContactID=@ContactID  ";
            
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue(@"ContactID", contactID);

            try
            {
                connection.Open();

                affectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                return false;
            }
            finally
            {
                connection.Close();
            }

            return (affectedRows > 0);

        }


        public static DataTable getAllContacts()
        {

            DataTable tb = new DataTable();
            SqlConnection connection = new SqlConnection(clsContactDataAccessSettings.connectionString);

            string query = "select * from contacts ";

            SqlCommand cmd = new SqlCommand(query, connection);
           

            try
            {
                connection.Open();

                SqlDataReader read=  cmd.ExecuteReader();

                if (read.HasRows)
                {
                    tb.Load(read);
                }
                read.Close();
            }
            catch (Exception ex)
            {

              //  return null;
            }
            finally
            {
                connection.Close();
            }

            return tb;

        }

        public  static bool isContactExist(int contactID)
        {
       
            SqlConnection connection = new SqlConnection(clsContactDataAccessSettings.connectionString);

            string query = "select found=1  from contacts where  ContactID=@ContactID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue(@"ContactID", contactID);

            try
            {
                connection.Open();

                object result = cmd.ExecuteScalar();
                if (int.TryParse(result.ToString(),out int value))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

              // return false;
            }
            finally
            {
                connection.Close();
            }

            return false; ;
        }


       
      



    }


}
