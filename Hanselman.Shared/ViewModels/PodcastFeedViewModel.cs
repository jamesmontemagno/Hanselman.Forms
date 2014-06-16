using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace Hanselman.Shared
{
	public class PodcastFeedViewModel : BaseViewModel
	{
        public PodcastFeedViewModel()
		{
			Title = "Podcast";
			Icon = "podcast.png";
		}


        private ObservableCollection<PodcastFeedItem> feedItems = new ObservableCollection<PodcastFeedItem>();

		/// <summary>
		/// gets or sets the feed items
		/// </summary>
        public ObservableCollection<PodcastFeedItem> FeedItems
		{
			get { return feedItems; }
			set { feedItems = value; OnPropertyChanged("FeedItems"); }
		}

        private PodcastFeedItem selectedFeedItem;
		/// <summary>
		/// Gets or sets the selected feed item
		/// </summary>
        public PodcastFeedItem SelectedFeedItem
		{
			get{ return selectedFeedItem; }
			set
			{
				selectedFeedItem = value;
				OnPropertyChanged("SelectedFeedItem");
			}
		}

		private Command loadItemsCommand;
		/// <summary>
		/// Command to load/refresh items
		/// </summary>
		public Command LoadItemsCommand
		{
			get { return loadItemsCommand ?? (loadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand())); }
		}

		private async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try{
				var httpClient = new HttpClient();
                var feed = "http://pwop.com/feed.aspx?show=hanselminutes&filetype=master";
				var responseString = await httpClient.GetStringAsync(feed);

				FeedItems.Clear();
				var items = await ParseFeed(responseString);
				foreach (var item in items)
				{
					FeedItems.Add(item);
				}
			} catch (Exception ex) {
				var page = new ContentPage();
				var result = page.DisplayAlert ("Error", "Unable to load podcast.", "OK", null);
			}

			IsBusy = false;
		}



		/// <summary>
		/// Parse the RSS Feed
		/// </summary>
		/// <param name="rss"></param>
		/// <returns></returns>
        private async Task<List<PodcastFeedItem>> ParseFeed(string rss)
		{
			return await Task.Run(() =>
				{
					var xdoc = XDocument.Parse(rss);
					var id = 0;
					return (from item in xdoc.Descendants("item")
					    let enclosureElement = item.Element("enclosure")
					    where enclosureElement != null
					    select new PodcastFeedItem
						{
							Title = (string)item.Element("title"),
							Description = (string)item.Element("description"),
							Link = (string)item.Element("link"),
							PublishDate = (string)item.Element("pubDate"),
                            PodcastLink = (string)enclosureElement.Attribute("url"),
							Id = id++
						}).ToList();
				});
		}

		/// <summary>
		/// Gets a specific feed item for an Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public PodcastFeedItem GetFeedItem(int id)
		{
			return FeedItems.FirstOrDefault(i => i.Id == id);
		}
	}
}

