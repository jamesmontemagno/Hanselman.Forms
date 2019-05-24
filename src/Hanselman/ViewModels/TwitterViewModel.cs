using System;
using Xamarin.Forms;
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
using Hanselman.Models;

namespace Hanselman.ViewModels
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

        Command loadTweetsCommand;

        public Command LoadTweetsCommand => loadTweetsCommand ??
                  (loadTweetsCommand = new Command(async () =>
                  {
                      await ExecuteLoadTweetsCommand();
                  }, () =>
                  {
                      return !IsBusy;
                  }));

      

        public async Task ExecuteLoadTweetsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            LoadTweetsCommand.ChangeCanExecute();

            try
            {




               

               // Tweets.ReplaceRange(tweets);

            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to load tweets.", "OK");
            }

            IsBusy = false;
            LoadTweetsCommand.ChangeCanExecute();
        }
    }
}

