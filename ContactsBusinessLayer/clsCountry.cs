using ContactsDataAccessLayer;
using System.Data;


namespace ContactsBusinessLayer
{
    public class clsCountry
    {
       
            public enum enMode { eAddNew = 0, eUpdate = 1 };

            public int ID { set; get; }
            public string countryName { set; get; }

            public string Code { set; get; }
            public string phoneCode { set; get; }

            public enMode modeOfCountryObject = enMode.eAddNew;



            public clsCountry()
            {
                this.ID = -1;
                this.countryName = "";
                this.Code = "";
                this.phoneCode = "";
                this.modeOfCountryObject = enMode.eAddNew;

            }

            private clsCountry(int ID, string countryName,string code,string phoneCode)
            {
                this.ID = ID;
                this.countryName = countryName;
               this.phoneCode = phoneCode;
                this.Code = code;

                this.modeOfCountryObject = enMode.eUpdate;

            }


            public static clsCountry find(int countryID)
            {

                string countryName = "",code="",phoneCode="";

                if (clsCountryData.findCountryByID(ref countryID, ref countryName,ref code,ref phoneCode))
                {
                    return new clsCountry(countryID, countryName,code,phoneCode);
                }
                else
                {
                    return null;

                }
            }


            public static clsCountry find(string countryName)
            {

                int ID = 0;
              string code = "", phoneCode = "";

                if (clsCountryData.findCountryByName(ref ID, ref countryName,ref code,ref phoneCode))
                {
                    return new clsCountry(ID, countryName, code, phoneCode);
                }
                else
                {
                    return null;

                }
            }



            public static bool isCountryExistByID(int ID)
            {
                return (clsCountryData.isCountryExistByID(ID));
            }
            public static bool isCountryExistByName(string countryName)
            {
                return (clsCountryData.isCountryExistByName(countryName));
            }

        
        private  bool _addNewCountry()
        {
            this.ID = clsCountryData.addNewCountry(this.countryName,this.Code,this.phoneCode);

            return (this.ID != -1);
        }

        private bool _updateCountry()
        {
            return clsCountryData.updateCountry(this.ID,this.countryName,this.Code,this.phoneCode);

        }

        public static bool deleteCountry(int countryID)
        {
            return clsContactDataAccess.deleteContact(countryID);
        }


        public static DataTable getAllCountries()
        {
            return clsCountryData.getAllCountries();
        }
       public bool save()
        {
            switch (modeOfCountryObject)
            {
                case enMode.eAddNew:
                    if (_addNewCountry())
                    {
                        this.modeOfCountryObject = enMode.eUpdate;
                        return true;
                    }
                    return false;
                case enMode.eUpdate:
                    return _updateCountry();
                    
                default:
                    return false;
               
            }
        }
    }
}
