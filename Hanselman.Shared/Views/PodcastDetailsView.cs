using System;
using Xamarin.Forms;

namespace Hanselman.Shared
{
	public class PodcastDetailsView : BaseView
	{
        public PodcastDetailsView(PodcastFeedItem item)
		{
            BindingContext = item;

            var stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 10
            };

            var webView = new WebView();
            webView.Source = new HtmlWebViewSource
            {
                Html = item.Description
            };



            stack.Children.Add(new ScrollView { VerticalOptions = LayoutOptions.FillAndExpand, Content = webView });

            var podcastImage = new Image
            {
                Source = new FileImageSource { File = "podcast.png" }
            };
            podcastImage.GestureRecognizers.Add(new TapGestureRecognizer((view, args) =>
            {
                this.Navigation.PushAsync(new WebsiteView(item.PodcastLink, "Podcast"));
            }));

            var podcastPlayStack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Horizontal,
                Spacing = 20,
                Children = { podcastImage }
            };

            stack.Children.Add(podcastPlayStack);

            Content = stack;  


		}
	}
}

