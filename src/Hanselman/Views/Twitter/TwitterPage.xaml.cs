using Hanselman.Interfaces;
using Hanselman.Models;
using Hanselman.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hanselman.Views
{
    public partial class TwitterPage : ContentPage, IPageHelpers
    {
        TwitterViewModel? vm;
        TwitterViewModel? ViewModel => vm ??= BindingContext as TwitterViewModel;

        public TwitterPage()
        {
            InitializeComponent();

            BindingContext = new TwitterViewModel();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (DeviceInfo.Platform != DevicePlatform.UWP)
                OnPageVisible();
        }

        public void OnPageVisible()
        {
            if (ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.Tweets.Count > 0)
                return;

            ViewModel.LoadCommand.ExecuteAsync().ContinueWith(t => { });
        }
    }
}
