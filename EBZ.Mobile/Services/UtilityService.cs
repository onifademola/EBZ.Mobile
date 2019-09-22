using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBZ.Mobile.Services
{
    public class UtilityService
    {
        SettingsService _settingsService = new SettingsService();
        string[] roles = null;
        StorageService _storageService = new StorageService();

        public bool IsTokenExpired()
        {
            var tokenDate = _settingsService.ValidToSetting;
            if (tokenDate != null)
            {
                try
                {
                    //DateTime tempDate = DateTime.Today.AddMonths(9);
                    DateTime tokenEpiryDate = Convert.ToDateTime(tokenDate);
                    if (DateTime.Now < tokenEpiryDate)
                        return false;
                    return true;
                }
                catch (Exception)
                {
                    return true;
                }
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

        public bool UserIsInRole(string roleName)
        {            
            Task.Run(async () => { roles = await _storageService.GetFromCache<string[]>("userRoles"); });

            if (roles != null)
            {
                List<string> lst = roles.OfType<string>().ToList();
                var check = lst.Contains(roleName);
                if (check == true)
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}
