﻿using System;
using System.Threading.Tasks;

namespace EBZ.Mobile.ServicesInterface
{
    public interface INavigationService
    {
        string CurrentPageKey { get; }

        void Configure(string pageKey, Type pageType);
        Task GoBack();
        Task ClearModalStack();
        void ClearBackStack();
        void ClearBackStack4Login();
        Task GoToRoot();
        Task NavigateModalAsync(string pageKey, bool animated = true);
        Task NavigateModalAsync(string pageKey, object parameter, bool animated = true);
        Task NavigateAsync(string pageKey, bool animated = true);
        Task NavigateAsync(string pageKey, object parameter, bool animated = true);
    }
}
