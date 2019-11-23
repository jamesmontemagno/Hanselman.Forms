using Hanselman.Interfaces;
using Hanselman.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

// ChrisNTR cheered 100 on October 18th 2019
// HuniePop donated $2.50 on November 22nd 2019
// mattleibow cheered 5 on November 22nd 2019
// KymPhillpotts cheered 69 on November 22nd 2019
// CommitttedBlock5 subscribed on November 22nd 2019
// ptDave20 gift sub to andre_abrantes on November 22nd 2019
// KymPhillpotts subscribed for the 11 month on November 22nd 2019

namespace Hanselman.Views
{
    public partial class BlogCollectionPage : ContentPage, IPageHelpers
    {
        BlogFeedViewModel? viewModel;
        BlogFeedViewModel? ViewModel => viewModel ??= BindingContext as BlogFeedViewModel;
        public BlogCollectionPage()
        {
            InitializeComponent();

            BindingContext = new BlogFeedViewModel();
        }

        void SetSpan()
        {
            var gil = (GridItemsLayout)CollectionViewBlog.ItemsLayout;
            gil.Span = (int)Application.Current.Resources["BlogSpan"];
        }



        private void App_SpanChanged(object sender, System.EventArgs e)
        {
            SetSpan();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.SpanChanged -= App_SpanChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            OnPageVisible();
            SetSpan();
            App.SpanChanged += App_SpanChanged;
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