﻿using System;
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

[assembly: Dependency(typeof(DataService))]
namespace Hanselman.Services
{
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

            return items?.Select(i => new BlogFeedItem(i));
        }
        public Task<IEnumerable<PodcastEpisode>> GetPodcastEpisodesAsync(bool forceRefresh) =>
            mock.GetPodcastEpisodesAsync(forceRefresh);

        public Task<IEnumerable<Podcast>> GetPodcastsAsync(bool forceRefresh) =>
            mock.GetPodcastsAsync(forceRefresh);

        public Task<IEnumerable<Tweet>> GetTweetsAsync(bool forceRefresh) =>
            GetAsync<IEnumerable<Tweet>>($"api/GetTweets?code={Constants.TweetKey}", "tweets", 15, forceRefresh);

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
                Console.WriteLine($"Unable to get information from server {ex}");
            }

            return default;
        }
    }
}
