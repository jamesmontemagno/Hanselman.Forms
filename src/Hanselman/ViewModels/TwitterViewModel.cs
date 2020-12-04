using Xamarin.Forms;
using System.Threading.Tasks;
using MvvmHelpers;
using Hanselman.Models;
using Xamarin.Essentials;
using Hanselman.Shared.Models;
using MvvmHelpers.Commands;

namespace Hanselman.ViewModels
{
    public class TwitterViewModel : ViewModelBase
    {

        public TweetSentiment Sentiment { get; set; }
        public GridLength PositiveGridLength { get; set; } = GridLength.Star;
        public GridLength NeutralGridLength { get; set; } = GridLength.Star;
        public GridLength NegativeGridLength { get; set; } = GridLength.Star;
        public ObservableRangeCollection<Tweet> Tweets { get; set; }
        

        public TwitterViewModel()
        {
            Title = "Twitter";
            Icon = "slideout.png";
            Tweets = new ObservableRangeCollection<Tweet>();
            Sentiment = new TweetSentiment();
            OpenTweetCommand = new AsyncCommand<string>(ExecuteOpenTweetCommand);
        }

        public AsyncCommand<string> OpenTweetCommand { get; }

        AsyncCommand? loadCommand;
        AsyncCommand? refreshCommand;
        public AsyncCommand RefreshCommand =>
            refreshCommand ??= new AsyncCommand(Refresh);

        public AsyncCommand LoadCommand =>
            loadCommand ??= new AsyncCommand(Load);


        Task Refresh() => ExecuteLoadCommand(true);
        Task Load() => ExecuteLoadCommand(false);

        public async Task ExecuteLoadCommand(bool forceRefresh)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
#if DEBUG
                await Task.Delay(1000);
#endif
                var items = await DataService.GetTweetsAsync(forceRefresh);
                if (items == null)
                {
                    await DisplayAlert("Error", "Unable to load tweets.", "OK");
                }
                else
                {
                    Tweets.ReplaceRange(items);
                }

                Sentiment = await DataService.GetTwitterSentiment();
                PositiveGridLength = new GridLength(Sentiment.Positive, GridUnitType.Star);
                NeutralGridLength = new GridLength(Sentiment.Neutral, GridUnitType.Star);
                NegativeGridLength = new GridLength(Sentiment.Negative, GridUnitType.Star);
                OnPropertyChanged(nameof(Sentiment));
                OnPropertyChanged(nameof(PositiveGridLength));
                OnPropertyChanged(nameof(NeutralGridLength));
                OnPropertyChanged(nameof(NegativeGridLength));
            }
            catch
            {
                await DisplayAlert("Error", "Unable to load tweets.", "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        async Task ExecuteOpenTweetCommand(string statusId)
        {
            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                if (await Launcher.CanOpenAsync("twitter://"))
                {
                    await Launcher.OpenAsync($"twitter://status?id={statusId}");
                    return;
                }
                else if (await Launcher.CanOpenAsync("tweetbot://"))
                {
                    await Launcher.OpenAsync($"tweetbot:///status/{statusId}");
                    return;
                }
            }

            await OpenBrowserAsync("http://twitter.com/shanselman/status/" + statusId);
        }
    }
}

