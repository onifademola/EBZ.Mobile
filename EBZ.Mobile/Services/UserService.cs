using System;

namespace EBZ.Mobile.Services
{
    public class UserService
    {
        SettingsService _settingsService = new SettingsService();

        public UserService()
        {

        }

        public bool IsAuthenticated()
        {
            var check = _settingsService.UserNameSetting;
            if (check.Equals(string.Empty))
                return false;
            return true;
        }

        public bool IsTokenExpired()
        {
            var tokenDate = _settingsService.ValidToSetting;
            if (!tokenDate.Equals(string.Empty))
            {
                DateTime tokenEpiryDate = Convert.ToDateTime(tokenDate);
                if (DateTime.Now < tokenEpiryDate)
                    return true;
            }
            return false;
        }

        public string LoggedInUser()
        {
            return _settingsService.UserNameSetting;
        }

        public string LoggedInUserToken()
        {
            return _settingsService.TokenSetting;
        }

        public string LoggedInUserRole()
        {
            return _settingsService.RolesSetting;
        }
    }
}
