using System;

namespace EBZ.Mobile.Services
{
    public class UtilityService
    {
        SettingsService _settingsService = new SettingsService();
        public bool IsTokenExpired()
        {
            var tokenDate = _settingsService.ValidToSetting;
            if (tokenDate != null)
            {
                //DateTime tempDate = DateTime.Today.AddMonths(9);
                DateTime tokenEpiryDate = Convert.ToDateTime(tokenDate);
                if (DateTime.Now < tokenEpiryDate)
                    return false;
                return true;
            }
            return true;
        }

        public bool IsAuthenticated()
        {
            var check = _settingsService.UserNameSetting;
            if (check == null)
                return false;
            return true;
        }
    }
}
