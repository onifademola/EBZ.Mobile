using System;

namespace EBZ.Mobile.Services
{
    public class UtilityService
    {
        SettingsService _settingsService = new SettingsService();
        public bool IsTokenExpired()
        {
            var tokenDate = _settingsService.ValidToSetting;
            if (tokenDate.Length > 0)
            {
                DateTime tokenEpiryDate = Convert.ToDateTime("01/09/2018");
                if (DateTime.Now > tokenEpiryDate)
                    return true;
            }
            return false;
        }

        public bool IsAuthenticated()
        {
            var check = _settingsService.UserNameSetting;
            if (check.Equals(string.Empty))
                return false;
            return true;
        }
    }
}
