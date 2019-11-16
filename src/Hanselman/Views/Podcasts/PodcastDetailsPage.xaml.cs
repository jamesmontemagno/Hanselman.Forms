using System.Threading.Tasks;
using Hanselman.Models;
using Hanselman.ViewModels;
using Xamarin.Forms;

// chrisntr cheered 200 on March 22nd 2019


namespace Hanselman.Views
{
    public partial class PodcastDetailsPage : ContentPage
    {
        PodcastDetailsViewModel? vm; 
        PodcastDetailsViewModel? VM => vm ??= BindingContext as PodcastDetailsViewModel;
        public PodcastDetailsPage()
        {
            InitializeComponent();
        }

        public PodcastDetailsPage(Podcast podcast) : this()
        {
            BindingContext = new PodcastDetailsViewModel(podcast);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(VM?.Episodes.Count == 0)
                VM.LoadEpisodesCommand.Execute(null);
        }

        void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = sender as ListView;
            if (!(listView?.SelectedItem is PodcastEpisode episode))
                return;

            await Navigation.PushModalAsync(new PodcastEpisodePage(episode, VM?.Podcast?.Title ?? string.Empty));


            listView.SelectedItem = null;
        }

        void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

            if(e.ItemIndex == 0 && StackLayoutInfo.IsVisible)
            {
                StackLayoutInfo.FadeTo(0).ContinueWith((t) =>
                {
                    StackLayoutInfo.IsVisible = false;
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            if (VM == null || VM.IsBusy || VM.Episodes.Count == 0)
                return;

            //hit bottom!
            if (e.ItemIndex == VM.Episodes.Count - 1)
            {
                VM.LoadMoreEpisodes();
            }
        }

        void ListView_ItemDisappearing(object sender, ItemVisibilityEventArgs e)
        {
            if (e.ItemIndex != 0 || StackLayoutInfo.IsVisible)
                return;

            StackLayoutInfo.FadeTo(1);
            StackLayoutInfo.IsVisible = true;
        }
    }
}