using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using MvvmHelpers;
using Hanselman.Models;

namespace Hanselman.ViewModels
{
    public class BlogFeedViewModel : BaseViewModel
    {
        public BlogFeedViewModel()
        {
            Title = "Blog";
            Icon = "blog.png";
        }


        /// <summary>
        /// gets or sets the feed items
        /// </summary>
        public ObservableRangeCollection<FeedItem> FeedItems { get; } = new ObservableRangeCollection<FeedItem>();

        FeedItem selectedFeedItem;
        /// <summary>
        /// Gets or sets the selected feed item
        /// </summary>
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
                var items = await ParseFeed(responseString);
                FeedItems.ReplaceRange(items);
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to load blog.", "OK");
            }


            IsBusy = false;
        }



        /// <summary>
        /// Parse the RSS Feed
        /// </summary>
        /// <param name="rss"></param>
        /// <returns></returns>
        async Task<List<FeedItem>> ParseFeed(string rss)
        {
            return await Task.Run(() =>
                {
                    var xdoc = XDocument.Parse(rss);
                    var id = 0;
                    return (from item in xdoc.Descendants("item")
                            select new FeedItem
                            {
                                Title = (string)item.Element("title"),
                                Description = (string)item.Element("description"),
                                Link = (string)item.Element("link"),
                                PublishDate = (string)item.Element("pubDate"),
                                Category = (string)item.Element("category"),
                                Id = id++
                            }).ToList();
                });
        }

        /// <summary>
        /// Gets a specific feed item for an Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FeedItem GetFeedItem(int id) => FeedItems.FirstOrDefault(i => i.Id == id);
    }
}

