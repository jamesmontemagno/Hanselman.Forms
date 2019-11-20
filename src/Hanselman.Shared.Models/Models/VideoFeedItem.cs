using System;
using System.Collections.Generic;
using Hanselman.Helpers;
using Newtonsoft.Json;

namespace Hanselman.Models
{
    [Preserve(AllMembers = true)]
    public class VideoFeedItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("t")]
        public string Title { get; set; }

        [JsonProperty("d1")]
        public string Date { get; set; }

        [JsonProperty("d2")]
        public string Description { get; set; }

        [JsonProperty("d3")]
        public TimeSpan Duration { get; set; }

        [JsonProperty("u")]
        public string Url { get; set; }

        [JsonProperty("tu")]
        public string ThumbnailUrl { get; set; }

        [JsonProperty("vu")]
        public List<VideoContentItem> VideoUrls { get; set; }

        string displayDate;
        [JsonIgnore]
        public string DisplayDate
        {
            get => DateTimeOffset.TryParse(Date, out var time) ? time.PodcastEpisodeHumanize() : Date;
            set => displayDate = value;
        }
    }
}