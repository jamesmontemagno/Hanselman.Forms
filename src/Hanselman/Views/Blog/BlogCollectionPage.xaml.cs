using Hanselman.Interfaces;
using Hanselman.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

// ChrisNTR cheered 100 on October 18th 2019

namespace Hanselman.Views
{
    public partial class BlogCollectionPage : ContentPage, IPageHelpers
    {
        DisplayOrientation orientation;
        BlogFeedViewModel? viewModel;
        BlogFeedViewModel? ViewModel => viewModel ??= BindingContext as BlogFeedViewModel;
        public BlogCollectionPage()
        {
            InitializeComponent();

            BindingContext = new BlogFeedViewModel();

            orientation = DeviceDisplay.MainDisplayInfo.Orientation;
            if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            {
                DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
                SetSize();
            }
        }


        void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            if(orientation != e.DisplayInfo.Orientation)
            {
                orientation = e.DisplayInfo.Orientation;
                SetSize();
            }
        }

        void SetSize()
        {
            var gil = (GridItemsLayout)CollectionViewBlog.ItemsLayout;
            gil.Span = orientation == DisplayOrientation.Portrait ? 1 : 2;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OnPageVisible();
        }

        public void OnPageVisible()
        {
            if (ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.FeedItems.Count > 0)
            {
                return;
            }

            ViewModel.LoadCommand.Execute(true);
        }
    }
}