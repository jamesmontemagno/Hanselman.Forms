using System;
using Xamarin.Forms;

namespace Hanselman.Shared
{
	public class TwitterView : BaseView
	{
		private TwitterViewModel ViewModel
		{
			get { return BindingContext as TwitterViewModel; }
		}
		public TwitterView ()
		{
			BindingContext = new TwitterViewModel ();

			var refresh = new ToolbarItem {
				Command = ViewModel.LoadTweetsCommand,
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

			listView.ItemsSource = ViewModel.Tweets;

			var cell = new DataTemplate(typeof(ImageCell));
      cell.SetBinding(ImageCell.TextProperty, "Text");
      cell.SetBinding(ImageCell.DetailProperty, "Date");
      cell.SetBinding(ImageCell.ImageSourceProperty, "Image");
			listView.ItemTemplate = cell;

			listView.ItemTapped +=  (sender, args) => {
				if(listView.SelectedItem == null)
					return;
				var tweet = listView.SelectedItem as Tweet;
				this.Navigation.PushAsync(new WebsiteView("http://m.twitter.com/shanselman/status/"+ tweet.StatusID, tweet.Date));
				listView.SelectedItem = null;
			};

			stack.Children.Add (listView);

			Content = stack;
		}
			


		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			if (ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.Tweets.Count > 0)
				return;

			ViewModel.LoadTweetsCommand.Execute (null);

		}
	}
}

