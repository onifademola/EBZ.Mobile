using System;

namespace EBZ.Mobile.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User()
        {

        }

        public User(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        //check that user information is not empty
        public bool CheckInformation()
        {
            if (Username != String.Empty && Password != String.Empty)
                return true;
            else
                return false;
        }
    }
}
