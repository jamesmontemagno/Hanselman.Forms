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

        public string Link { get; set; }
        public string PublishDate { get; set; }
        public string Author { get; set; }
        public int Id { get; set; }
        public string CommentCount { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
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
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
