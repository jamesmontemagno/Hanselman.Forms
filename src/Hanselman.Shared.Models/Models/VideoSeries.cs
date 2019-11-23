using System;
using Newtonsoft.Json;

namespace Hanselman.Models
{
    public class VideoSeries
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Art { get; set; }

        [JsonIgnore]
        public string UriRoute =>
            $"{nameof(Id)}={Id}&{nameof(Title)}={Uri.EscapeDataString(Title)}";

        /*public string ToUriRoute() =>
            Uri.EscapeDataString(JsonConvert.SerializeObject(this));

        public static VideoSeries FromUriRoute(string route) =>
            JsonConvert.DeserializeObject<VideoSeries>(Uri.UnescapeDataString(route));*/
    }
}