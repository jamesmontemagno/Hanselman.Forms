using System.Collections.Generic;
using System.Threading.Tasks;
using Hanselman.Models;

namespace Hanselman.Interfaces
{
    public interface IDataService
    {
        Task<IEnumerable<Podcast>> GetPodcastsAsync(bool forceRefresh);
        Task<IEnumerable<PodcastEpisode>> GetPodcastEpisodesAsync(string id, bool forceRefresh);
        Task<IEnumerable<BlogFeedItem>> GetBlogItemsAsync(bool forceRefresh);
        Task<IEnumerable<Tweet>> GetTweetsAsync(bool forceRefresh);
        Task<IEnumerable<VideoSeries>> GetVideoSeriesAsync(bool forceRefresh);
        Task<IEnumerable<VideoFeedItem>> GetVideoEpisodesAsync(string id, bool forceRefresh);
        Task<IEnumerable<FeaturedItem>> GetFeaturedItemsAsync();
    }
}
