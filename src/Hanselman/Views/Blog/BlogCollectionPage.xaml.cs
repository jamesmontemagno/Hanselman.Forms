using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hanselman.Interfaces;
using Hanselman.Models;
using Hanselman.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hanselman.Views
{
    public partial class BlogCollectionPage : ContentPage, IPageHelpers
    {
        DisplayOrientation orientation;
        BlogFeedViewModel viewModel;
        BlogFeedViewModel ViewModel => viewModel ?? (viewModel = BindingContext as BlogFeedViewModel);
        public BlogCollectionPage()
        {
            InitializeComponent();

            orientation = DeviceDisplay.MainDisplayInfo.Orientation;
            if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            {
                DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
                SetSize();
            }

            ViewModel.FeedItems.CollectionChanged += FeedItems_CollectionChanged;
        }

        private void FeedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CollectionViewBlog.ItemsSource = ViewModel.FeedItems;
        }

        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
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
            if (DeviceInfo.Platform != DevicePlatform.UWP)
                OnPageVisible();
        }

        public void OnPageVisible()
        {
            if (ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.FeedItems.Count > 0)
                return;

            ViewModel.LoadItemsCommand.Execute(null);
        }
    }
}