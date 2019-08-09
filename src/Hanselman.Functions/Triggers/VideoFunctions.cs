using System;
using Hanselman.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using System.Linq;
using Hanselman.Functions.Models;
using System.Net.Http;
using System.Text;
using System.Json;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Hanselman.Functions.Helpers;
using System.Net;

// blounty__ cheered 100 August 9, 2019

namespace Hanselman.Functions.Triggers
{
    public class VideoFunctions
    {
        [FunctionName(nameof(GetVideoEpisodes))]
        public static HttpResponseMessage GetVideoEpisodes(
           [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
            [Blob("hanselman/video-azurefridays.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inFridays,
            [Blob("hanselman/video-events.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inEvents,
           ILogger log)
        {
            string name = req.Query["id"];
            switch (name)
            {
                case "azurefridays":
                    return BlobHelpers.BlobToHttpResponseMessage(inFridays, log, name);
                case "events":
                    return BlobHelpers.BlobToHttpResponseMessage(inEvents, log, name);
                default:
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [FunctionName(nameof(VideosUpdate))]
        public static async Task VideosUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
            [Blob("hanselman/video-azurefridays.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outFridays,
            [Blob("hanselman/video-events.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outEvents,
            [HttpClientFactory]HttpClient client,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var videoFeeds = new Dictionary<string, (Stream blob, string photo)>();

            string link = req.Query["id"];

            var azureFridayLogo = "https://hanselmanformsstorage.blob.core.windows.net/hanselman-public/azure_friday.jpg";
            var eventsLogo = "https://hanselmanformsstorage.blob.core.windows.net/hanselman-public/scott_events.jpg";

            switch (link)
            {
                case "https://s.ch9.ms/Niners/Glucose/Posts/RSS/mp4":
                    videoFeeds.Add(link, (outFridays, azureFridayLogo));
                    break;
                case "https://s.ch9.ms/Events/Speakers/scott-hanselman/RSS/mp4":
                    videoFeeds.Add(link, (outEvents, eventsLogo));
                    break;
                default:
                    videoFeeds.Add("https://s.ch9.ms/Niners/Glucose/Posts/RSS/mp4", (outFridays, azureFridayLogo));
                    videoFeeds.Add("https://s.ch9.ms/Events/Speakers/scott-hanselman/RSS/mp4", (outEvents, eventsLogo));
                    break;
            }

            foreach (var feed in videoFeeds)
            {
                var rss = await client.GetStringAsync(feed.Key);
                var parse = FeedItemHelpers.ParseVideoFeed(rss, feed.Value.photo);

                log.LogInformation("Writting feed to blob.");
                using (var writer = new StreamWriter(feed.Value.blob))
                {
                    var json = JsonConvert.SerializeObject(parse, Formatting.None);
                    writer.Write(json);
                }
            }


            log.LogInformation("Podcast function finished.");
        }
    }
}
