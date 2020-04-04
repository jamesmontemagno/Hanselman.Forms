using Xamarin.Forms;
using System.Threading.Tasks;
using MvvmHelpers;
using Hanselman.Models;
using Xamarin.Essentials;
using Hanselman.Shared.Models;

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
            OpenTweetCommand = new Command<string>(async (s) => await ExecuteOpenTweetCommand(s));
        }

        public Command<string> OpenTweetCommand { get; }

        Command? loadCommand;
        Command? refreshCommand;
        public Command RefreshCommand =>
            refreshCommand ??= new Command(async () =>
                  {
                      await ExecuteLoadCommand(true);
                  }, () =>
                  {
                      return !IsBusy;
                  });

        public Command LoadCommand =>
            loadCommand ??= new Command(async () =>
                  {
                      await ExecuteLoadCommand(false);
                  }, () =>
                  {
                      return !IsBusy;
                  });

      

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
            }

            LoadCommand.ChangeCanExecute();
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

