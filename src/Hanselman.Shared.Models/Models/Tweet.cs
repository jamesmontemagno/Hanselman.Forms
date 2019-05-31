using Hanselman.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace Hanselman.Models
{
    public partial class Tweet
    {
        public Tweet()
        {
        }

        public string StatusID { get; set; }

        public string ScreenName { get; set; }

        public string Text { get; set; }

        [JsonIgnore]
        public string Date => CreatedAt.ToString("g");
        [JsonIgnore]
        public string DateHumanized => CreatedAt.TwitterHumanize();
        [JsonIgnore]
        public string RTCount => CurrentUserRetweet == 0 ? string.Empty : CurrentUserRetweet + " RT";

        public string Image { get; set; }

        public DateTime CreatedAt
        {
            get;
            set;
        }

        public ulong CurrentUserRetweet
        {
            get;
            set;
        }
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
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}

