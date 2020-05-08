using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Hanselman.Helpers;
using Hanselman.Interfaces;
using Hanselman.Models;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Hanselman.Services;
using System.Diagnostics;
using Hanselman.Shared.Models;

[assembly: Dependency(typeof(DataService))]
namespace Hanselman.Services
{
    [Preserve(AllMembers = true)]
    public class DataService : IDataService
    {
        readonly HttpClient client;
        readonly MockDataService mock;
        public DataService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri(Constants.BaseUrl)
            };

            mock = new MockDataService();
        }
        public async Task<IEnumerable<BlogFeedItem>> GetBlogItemsAsync(bool forceRefresh)
        {
            var items = await GetAsync<IEnumerable<FeedItem>>($"api/GetBlogFeed?code={Constants.BlogKey}", "blogfeed", 30, forceRefresh);

            return items?.Select(i => new BlogFeedItem(i)) ?? Enumerable.Empty<BlogFeedItem>();
        }

        public Task<IEnumerable<FeaturedItem>> GetFeaturedItemsAsync() =>
            mock.GetFeaturedItemsAsync();

        public Task<IEnumerable<PodcastEpisode>> GetPodcastEpisodesAsync(string id, bool forceRefresh) => 
            GetAsync<IEnumerable<PodcastEpisode>>($"api/GetPodcastEpisodes?code={Constants.PodcastEpisodesKey}&id={id}", $"pod_{id}", 180, false);

        IEnumerable<Podcast> podcastCache;
        public async Task<IEnumerable<Podcast>> GetPodcastsAsync(bool forceRefresh)
        {
            if(forceRefresh || podcastCache == null)
                podcastCache = await mock.GetPodcastsAsync(forceRefresh);

            return podcastCache;
        }

        public Podcast? GetPodcast(string id) =>
            podcastCache?.FirstOrDefault(p => p.Id == id);

        public Task<IEnumerable<Tweet>> GetTweetsAsync(bool forceRefresh) =>
            GetAsync<IEnumerable<Tweet>>($"api/GetTweets?code={Constants.TweetKey}", "tweets", 15, forceRefresh);

        Dictionary<string, IEnumerable<VideoFeedItem>> videoSeriesCache = new Dictionary<string, IEnumerable<VideoFeedItem>>();
        public VideoFeedItem? GetVideoEpisode(string seriesId, string id)
        {
            if (!videoSeriesCache.ContainsKey(seriesId))
                return null;

            return videoSeriesCache[seriesId].FirstOrDefault(v => v.Id == id);
        }

        public async Task<IEnumerable<VideoFeedItem>> GetVideoEpisodesAsync(string id, bool forceRefresh)
        {
            if (!forceRefresh && videoSeriesCache.ContainsKey(id))
                return videoSeriesCache[id];

            var videos = await GetAsync<IEnumerable<VideoFeedItem>>($"api/GetVideoEpisodes?code={Constants.VideoEpisodesKey}&id={id}", $"video_{id}", 240, false);

            if (videoSeriesCache.ContainsKey(id))
                videoSeriesCache[id] = videos;
            else
                videoSeriesCache.Add(id, videos);
            return videos;
        }

        public Task<TweetSentiment> GetTwitterSentiment() =>
            GetAsync<TweetSentiment>($"api/GetTweetSentiment?code={Constants.TweetSentimentKey}", "tweetsentiment", 15, false);

        public Task<IEnumerable<VideoFeedItem>> GetVideoEpisodesAsync(string id, bool forceRefresh) =>
            GetAsync<IEnumerable<VideoFeedItem>>($"api/GetVideoEpisodes?code={Constants.VideoEpisodesKey}&id={id}", $"video_{id}", 240, false);

        public Task<IEnumerable<VideoSeries>> GetVideoSeriesAsync(bool forceRefresh) =>
            mock.GetVideoSeriesAsync(forceRefresh);

        async Task<T> GetAsync<T>(string url, string key, int mins = 7, bool forceRefresh = false)
        {
            var json = string.Empty;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                json = Barrel.Current.Get<string>(key);
            else if (!forceRefresh && !Barrel.Current.IsExpired(key))
                json = Barrel.Current.Get<string>(key);

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await client.GetStringAsync(url);
                    Barrel.Current.Add(key, json, TimeSpan.FromMinutes(mins));
                }
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
                throw ex;
            }
        }

    }
}
