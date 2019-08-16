using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Hanselman.Models
{
    public class FeedItem
    {
        public FeedItem()
        {
        }

        [JsonProperty("l")]
        public string Link { get; set; }

        [JsonProperty("pd")]
        public string PublishDate { get; set; }

        [JsonProperty("a")]
        public string Author { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("cc")]
        public string CommentCount { get; set; }

        [JsonProperty("c1")]
        public string Category { get; set; }

        [JsonProperty("t")]
        public string Title { get; set; }

        [JsonProperty("c2")]
        public string Caption { get; set; }

        [JsonProperty("fi")]
        public string FirstImage { get;set; }
    }

    public partial class Feeditem
    {
        public static Feeditem[] FromJson(string json) => JsonConvert.DeserializeObject<Feeditem[]>(json, FeedItemConverter.Settings);
    }

    public static partial class Serialize
    {
        public static string ToJson(this Feeditem[] self) => JsonConvert.SerializeObject(self, FeedItemConverter.Settings);
    }

    static class FeedItemConverter
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
