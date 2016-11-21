using Hanselman.Portable.Models;
using Hanselman.Portable.ViewModels;
using Xamarin.Forms;

namespace Hanselman.Portable.Views
{
    public partial class Channel9VideosPage : ContentPage
    {
        private Channel9VideosViewModel ViewModel
        {
            get { return BindingContext as Channel9VideosViewModel; }
        }
        

        public Channel9VideosPage()
        {
            InitializeComponent();
            BindingContext = new Channel9VideosViewModel();

            listView.ItemTapped += (sender, args) =>
            {
                if (listView.SelectedItem == null)
                    return;
                this.Navigation.PushAsync(new Channel9VideoPlaybackPage
                  (listView.SelectedItem as VideoFeedItem));
                listView.SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.FeedItems.Count > 0)
                return;

            ViewModel.LoadItemsCommand.Execute(null);
        }
    }
}
