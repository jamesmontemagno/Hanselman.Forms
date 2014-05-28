using System;
using Xamarin.Forms;

namespace Hanselman.Shared
{
	public class AboutView : BaseView
	{
		public AboutView ()
		{
			var stack = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Spacing = 10
			};

			var image = new Image ();
			image.Source = ImageSource.FromFile ("scott.png");
			image.Aspect = Aspect.AspectFill;

			stack.Children.Add (image);

			var stack2 = new StackLayout {
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Spacing = 10,
				Padding = 10
			};

			var about = new Label {
				Font = Font.SystemFontOfSize (NamedSize.Medium),
				Text = "My name is Scott Hanselman. I'm a programmer, teacher, and speaker. I work out of my home office in Portland, Oregon for the Web Platform Team at Microsoft, but this blog, its content and opinions are my own. I blog about technology, culture, gadgets, diversity, code, the web, where we're going and where we've been. I'm excited about community, social equity, media, entrepreneurship and above all, the open web.",
				LineBreakMode = LineBreakMode.WordWrap
			};


			stack2.Children.Add(about);

			var findMe = new Label {
				Font = Font.BoldSystemFontOfSize (NamedSize.Medium),
				Text = "Find Me", 
				LineBreakMode = LineBreakMode.NoWrap
			};

			stack2.Children.Add(findMe);



			var twitter = new Image {
				Source = new FileImageSource { File = "twitter.png"}
			};
			twitter.GestureRecognizers.Add (new TapGestureRecognizer ((view, args) =>{
				this.Navigation.PushAsync(new WebsiteView("http://m.twitter.com/shanselman", "@shanselman"));
			}));

			var facebook = new Image {
				Source = new FileImageSource { File = "facebook.png"}
			};
			facebook.GestureRecognizers.Add (new TapGestureRecognizer ((view, args) =>{
				this.Navigation.PushAsync(new WebsiteView("http://facebook.com/scott.hanselman", "Scott @Facebook"));
			}));

			var instagram = new Image {
				Source = new FileImageSource { File = "instagram.png"}
			};
			instagram.GestureRecognizers.Add (new TapGestureRecognizer ((view, args) =>{
				this.Navigation.PushAsync(new WebsiteView("http://instagram.com/shanselman", "Scott @Instagram"));
			}));

			var google = new Image {
				Source = new FileImageSource { File = "googleplus.png"}
			};
			google.GestureRecognizers.Add (new TapGestureRecognizer ((view, args) =>{
				this.Navigation.PushAsync(new WebsiteView("http://plus.google.com/108573066018819777334?rel=me", "Hanselman+"));
			}));

			var socialStack = new StackLayout {
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Orientation = StackOrientation.Horizontal,
				Spacing = 20,
				Children = {twitter, facebook, instagram, google}
			};

			stack2.Children.Add (socialStack);

			stack.Children.Add (new ScrollView{VerticalOptions = LayoutOptions.FillAndExpand, Content = stack2 });

			Content = stack;
		}
	}
}

