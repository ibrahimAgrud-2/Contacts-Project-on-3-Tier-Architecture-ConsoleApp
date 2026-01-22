using System;
using ContactsBusinessLayer;
namespace ContactsConsoleAPP___PresentationLayer
{
    internal class Program
    {

        //[TR] Kullanıcıdan veriyi okuyup contact objesi oluşturuyoruz. 
        static void addNewContact()
        {
            clsContact contact = new clsContact();
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

            Console.Write("Enter countryID: ");
            int countryID = 0;
            while (!int.TryParse(Console.ReadLine(), out countryID))
            {
                Console.Write("CountryID must be integer: ");
            }
            contact.countryID = countryID;

            if (contact.save())
            {
                Console.WriteLine("Contact Added Successfully with id=" + contact.ID);
            }
            else
            {
                Console.WriteLine("Adding failed");

            }
        }


        static void Main(string[] args)
        {
       


        }
    }
}


