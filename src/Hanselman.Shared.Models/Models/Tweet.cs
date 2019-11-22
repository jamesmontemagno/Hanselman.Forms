using Hanselman.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace Hanselman.Models
{
    [Preserve(AllMembers = true)]
    public partial class Tweet
    {
        public Tweet()
        {
        }

        [JsonProperty("sid")]
        public string StatusID { get; set; }

        [JsonProperty("sn")]
        public string ScreenName { get; set; }

        [JsonProperty("t")]
        public string Text { get; set; }

        [JsonProperty("i")]
        public string Image { get; set; }

        [JsonProperty("ca")]
        public DateTime CreatedAt
        {
            get;
            set;
        }

        [JsonProperty("rc")]
        public long RetweetCount { get; set; }

        [JsonProperty("fc")]
        public long FavoriteCount { get; set; }

        [JsonProperty("m")]
        public string MediaUrl { get; set; }


        [JsonIgnore]
        public bool HasMedia => !string.IsNullOrWhiteSpace(MediaUrl);

        [JsonIgnore]
        public string Date => CreatedAt.ToString("g");
        [JsonIgnore]
        public string DateHumanized => CreatedAt.TwitterHumanize();
        [JsonIgnore]
        public string RTCount => RetweetCount == 0 ? string.Empty : RetweetCount + " RT";
    }

    public partial class Tweet
    {
        public static Tweet[] FromJson(string json) => JsonConvert.DeserializeObject<Tweet[]>(json, TweetConverter.Settings);
    }

    public static partial class Serialize
    {
        public static string ToJson(this Tweet[] self) => JsonConvert.SerializeObject(self, TweetConverter.Settings);
    }

    static class TweetConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Formatting = Formatting.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}

