using WebConferenceClient.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;
using System.ServiceModel;
using WebConferenceClient.ServiceReference;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using WebConferenceClient.Models;
using Windows.UI;

namespace WebConferenceClient.Views
{
    public sealed partial class ChannelPage : Page, IServiceCallback
    {
        private ChannelPageViewModel vm;

        public ChannelPage()
        {
            this.InitializeComponent();

            vm = DataContext as ChannelPageViewModel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!vm.IsConnected)
            {
                var dialog = new AddressDialog();
                await dialog.ShowAsync();
                vm.Proxy = new ServiceClientBase(new InstanceContext(this), new NetTcpBinding(SecurityMode.None), new EndpointAddress(dialog.Address.Trim()));
                await vm.Proxy.OpenAsync();
                vm.LocalClient = new Client()
                {
                    Name = dialog.Username.Trim()
                };


                await vm.Proxy.StartSessionAsync(vm.LocalClient);
                vm.IsConnected = true;
            }
        }

        private async void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            var message = new Message()
            {
                Content = this.InputBox.Text
            };

            this.InputBox.Text = String.Empty;

            await vm.Proxy.SendMessageAsync(message);
        }

        public async void ConnectionClient(Client client)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var time = new TextBlock();
                time.Margin = new Thickness(0, 0, 4, 4);
                time.Foreground = new SolidColorBrush(Colors.Blue);
                time.Text = String.Format("[{0}]", DateTime.Now.ToString("HH:mm"));

                var username = new TextBlock();
                username.Width = 0;

                var content = new TextBlock();
                content.Width = double.NaN;
                content.Margin = new Thickness(4, 0, 0, 4);
                content.Text = String.Format("{0} has joined.", client.Name);
                content.IsTextSelectionEnabled = true;

                vm.Items.Add(new OutputItem()
                {
                    Time = time,
                    Username = username,
                    Content = content
                });
            });
        }

        public async void DisconnectionClient(Client client)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var time = new TextBlock();
                time.Margin = new Thickness(0, 0, 4, 4);
                time.Foreground = new SolidColorBrush(Colors.Blue);
                time.Text = String.Format("[{0}]", DateTime.Now.ToString("HH:mm"));

                var username = new TextBlock();
                username.Width = 0;

                var content = new TextBlock();
                content.Width = double.NaN;
                content.Margin = new Thickness(4, 0, 0, 4);
                content.Text = String.Format("{0} has leaved.", client.Name);
                content.IsTextSelectionEnabled = true;

                vm.Items.Add(new OutputItem()
                {
                    Time = time,
                    Username = username,
                    Content = content
                });
            });
        }

        public async void ReceiveMessage(Client client, Message message)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var time = new TextBlock();
                time.Margin = new Thickness(0, 0, 4, 4);
                time.Foreground = new SolidColorBrush(Colors.Blue);
                time.Text = String.Format("[{0}]", DateTime.Now.ToString("HH:mm"));

                var username = new TextBlock();
                username.Width = double.NaN;
                username.Margin = new Thickness(4, 0, 4, 4);
                username.Foreground = new SolidColorBrush(Colors.Blue);
                username.Text = String.Format("{0}", client.Name);

                var content = new TextBlock();
                content.Width = double.NaN;
                content.Margin = new Thickness(4, 0, 0, 4);
                content.Text = message.Content;
                content.TextWrapping = TextWrapping.Wrap;
                content.IsTextSelectionEnabled = true;

                vm.Items.Add(new OutputItem()
                {
                    Time = time,
                    Username = username,
                    Content = content
                });
            });
        }
    }
}
