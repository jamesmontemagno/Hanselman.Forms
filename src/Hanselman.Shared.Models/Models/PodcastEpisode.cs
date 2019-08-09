using System;
using Hanselman.Helpers;
using Newtonsoft.Json;

namespace Hanselman.Models
{
    public class PodcastEpisode
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("t")]
        public string Title { get; set; }

        [JsonProperty("d1")]
        public string Date { get; set; }

        [JsonProperty("d2")]
        public string Description { get; set; }

        [JsonProperty("m")]
        public string Mp3Url { get; set; }

        [JsonProperty("l")]
        public string ArtworkUrl { get; set; }

        [JsonProperty("d3")]
        public string Duration { get; set; }

        [JsonProperty("e")]
        public string Explicit { get; set; }

        [JsonProperty("en")]
        public string EpisodeNumber { get; set; }

        [JsonProperty("eu")]
        public string EpisodeUrl { get; set; }

        [JsonIgnore]
        public string PodcastName
        {
            get;set;
        }

        string displayDate;
        [JsonIgnore]
        public string DisplayDate
        {
            get => DateTimeOffset.TryParse(Date, out var time) ? time.PodcastEpisodeHumanize() : Date;
            set => displayDate = value;
        }
    }

}
