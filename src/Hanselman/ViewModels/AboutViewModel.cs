using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Hanselman.Helpers;
using Hanselman.Models;
using Hanselman.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace Hanselman.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        public AsyncCommand GoToSettingsCommand { get; set; }
        public List<SocialItem> SocialItems { get; }
        public ObservableRangeCollection<FeaturedItem> FeaturedItems { get; } = new ObservableRangeCollection<FeaturedItem>();
        

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

        bool hasData;
        public bool HasData
        {
            get => hasData;
            set => SetProperty(ref hasData, value);
        }

        ICommand? loadCommand;
        public ICommand LoadCommand =>
            loadCommand ??= new AsyncCommand(() => ExecuteLoadCommand());



        async Task ExecuteLoadCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            HasData = true;
            try
            {
                var items = await DataService.GetFeaturedItemsAsync();
                if (items == null)
                {
                    await DisplayAlert("Error", "Unable to load Featured Items.", "OK");
                }
                else
                {
                    FeaturedItems.ReplaceRange(items);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            HasData = FeaturedItems.Count > 0;
        }
    }
}
