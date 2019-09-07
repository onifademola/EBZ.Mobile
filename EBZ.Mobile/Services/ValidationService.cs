using System.Text.RegularExpressions;

namespace EBZ.Mobile.Services
{
    public class ValidationService
    {
        private const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        private const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";


        public bool IsEmailValid(string email)
        {
            bool IsValid = (Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase)); //, TimeSpan.FromMilliseconds(250)));
            return IsValid;
        }

        public bool IsPasswordValid(string password)
        {
            bool IsValid = Regex.IsMatch(password, passwordRegex, RegexOptions.IgnoreCase);
            return IsValid;
        }
    }
}
