using Xamarin.Forms;
using System.Threading.Tasks;
using MvvmHelpers;
using Hanselman.Models;

namespace Hanselman.ViewModels
{
    public class TwitterViewModel : ViewModelBase
    {

        public ObservableRangeCollection<Tweet> Tweets { get; set; }

        public TwitterViewModel()
        {
            Title = "Twitter";
            Icon = "slideout.png";
            Tweets = new ObservableRangeCollection<Tweet>();
        }

        Command loadCommand;
        Command refreshCommand;
        public Command RefreshCommand => refreshCommand ??
                  (refreshCommand = new Command(async () =>
                  {
                      await ExecuteLoadCommand(true);
                  }, () =>
                  {
                      return !IsBusy;
                  }));

        public Command LoadCommand => loadCommand ??
                  (loadCommand = new Command(async () =>
                  {
                      await ExecuteLoadCommand(false);
                  }, () =>
                  {
                      return !IsBusy;
                  }));

      

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
    }
}

