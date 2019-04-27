using Hanselman.Models;
using Hanselman.ViewModels;
using Xamarin.Forms;

namespace Hanselman.Views
{
    public partial class PodcastPage : ContentPage
    {
        PodcastViewModel ViewModel => BindingContext as PodcastViewModel;


        public PodcastPage(MenuType item)
        {
            InitializeComponent();
            BindingContext = new PodcastViewModel(item);

            listView.ItemTapped += (sender, args) =>
            {
                if (listView.SelectedItem == null)
                    return;
                Navigation.PushAsync(new PodcastPlaybackPage
                  (listView.SelectedItem as FeedItem));
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
