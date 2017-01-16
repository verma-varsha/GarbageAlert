using System;
using System.Collections.Generic;
using System.Text;

namespace cleanIndia
{
    class RegisterData
    {
        public RegisterData(string Name,string Email,string PhoneNumber,string Password)
        {
            name = Name;
            email = Email;
            password = Password;
            phoneNumber = PhoneNumber;
        }
        public RegisterData()
        {

        }
       public string name { get; set; }
       public string email { get; set; }
       public string password { get; set; }
       public string phoneNumber { get; set; }
    }
}
