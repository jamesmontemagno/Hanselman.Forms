using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Hanselman.Models
{
    public class PodcastEpisode
    {
        public string Title { get; set; }
        public string Date { get; set; }

        public string Description { get; set; }

        public string Mp3Url { get; set; }

        public string ArtworkUrl { get; set; }

        public string Duration { get; set; }

        public bool Explicit { get; set; }

        public string EpisodeNumber { get; set; }

        public string EpisodeUrl { get; set; }
    }

}
