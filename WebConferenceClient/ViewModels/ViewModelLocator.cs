using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace WebConferenceClient.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<HomePageViewModel>();
            SimpleIoc.Default.Register<ChannelPageViewModel>();
        }

        public HomePageViewModel HomePage
        {
            get { return ServiceLocator.Current.GetInstance<HomePageViewModel>(); }
        }

        public ChannelPageViewModel ChannelPage
        {
            get { return ServiceLocator.Current.GetInstance<ChannelPageViewModel>(); }
        }
    }
}
