using System;
using System.Collections.Generic;
using System.Text;

namespace EBZ.Mobile.ServicesInterface
{
    public interface ISettingsService
    {
        void AddItem(string key, string value);
        string GetItem(string key);
        string UserNameSetting { get; set; }
        string UserIdSetting { get; set; }
        string TokenSetting { get; set; }
        string ValidToSetting { get; set; }
        string RolesSetting { get; set; }

    }
}
