using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WebConferenceClient
{
    public sealed partial class MainPage : Page
    {
        private List<HamburgerMenuItem> MenuItemList = new List<HamburgerMenuItem>(new[]
        {
            new HamburgerMenuItem()
            {
                Icon = "ms-appx:///Assets/icons/home.png",
                Name = "Home",
                Page = typeof(Views.HomePage)
            },
            new HamburgerMenuItem()
            {
                Icon = "ms-appx:///Assets/icons/channel.png",
                Name = "Channel",
                Page = typeof(Views.ChannelPage)
            }
        });

        public MainPage()
        {
            this.InitializeComponent();

            HamburgerMenu.ItemsSource = MenuItemList;
        }

        private void HamburgerMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as HamburgerMenuItem;

            ContentFrame.Navigate(item.Page);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            /* Go to the Home page ... */
            var item = HamburgerMenu.Items[0] as HamburgerMenuItem;

            HamburgerMenu.SelectedItem = item;
            ContentFrame.Navigate(item.Page);
        }
    }
}
