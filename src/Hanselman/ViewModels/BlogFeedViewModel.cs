using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;
using MvvmHelpers;
using Hanselman.Models;
using System.Windows.Input;
using Xamarin.Essentials;
using System;

namespace Hanselman.ViewModels
{
    public class BlogFeedViewModel : ViewModelBase
    {
        public ObservableRangeCollection<BlogFeedItem> FeedItems { get; }
        public ICommand BlogSelectedCommand { get; }

        public BlogFeedViewModel()
        {
            Title = "Blog";
            Icon = "blog.png";
            FeedItems = new ObservableRangeCollection<BlogFeedItem>();
            BlogSelectedCommand = new Command(async () => await ExecuteBlogSelectedCommand());
        }

        async Task ExecuteBlogSelectedCommand()
        {
            if (SelectedFeedItem == null)
                return;

            await Browser.OpenAsync(SelectedFeedItem.Link, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredControlColor = Color.White,
                PreferredToolbarColor = (Color)Application.Current.Resources["PrimaryColor"]
            });

            SelectedFeedItem = null;
        }

        FeedItem selectedFeedItem;

        public FeedItem SelectedFeedItem
        {
            get => selectedFeedItem;
            set => SetProperty(ref selectedFeedItem, value);
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


        async Task ExecuteLoadCommand(bool forceRefresh)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
#if DEBUG
                await Task.Delay(1000);
#endif
                var items = await DataService.GetBlogItemsAsync(forceRefresh);
                if(items == null)
                {
                    await DisplayAlert("Error", "Unable to load blog.", "OK");
                }
                else
                {
                    FeedItems.ReplaceRange(items);
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", "Unable to load blog.", "OK");
            }
            finally
            {
                IsBusy = false;
            }

            LoadCommand.ChangeCanExecute();
        }

        /// <summary>
        /// Gets a specific feed item for an Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FeedItem GetFeedItem(string id) => FeedItems.FirstOrDefault(i => i.Id == id);
    }
}

