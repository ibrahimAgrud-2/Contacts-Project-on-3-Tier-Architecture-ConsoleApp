using System;
using System.Data;
using ContactsBusinessLayer;
namespace ContactsConsoleAPP___PresentationLayer
{
    internal class Program
    {


        static void readContactInfo(ref clsContact contact)
        {
            
            Console.Write("Enter First name: ");
            contact.firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            contact.lastName = Console.ReadLine();
            Console.Write("Enter Email: ");
            contact.email = Console.ReadLine();
            Console.Write("Enter Phone: ");
            contact.phone = Console.ReadLine();
            Console.Write("Enter Address: ");
            contact.address = Console.ReadLine();
            Console.Write("Enter Date of Birth(day-month-year): ");
  
            DateTime dateOfBirth = DateTime.Now;
            while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
            {
                Console.Write("Date of Birth format must be day-month-year: ");

            }
            contact.dateOfBirth = dateOfBirth;

            Console.Write("Enter countryID: ");
            int countryID = 0;
            while (!int.TryParse(Console.ReadLine(), out countryID))
            {
                Console.Write("CountryID must be integer: ");
            }
            contact.countryID = countryID;
        }
        static void addNewContact()
        {

            clsContact contact = new clsContact();
            readContactInfo(ref contact);
           
            if (contact.save())
            {
                Console.WriteLine("Contact Added Successfully with id=" + contact.ID);
            }
            else
            {
                Console.WriteLine("Adding failed");

            }
        }
   

        
        static void updateContact(int contactID)
        {
            if (!clsContact.isContactExist(contactID))
            {
                Console.WriteLine("Contact does not exist");
                return;
            }

            clsContact contact =clsContact.find(contactID);

            displayContact(contact);

            readContactInfo(ref contact);

            if (contact.save())
            {
                Console.WriteLine("Contact with [{0}] ID updated Successfully", contactID);
                displayContact(contact);
            }
            else
            {
                Console.WriteLine("Update Failed!!");
            }


        }

        static void displayContact(clsContact contact)
        {
            if (contact==null)
            {
                Console.WriteLine("No valid contact to display");

                return;
            }
            Console.WriteLine("\n________________Contact Info_____________________");
            Console.WriteLine("ID           : {0}", contact.ID);
            Console.WriteLine("First name   : {0}", contact.firstName);
            Console.WriteLine("Last name    : {0}", contact.lastName);
            Console.WriteLine("Email        : {0}", contact.email);
            Console.WriteLine("Phone        : {0}", contact.phone);
            Console.WriteLine("Address      : {0}", contact.address);
            Console.WriteLine("Date of birth: {0}", contact.dateOfBirth);
            Console.WriteLine("Image path   : {0}", contact.imagePath);
            Console.WriteLine("Country ID   : {0}", contact.countryID);
            Console.WriteLine("__________________________________________________");

        }
   

        static void deleteContact(int contactID)
        {
            if (clsContact.isContactExist(contactID))
            {
               
                 if (clsContact.deleteContact(contactID))
                 {
                
                Console.WriteLine("Contact Deleted");
    
                 }
                 else
                   {
                Console.WriteLine("Delete process canceled");

                  }
              
            }
            else
            {
                Console.WriteLine("Contact does not exist");
            }





        }
        
        static void getAllContacts()
        {
            DataTable tb = new DataTable();

            tb = clsContact.getAllContacts();

            Console.WriteLine("__________________Contacts _____________________");
            foreach (DataRow rom in tb.Rows)
            {
                Console.WriteLine($"{rom["contactID"]},{rom["Firstname"]},{rom["lastName"]}");
            }
        }

        //Test Country Business


        static void displayCountry(clsCountry country)
        {
            if (country == null)
            {
                Console.WriteLine("No valid contact to display");

                return;
            }
            Console.WriteLine("\n________________Contact Info_____________________\n");
            Console.WriteLine("ID         : {0}", country.ID);
            Console.WriteLine("Country    : {0}", country.countryName);
            Console.WriteLine("Phone Code :  {0}", country.phoneCode);
            Console.WriteLine("Code       :  {0}", country.Code);

            Console.WriteLine("__________________________________________________");

        }

        static void findCountryByID(int ID)
        {
            clsCountry country1 = clsCountry.find(ID);

            if (country1!=null)
            {
                displayCountry(country1);
            }
            else
            {
                Console.WriteLine("Country with [{0}] could not found",ID);
            }
        }
        static void findCountryByName(string countryName)
        {
            clsCountry country1 = clsCountry.find(countryName);

            if (country1 != null)
            {
                displayCountry(country1);
            }
            else
            {
                Console.WriteLine("[{0}] could not found", countryName);
            }
        }
     

        static void isCountryExistByID(int ID)
        {
            if (clsCountry.isCountryExistByID(ID))
            {
                Console.WriteLine("Country exists");
            }
            else
            {
                Console.WriteLine("Country does not exists");

            }

        }
        static void isCountryExistByName(string  countryName)
        {
            if (clsCountry.isCountryExistByName(countryName))
            {
                Console.WriteLine("{0} exists in database",countryName);
            }
            else
            {
                Console.WriteLine("{0} does not exists in database",countryName);

            }

        }
        
        static void addNewCountry(string countryName,string code,string phoneCode)
        {
            if (clsCountry.isCountryExistByName(countryName))
            {
                Console.WriteLine("{0} is already exists", countryName);
                return;
                
            }
            clsCountry country1 = new clsCountry();
            country1.countryName = countryName;
            country1.Code = code;
            country1.phoneCode = phoneCode;


            if (country1.save())
            {
                Console.WriteLine("{0} added successfully", countryName);

            }
            else
            {
                Console.WriteLine("{0} could not be added", countryName);
            }

        }

        static void updateCountry(int countryID)
        {
            if (!clsCountry.isCountryExistByID(countryID))
            {
                Console.WriteLine("{0} is not exists", countryID);
                return;

            }
            clsCountry country1 = clsCountry.find(countryID);
            country1.countryName = "Türkiye";


            if (country1.save())
            {
                Console.WriteLine("{0} updated successfully", country1.countryName);

            }
            else
            {
                Console.WriteLine("{0} could not be updated", country1.countryName);
            }

        }


        static void deleteCountry(int countryID)
        {
            if (!clsCountry.isCountryExistByID(countryID))
            {
                Console.WriteLine("Country with ID [{0}]  could not found", countryID);
            }
            if (clsCountry.deleteCountry(countryID))     
            {
                Console.WriteLine("Country with ID [{0}] deleted successfully", countryID);

            }
            else
            {
                Console.WriteLine("Country with ID [{0}] could not be deleted successfully", countryID);
            }
        }


        static void listCountries()
        {
            DataTable dt = new DataTable();

            dt = clsCountry.getAllCountries();
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(" -- {0}, {1} ", row["countryID"], row["CountryName"]);
            }
        }
        static void Main(string[] args)
        {

            deleteContact(4);
            //getAllContacts();

        }
    }
}


