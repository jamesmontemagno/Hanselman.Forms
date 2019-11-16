using System.Collections.Generic;
using Hanselman.Helpers;
using Hanselman.Models;
using Hanselman.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace Hanselman.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AsyncCommand GoToSettingsCommand { get; set; }
        public List<SocialItem> SocialItems { get; }
        public AboutViewModel()
        {
            SocialItems = new List<SocialItem>
            {
                new SocialItem
                {
                    Icon = IconConstants.TwitterCircle,
                    Url = "https://www.twitter.com/shanselman"
                },
                new SocialItem
                {
                    Icon = IconConstants.FacebookBox,
                    Url = "https://www.facebook.com/shanselman"
                },
                new SocialItem
                {
                    Icon = IconConstants.Instagram,
                    Url = "https://www.instagram.com/shanselman"
                }
            };

            GoToSettingsCommand = new AsyncCommand(() => Application.Current.MainPage.Navigation.PushModalAsync(new SettingsPage()));
        }
    }
}
