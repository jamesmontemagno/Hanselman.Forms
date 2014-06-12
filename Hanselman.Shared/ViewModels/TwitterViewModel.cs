using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using LinqToTwitter;
using System.Threading.Tasks;
using System.Linq;

namespace Hanselman.Shared
{
	public class TwitterViewModel : BaseViewModel
	{

		public ObservableCollection<Tweet> Tweets{ get; set; }

		public TwitterViewModel ()
		{
			Title = "Twitter";
			Icon = "slideout.png";
			Tweets = new ObservableCollection<Tweet> ();

		}

		private Command loadTweetsCommand;

		public Command LoadTweetsCommand {
			get { return loadTweetsCommand ?? (loadTweetsCommand = new Command (ExecuteLoadTweetsCommand)); }
		}

		private async void ExecuteLoadTweetsCommand ()
		{
			if (IsBusy)
				return;

			IsBusy = true;
			try {

				Tweets.Clear ();
				var auth = new ApplicationOnlyAuthorizer () {
					CredentialStore = new InMemoryCredentialStore {
						ConsumerKey = "ZTmEODUCChOhLXO4lnUCEbH2I",
						ConsumerSecret = "Y8z2Wouc5ckFb1a0wjUDT9KAI6DUat5tFNdmIkPLl8T4Nyaa2J",
					},
				};
				await auth.AuthorizeAsync ();

				var twitterContext = new TwitterContext (auth);

#if !WINDOWS_PHONE
        IQueryable<LinqToTwitter.Status> queryResponse =
					(from tweet in twitterContext.Status
					where tweet.Type == StatusType.User &&
					  tweet.ScreenName == "shanselman" &&
					  tweet.Count == 100 &&
					  tweet.IncludeRetweets == true &&
					  tweet.ExcludeReplies == true
					select tweet);
					
				var queryTweets = queryResponse.ToList ();
				var tweets =
					(from tweet in queryTweets
					select new Tweet {
						StatusID = tweet.StatusID,
						ScreenName = tweet.User.ScreenNameResponse,
						Text = tweet.Text, 
						CurrentUserRetweet = tweet.CurrentUserRetweet, 
						CreatedAt = tweet.CreatedAt
					}).ToList ();
#else

				var tweets =
					await (from tweet in twitterContext.Status
					where tweet.Type == StatusType.User &&
					  tweet.ScreenName == "shanselman" &&
					  tweet.Count == 100 &&
					  tweet.IncludeRetweets == true &&
					  tweet.ExcludeReplies == true
                 select new Tweet
                 {
                   StatusID = tweet.StatusID,
                   ScreenName = tweet.User.ScreenNameResponse,
                   Text = tweet.Text,
                   CurrentUserRetweet = tweet.CurrentUserRetweet,
                   CreatedAt = tweet.CreatedAt
                 }).ToListAsync();

#endif

        foreach (var tweet in tweets) {
					Tweets.Add (tweet);
				}
			} catch (Exception ex) {
				var page = new ContentPage();
				page.DisplayAlert ("Error", "Unable to load twitter.", "OK", null);
			}

			IsBusy = false;
		}
	}
}

