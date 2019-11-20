using Hanselman.Models;
using Hanselman.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hanselman.Views
{
    public partial class VideoSeriesPage : ContentPage
    {
        VideoSeriesViewModel VM => (VideoSeriesViewModel)BindingContext;
        public VideoSeriesPage(VideoSeries series)
        {
            InitializeComponent();
            BindingContext = new VideoSeriesViewModel(series);
        }
        public VideoSeriesPage()
        {
            InitializeComponent();
            BindingContext = new VideoSeriesViewModel();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (VM.Episodes.Count == 0)
                VM.LoadEpisodesCommand.Execute(null);
        }

        private void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is VideoFeedItem item && item != null)
            {
                await Navigation.PushAsync(new VideoDetailsPage(item));
                ((ListView)sender).SelectedItem = null;
            }
        }
    }
}
