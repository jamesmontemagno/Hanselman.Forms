using System;
using Xamarin.Forms;

namespace Hanselman.Shared
{
	public class PodcastView : BaseView
	{
        private PodcastFeedViewModel ViewModel
		{
            get { return BindingContext as PodcastFeedViewModel; }
		}
        public PodcastView()
		{
            BindingContext = new PodcastFeedViewModel();

			var refresh = new ToolbarItem {
				Command = ViewModel.LoadItemsCommand,
				Icon = "refresh.png",
				Name = "refresh",
				Priority = 0
			};

			ToolbarItems.Add (refresh);

			var stack = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(0, 8, 0, 8)
			};

			var activity = new ActivityIndicator {
				Color = Helpers.Color.DarkBlue.ToFormsColor(),
				IsEnabled = true
			};
			activity.SetBinding (ActivityIndicator.IsVisibleProperty, "IsBusy");
			activity.SetBinding (ActivityIndicator.IsRunningProperty, "IsBusy");

			stack.Children.Add (activity);

			var listView = new ListView ();

			listView.ItemsSource = ViewModel.FeedItems;

			var cell = new DataTemplate(typeof(ListTextCell));

			cell.SetBinding (TextCell.TextProperty, "Title");
			cell.SetBinding (TextCell.DetailProperty, "PublishDate");

			listView.ItemTapped +=  (sender, args) => {
				if(listView.SelectedItem == null)
					return;
				this.Navigation.PushAsync(new PodcastDetailsView(listView.SelectedItem as PodcastFeedItem));
				listView.SelectedItem = null;
			};

			listView.ItemTemplate = cell;

			stack.Children.Add (listView);

			Content = stack;
		}
			

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			if (ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.FeedItems.Count > 0)
				return;

			ViewModel.LoadItemsCommand.Execute (null);

		}
	}
}

