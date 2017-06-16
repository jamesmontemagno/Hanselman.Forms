using System;
using Foundation;
using Hanselman.Portable;

namespace WatchExtension
{
	public partial class TwitterRow : NSObject {

		public TwitterRow ()
		{
		}

		public void Set (Tweet tweet) 
		{
			Console.WriteLine ("set: title=" + tweet.Text);

			// I don't even...
			// http://stackoverflow.com/questions/28031832/how-can-i-reload-the-data-in-a-watchkit-tableview
			TweetLabel.SetText (@""); // this fixes the issue I was having :-\
			TweetLabel.SetText (tweet.Text);

		}
	}
}

