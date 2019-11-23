using System.Linq;
using Hanselman.Helpers;
using Hanselman.Models;
using Hanselman.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


// mattleibow cheered 33 on November 22nd 2019

namespace Hanselman.Views
{ 
    [Preserve(AllMembers =true)]
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

        void SetSpan()
        {
            var gil = (GridItemsLayout)CollectionViewVideos.ItemsLayout;
            gil.Span = (int)Application.Current.Resources["VideoSpan"];
        }



        void App_SpanChanged(object sender, System.EventArgs e) =>
            SetSpan();

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.SpanChanged -= App_SpanChanged;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (VM.Episodes.Count == 0)
                VM.LoadEpisodesCommand.Execute(null);


            SetSpan();
            App.SpanChanged += App_SpanChanged;
        }



        async void CollectionViewVideos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is VideoFeedItem item && item != null)
            {
                await Navigation.PushAsync(new VideoDetailsPage(item));
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}
