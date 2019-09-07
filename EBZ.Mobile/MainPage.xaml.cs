using Syncfusion.XForms.TabView;
using Xamarin.Forms;

namespace EBZ.Mobile
{
    public partial class MainPage : ContentPage
    {
        private SfTabView tabView;
        public MainPage()
        {
            InitializeComponent();
            tabView = new SfTabView();
            Grid allContactsGrid = new Grid { BackgroundColor = Color.Red };
            Grid favoritesGrid = new Grid { BackgroundColor = Color.Green };
            Grid contactsGrid = new Grid { BackgroundColor = Color.Blue };
            var tabItems = new TabItemCollection
            {
                new SfTabItem()
                {
                    Title = "Calls",
                    Content = allContactsGrid
                },
                new SfTabItem()
                {
                    Title = "Favorites",
                    Content = favoritesGrid
                },
                new SfTabItem()
                {
                    Title = "Contacts",
                    Content = contactsGrid
                }
            };
            tabView.Items = tabItems;
            this.Content = tabView;
        }
    }
}
