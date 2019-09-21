using Akavache;
using EBZ.Mobile.ServicesInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EBZ.Mobile.Services
{
    public class NavigationService : INavigationService
    {
        private readonly object _sync = new object();
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
        private readonly Stack<NavigationPage> _navigationPageStack =
            new Stack<NavigationPage>();
        //public Dictionary<string, Type> savedPagesKey;
        
        private NavigationPage CurrentNavigationPage => _navigationPageStack.Peek();
        
        public void Configure(string pageKey, Type pageType)
        {
            lock (_sync)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    _pagesByKey[pageKey] = pageType;
                }
                else
                {
                    _pagesByKey.Add(pageKey, pageType);
                }
            }
            //savedPagesKey = _pagesByKey;
            Application.Current.Properties["_savedPagesKey"] = _pagesByKey;
           // BlobCache.InMemory.InsertAllObjects("PagesByKey", _pagesByKey);
        }

        public Page SetRootPage(string rootPageKey)
        {
            var rootPage = GetPage(rootPageKey);
            _navigationPageStack.Clear();
            var mainPage = new NavigationPage(rootPage);
            _navigationPageStack.Push(mainPage);
            return mainPage;
        }

        public string CurrentPageKey
        {
            get
            {
                lock (_sync)
                {
                    if (CurrentNavigationPage?.CurrentPage == null)
                    {
                        return null;
                    }

                    var pageType = CurrentNavigationPage.CurrentPage.GetType();

                    return _pagesByKey.ContainsValue(pageType)
                        ? _pagesByKey.First(p => p.Value == pageType).Key
                        : null;
                }
            }
        }

        public async Task GoBack()
        {
            var navigationStack = CurrentNavigationPage.Navigation;
            if (navigationStack.NavigationStack.Count > 1)
            {
                await CurrentNavigationPage.PopAsync();
                return;
            }

            if (_navigationPageStack.Count > 1)
            {
                _navigationPageStack.Pop();
                await CurrentNavigationPage.Navigation.PopModalAsync();
                return;
            }

            await CurrentNavigationPage.PopAsync();
        }

        public async Task ClearModalStack()
        {
            var navigationStack = CurrentNavigationPage.Navigation;
            if (navigationStack.ModalStack.Count > 1)
            {
                var existingPages = navigationStack.ModalStack.ToList();
                foreach (var page in existingPages)
                {
                    await navigationStack.PopModalAsync();
                }            
            }
        }

        public void ClearBackStack()
        {
            var navigationStack = CurrentNavigationPage.Navigation;
            if (navigationStack.NavigationStack.Count > 1)
            {
                var existingPages = navigationStack.NavigationStack.ToList();
                foreach (var page in existingPages)
                {
                    navigationStack.RemovePage(page);
                }
                //var nav = App.ViewNavigationService;
                //var mainPage = ((NavigationService)nav).SetRootPage("MainPage");
                //App.Current.MainPage = mainPage;
            }
        }

        public void ClearBackStack4Login()
        {
            var navigationStack = CurrentNavigationPage.Navigation;
            Page currentPage = navigationStack.NavigationStack.LastOrDefault();
            if (navigationStack.NavigationStack.Count > 1)
            {
                var existingPages = navigationStack.NavigationStack.ToList();
                foreach (var page in existingPages)
                {
                    if(page != currentPage)
                    {
                        navigationStack.RemovePage(page);
                    }
                }
            }
            //var actionPage = App.Current.MainPage;
            //if (actionPage.Navigation != null)
            //    actionPage = actionPage.Navigation.NavigationStack.Last();
        }

        public async Task GoToRoot()
        {
            var navigationStack = CurrentNavigationPage.Navigation;
            await navigationStack.PopToRootAsync();
        }

        public async Task NavigateModalAsync(string pageKey, bool animated = true)
        {
            await NavigateModalAsync(pageKey, null, animated);
        }

        public async Task NavigateModalAsync(string pageKey, object parameter, bool animated = true)
        {
            var page = GetPage(pageKey, parameter);
            NavigationPage.SetHasNavigationBar(page, false);
            var modalNavigationPage = new NavigationPage(page);
            await CurrentNavigationPage.Navigation.PushModalAsync(modalNavigationPage, animated);
            _navigationPageStack.Push(modalNavigationPage);
        }

        public async Task NavigateAsync(string pageKey, bool animated = true)
        {
            await NavigateAsync(pageKey, null, animated);
        }

        public async Task NavigateAsync(string pageKey, object parameter, bool animated = true)
        {
            var page = GetPage(pageKey, parameter);
            await CurrentNavigationPage.Navigation.PushAsync(page, animated);
        }

        private Page GetPage(string pageKey, object parameter = null)
        {
            if (!Application.Current.Properties.ContainsKey("_savedPagesKey"))
            {
                throw new InvalidOperationException(
                       "No registered views found");
            }

            Dictionary<string, Type> savedPagesKey = (Dictionary<string, Type>)Application.Current.Properties["_savedPagesKey"];

            lock (_sync)
            {
                if (!savedPagesKey.ContainsKey(pageKey))
                {
                    throw new ArgumentException(
                        $"No such page: {pageKey}. Did you forget to call NavigationService.Configure?");
                }

                var type = savedPagesKey[pageKey];
                ConstructorInfo constructor;
                object[] parameters;

                if (parameter == null)
                {
                    constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .FirstOrDefault(c => !c.GetParameters().Any());

                    parameters = new object[]
                    {
                    };
                }
                else
                {
                    constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .FirstOrDefault(
                            c =>
                            {
                                var p = c.GetParameters();
                                return p.Length == 1
                                       && p[0].ParameterType == parameter.GetType();
                            });

                    parameters = new[]
                    {
                    parameter
                };
                }

                if (constructor == null)
                {
                    throw new InvalidOperationException(
                        "No suitable constructor found for page " + pageKey);
                }

                var page = constructor.Invoke(parameters) as Page;
                return page;
            }
        }
    }
}
