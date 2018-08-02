using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
//using LinqToTwitter;
using System.Threading.Tasks;
using System.Linq;
using MvvmHelpers;
using System.Net.Http;
using System.Text;
using System.Json;
using System.Collections.Generic;
using QuickType;
using System.Globalization;

namespace Hanselman.Portable
{
    public class TwitterViewModel : BaseViewModel
    {

        public ObservableRangeCollection<Tweet> Tweets { get; set; }

        public TwitterViewModel()
        {
            Title = "Twitter";
            Icon = "slideout.png";
            Tweets = new ObservableRangeCollection<Tweet>();

        }

        private Command loadTweetsCommand;

        public Command LoadTweetsCommand
        {
            get
            {
                return loadTweetsCommand ??
                  (loadTweetsCommand = new Command(async () =>
                  {
                      await ExecuteLoadTweetsCommand();
                  }, () =>
                  {
                      return !IsBusy;
                  }));
            }
        }

        public async Task<string> GetAccessToken()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token ");
            var customerInfo = Convert.ToBase64String(new UTF8Encoding()
                                      .GetBytes("ZTmEODUCChOhLXO4lnUCEbH2I" + ":" + "Y8z2Wouc5ckFb1a0wjUDT9KAI6DUat5tFNdmIkPLl8T4Nyaa2J"));
            request.Headers.Add("Authorization", "Basic " + customerInfo);
            request.Content = new StringContent("grant_type=client_credentials",
                                                    Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonValue.Parse(json);

            return result["access_token"];
        }


        async Task<IEnumerable<TweetRaw>> GetTweets(string accessToken = null)
        {
            if (accessToken == null)
            {
                accessToken = await GetAccessToken();
            }

            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get,
                $"https://api.twitter.com/1.1/statuses/user_timeline.json?count=25&screen_name=shanselman&trim_user=0&exclude_replies=1");

            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline);
            string json = await responseUserTimeLine.Content.ReadAsStringAsync();

            return TweetRaw.FromJson(json);
        }

        public async Task ExecuteLoadTweetsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            LoadTweetsCommand.ChangeCanExecute();

            try
            {

               
                var tweetsRaw = await GetTweets();

                
                var tweets = tweetsRaw.Select(t => new Tweet
                {
                    StatusID = (ulong)t.Id,
                    ScreenName = t.User.ScreenName,
                    Text = t.Text,
                    CurrentUserRetweet = (ulong)t.RetweetCount,
                    CreatedAt = GetDate(t.CreatedAt, DateTime.MinValue),
                    Image = t.RetweetedStatus != null && t.RetweetedStatus.User != null ?
                                      t.RetweetedStatus.User.ProfileImageUrlHttps : (t.User.ScreenName == "shanselman" ? "scott159.png" : t.User.ProfileImageUrlHttps)
                });

                if (Device.RuntimePlatform == Device.iOS)
                {
                    // only does anything on iOS, for the Watch
                    DependencyService.Get<ITweetStore>().Save(tweets.ToList());
                }

                Tweets.ReplaceRange(tweets);

            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to load tweets.", "OK");
            }

            IsBusy = false;
            LoadTweetsCommand.ChangeCanExecute();
        }

        public static readonly string[] DateFormats = { "ddd MMM dd HH:mm:ss %zzzz yyyy",
                                                         "yyyy-MM-dd\\THH:mm:ss\\Z",
                                                         "yyyy-MM-dd HH:mm:ss",
                                                         "yyyy-MM-dd HH:mm"};

        public static DateTime GetDate(string date, DateTime defaultValue)
        {
            DateTime result;

            return String.IsNullOrWhiteSpace(date) ||
                !DateTime.TryParseExact(date,
                        DateFormats,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out result)
                    ? defaultValue
                    : result;
        }
    }
}

