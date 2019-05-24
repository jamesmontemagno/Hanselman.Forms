using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using MvvmHelpers;
using Hanselman.Models;
using System.Windows.Input;
using System;
using Xamarin.Essentials;
using Hanselman.Helpers;

namespace Hanselman.ViewModels
{
    public class BlogFeedViewModel : BaseViewModel
    {
        public ObservableRangeCollection<FeedItem> FeedItems { get; }
        public ICommand BlogSelectedCommand { get; }

        public BlogFeedViewModel()
        {
            Title = "Blog";
            Icon = "blog.png";
            FeedItems = new ObservableRangeCollection<FeedItem>();
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

        Command loadItemsCommand;
        /// <summary>
        /// Command to load/refresh items
        /// </summary>
        public Command LoadItemsCommand =>
            loadItemsCommand ?? (loadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand()));

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var responseString = string.Empty;
                using (var httpClient = new HttpClient())
                {
                    var feed = "http://feeds.hanselman.com/ScottHanselman";
                    responseString = await httpClient.GetStringAsync(feed);
                }
                await Task.Delay(1000);
                var items = ParseBlogFeed(responseString);
                FeedItems.ReplaceRange(items);
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to load blog.", "OK");
            }


            IsBusy = false;
        }
        internal static List<FeedItem> ParseBlogFeed(string rss)
        {
            var xdoc = XDocument.Parse(rss);
            var id = 0;

            return (from item in xdoc.Descendants("item")
                    select new FeedItem
                    {
                        Title = (string)item.Element("title"),
                        Caption = ((string)item.Element("description")).ExtractCaption(),
                        FirstImage = ((string)item.Element("description")).ExtractImage(),
                        Link = (string)item.Element("link"),
                        PublishDate = (string)item.Element("pubDate"),
                        Category = (string)item.Element("category"),
                        Id = id++
                    }).ToList();
        }





        /// <summary>
        /// Gets a specific feed item for an Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FeedItem GetFeedItem(int id) => FeedItems.FirstOrDefault(i => i.Id == id);
    }
}

