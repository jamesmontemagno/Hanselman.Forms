using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Hanselman.Portable.Models;
using Xamarin.Forms;

namespace Hanselman.Portable.ViewModels
{
    public class Channel9VideosViewModel : BaseViewModel
    {
        private VideoFeedItem _selectedFeedItem;

        public Channel9VideosViewModel()
        {
            Title = "Channel 9 videos";
            FeedItems = new ObservableCollection<VideoFeedItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        /// <summary>
        ///     Gets or sets the selected feed item
        /// </summary>
        public VideoFeedItem SelectedFeedItem
        {
            get { return _selectedFeedItem; }
            set
            {
                _selectedFeedItem = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadItemsCommand { get; }
        public ObservableCollection<VideoFeedItem> FeedItems { get; }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var error = false;
            try
            {
                var httpClient = new HttpClient();

                var feed = "https://channel9.msdn.com/Feeds/RSS";
                var responseString = await httpClient.GetStringAsync(feed);

                FeedItems.Clear();
                var items = await ParseFeed(responseString);
                foreach (var feedItem in items)
                    FeedItems.Add(feedItem);
            }
            catch
            {
                error = true;
            }
            finally
            {
                IsBusy = false;
            }

            if (error)
            {
                var page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to load channel 9 videos feed.", "OK");
            }
        }

        private async Task<IEnumerable<VideoFeedItem>> ParseFeed(string rss)
        {
            return await Task.Run(() =>
            {
                var xdoc = XDocument.Parse(rss);
                var list = new List<VideoFeedItem>();
                XNamespace media = "http://search.yahoo.com/mrss/";
                XNamespace itunes = "http://www.itunes.com/dtds/podcast-1.0.dtd";
                foreach (var item in xdoc.Descendants("item"))
                {
                    try
                    {
                        bool foundTheMagicKeyword = false;
                        foreach (var element in item.Elements())
                        {
                            var keyword = "hanselman".ToLowerInvariant();
                            if (element.Value.ToLowerInvariant().Contains(keyword))
                            {
                                foundTheMagicKeyword = true;
                                break;
                            }
                        }
                        if (!foundTheMagicKeyword)
                            continue;

                        var mediaGroup = item.Element(media + "group");
                        if (mediaGroup == null)
                            continue;

                        List<VideoContentItem> videoUrls = new List<VideoContentItem>();
                        foreach (var mediaUrl in mediaGroup.Elements())
                        {
                            var duration = mediaUrl.Attribute("duration").Value;
                            var fileSize = mediaUrl.Attribute("fileSize").Value;
                            var url = mediaUrl.Attribute("url").Value;
                            videoUrls.Add(new VideoContentItem()
                                {
                                    Duration = TimeSpan.FromSeconds(Convert.ToInt32(duration)),
                                    FileSize = long.Parse(fileSize),
                                    Url = url,
                                }
                            );
                        }

                        var videoFeedItem = new VideoFeedItem
                        {
                            VideoUrls = videoUrls.OrderByDescending(url => url.FileSize).ToList(),
                            Title = (string) item.Element("title"),
                            Description = (string) item.Element(itunes + "summary")?.Value,
                            Link = (string) item.Element("link"),
                            PublishDate = (string) item.Element("pubDate"),
                            Category = (string) item.Element("category"),
                            ThumbnailUrl = (string) item.Element(media + "thumbnail")?.Attribute("url")?.Value
                        };

                        list.Add(videoFeedItem);

                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("Unable to parse rss for item");
                    }
                }
                return list;
            });
        }
    }
}