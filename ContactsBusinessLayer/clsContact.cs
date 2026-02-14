using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsContact
    {
        public enum enMode { eAddNew = 0, eUpdate = 1 };

        public int ID { set; get; }
        public string firstName { set; get; }
        public string lastName { set; get; }
        public string email { set; get; }
        public string phone { set; get; }
        public string address { set; get; }
        public DateTime dateOfBirth { set; get; }
        public string imagePath { set; get; }
        public int countryID { set; get; }


        //Her object 2 farklı modu olur. Ya yeni üretilmiştir modu add ya da sitemde zaten vardır
        //ki bunuda find ile bulmuşuzdur modu update. Bunuda diğer propertylerden farkı yoktur.
        public enMode modeOfContactObject=enMode.eAddNew;



        //[TR] Sadece parametresiz const public. Ayrıca parametresiniz const ile oluşturacağımız objenin mode add;
        //çünkü sistemde henüz yok, eğer olmayan obje için save fonksiyonu çağırılırsa add yapılsın yani sisteme eklensin diye.
        public clsContact()
        {
           this.ID = 0;
           this.firstName = "";
           this.lastName = "";
           this.email = "";
           this.phone = "";
           this.address = "";
           this.dateOfBirth = DateTime.Now;
           this.imagePath = "";
           this.countryID = -1;
      
            this.modeOfContactObject = enMode.eAddNew;
            
        }



        //[TR]
        //Bu const'ı private yaptık ki dışardan erişilmesin. Çünkü bu constractır bir objenin ID dahil
        //tüm bilgilerini ister, dolaysıyla dışardan hiç bir şekilde ID'e direk erişim olmadığı için
        //bu const ile dışarda obje oluşturamayız. * Bu const ile sadece DB'de var olan contact'ı/objeyi sistemde
        //kullanmak istediğimiz zaman bu const ile oluştururuz.
        //Mesela bunu find fonksiyonunda kullandık. Çünkü bir contact DB de bulunursa sistemde
        //kullanılabilmesi için bu const gerekir.
        private clsContact(int ID,string firstName, string lastName, string email, string phone, string address, DateTime dateOfBirth, string imagePath,int countryID)
        {
            this.ID = ID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phone = phone;
            this.address = address;
            this.dateOfBirth = dateOfBirth;
            this.imagePath = imagePath; 
            this.countryID = countryID;

            //[TR]
            //Bu cost DB'de var olan bir objeyi sistemde oluşturmak için kullanıldığı için
            //modu add olamaz. update olmalı; çünkü zaten sistemde var.
            this.modeOfContactObject = enMode.eUpdate;
        }

        public static clsContact find(int ID)
        {
           
            string firstName="", lastName = "", email = "", phone = "", address = "", imagePath = "";
            DateTime dateOfBirth=DateTime.Now;
            int countryID=0;


            if (clsContactDataAccess.getContactInfoByID(ref ID, ref firstName, ref lastName, ref email, ref phone, ref address,  dateOfBirth, ref imagePath, ref countryID))
            {
                return new clsContact(ID,firstName,lastName,email,phone,address,dateOfBirth,imagePath,countryID);

            }
            else
                return null;
        }


        private bool _addNewContact()
        {
            //[TR]
            //ID otomatik olarak verildiği için database ID veremeyiz;çünkü ID identicle yani DB tradında ototmatik veriliyor.
            //Bize executeScaler'de kullanrak eklenen satırın ID'sini alıyoruz ve bunu ID'siz olan objecte ekliyoruz.
            //burada göderdiğimiz objeler akleme yapılmadan önce ID'si olmuyor biz kullanıcıdan ID istemiyoruz ID database atıyor. 
            //burda eğer this.ID= demeseydik ilgili objenin ID'sı DB'de olursa ama burada henüz gelmemiş olurdu bizde eğer direk o objeyi sistemde kullanmaya kalkarsak ID'den hata alırız.
            
            this.ID = clsContactDataAccess.addContactToDatabase(this.firstName, this.lastName, this.email, this.phone, this.address, this.dateOfBirth, this.countryID, this.imagePath);

            return (this.ID != -1);
        }

        private bool _updateContact()
        {
            return clsContactDataAccess.updateContactInfo(this.ID, this.firstName, this.lastName, this.email, this.phone, this.address, this.dateOfBirth, this.countryID, this.imagePath);

           
        }



        //[TR]
        //Delete fonksiyonu DB'den contact'ı silmek için var.
        //Bu yüzden gidipte önce contact find yapıp sonra delete yapmamızda gerek yok.
        //ID ile Direk delete yaparız dışardan. Ayrıca eğer o anki objeyi silem için
        //contact.delete dersen o obje DB'den silinir ama obje hala dolu olur yani
        //objede silinmiş contact veriler kalır buda tam manasıyla delete yapmış olmayzı
        //bu nedenle delete dışardan yapılmalı delete yapılacak obje sisteme yüklenmemeli direk DB'den silinmeli
        public static bool deleteContact(int ContactID)
        {
          
           return  clsContactDataAccess.deleteContact(ContactID);
            
        }

        public  bool save()
        {
            switch (this.modeOfContactObject)
            {
                case enMode.eAddNew:
                    if (_addNewContact())
                    {
                        //[TR]
                        //ekleme tamamlandığı için ve dolaysıyla bu contact sistemde olduğu için artık
                        //modu add yerine update yaparız. Save update için de kullanıldığı için
                        //eğer o anki objeyi update yapmak istediğindinde sisteme tekrar kayıt eder.

                        this.modeOfContactObject = enMode.eUpdate;
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                case enMode.eUpdate:
                  return  _updateContact();
                default:
                    return false;
               
            }

        }

        public static DataTable getAllContacts()
        {
            return clsContactDataAccess.getAllContacts();
            
        }
        public static bool isContactExist(int contactID)
        {
            return clsContactDataAccess.isContactExist(contactID);
        }


}
}
