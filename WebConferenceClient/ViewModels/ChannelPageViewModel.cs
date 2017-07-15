using System.Collections.ObjectModel;
using WebConferenceClient.Models;
using WebConferenceClient.ServiceReference;

namespace WebConferenceClient.ViewModels
{
    public class ChannelPageViewModel : ViewModel
    {
        private bool _isConnected = false;
        public bool IsConnected { get => _isConnected; set => _isConnected = value; }

        private Client _localClient;
        public Client LocalClient { get => _localClient; set => _localClient = value; }

        private ServiceClientBase _proxy;
        public ServiceClientBase Proxy { get => _proxy; set => _proxy = value; }

        private ObservableCollection<OutputItem> _items = new ObservableCollection<OutputItem>();
        public ObservableCollection<OutputItem> Items { get => _items; set => _items = value; }
    }
}
