using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hanselman.Interfaces;
using Xamarin.Forms;

namespace Hanselman.Views
{
    public partial class BlogPage : ContentPage, IPageHelpers
    {

        BlogFeedViewModel ViewModel => BindingContext as BlogFeedViewModel;


        public BlogPage()
        {
            InitializeComponent();
            BindingContext = new BlogFeedViewModel();

            listView.ItemTapped += (sender, args) =>
            {
                if (listView.SelectedItem == null)
                    return;
                Navigation.PushAsync(new BlogDetailsView(listView.SelectedItem as FeedItem));
                listView.SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void OnPageVisible()
        {
            if (ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.FeedItems.Count > 0)
                return;

            ViewModel.LoadItemsCommand.Execute(null);
        }
    }
}
