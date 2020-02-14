using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Hanselman.Models
{
    public enum FeaturedItemType
    {
        Blog = 0,
        Podcast = 1,
        Video = 2
    }

    public class FeaturedItem
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonProperty("t")]
        [JsonPropertyName("t")]
        [Required]
        public string Title { get; set; }

        [JsonProperty("i")]
        [JsonPropertyName("i")]
        [Required]
        public string Image { get; set; }

        [JsonProperty("l")]
        [JsonPropertyName("l")]
        [Required]
        public string Link { get; set; }

        [JsonProperty("fit")]
        [JsonPropertyName("fit")]
        [Required]
        public FeaturedItemType Type { get; set; } = FeaturedItemType.Blog;
    }
}
