using System.Collections.Generic;
using System.Threading.Tasks;
using Hanselman.Models;
using Hanselman.Shared.Models;

namespace Hanselman.Interfaces
{
    public interface IDataService
    {
        Task<TweetSentiment> GetTwitterSentiment();
        Task<IEnumerable<Podcast>> GetPodcastsAsync(bool forceRefresh);
        Podcast? GetPodcast(string id);
        Task<IEnumerable<PodcastEpisode>> GetPodcastEpisodesAsync(string id, bool forceRefresh);
        Task<IEnumerable<BlogFeedItem>> GetBlogItemsAsync(bool forceRefresh);
        Task<IEnumerable<Tweet>> GetTweetsAsync(bool forceRefresh);
        Task<IEnumerable<VideoSeries>> GetVideoSeriesAsync(bool forceRefresh);
        Task<IEnumerable<VideoFeedItem>> GetVideoEpisodesAsync(string id, bool forceRefresh);
        VideoFeedItem? GetVideoEpisode(string seriesId, string id);
        Task<IEnumerable<FeaturedItem>> GetFeaturedItemsAsync();
    }
}
