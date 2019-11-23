using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Net.Http;
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
            [Blob("hanselman/video-dotnet.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inDotNet,
            [Blob("hanselman/video-csharp.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inCSharp,
           ILogger log)
        {
            string name = req.Query["id"];
            switch (name)
            {
                case "azurefridays":
                    return BlobHelpers.BlobToHttpResponseMessage(inFridays, log, name);
                case "events":
                    return BlobHelpers.BlobToHttpResponseMessage(inEvents, log, name);
                case "dotnet":
                    return BlobHelpers.BlobToHttpResponseMessage(inDotNet, log, name);
                case "csharp":
                    return BlobHelpers.BlobToHttpResponseMessage(inCSharp, log, name);
                default:
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [FunctionName(nameof(VideosUpdate))]
        public static async Task VideosUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
            [Blob("hanselman/video-azurefridays.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outFridays,
            [Blob("hanselman/video-events.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outEvents,
            [Blob("hanselman/video-dotnet.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outDotNet,
            [Blob("hanselman/video-csharp.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outCSharp,
            [HttpClientFactory]HttpClient client,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var videoFeeds = new Dictionary<string, (Stream blob, string photo)>();

            string link = req.Query["id"];

            var azureFridayLogo = "https://hanselmanformsstorage.blob.core.windows.net/hanselman-public/azure_friday.jpg";
            var eventsLogo = "https://hanselmanformsstorage.blob.core.windows.net/hanselman-public/scott_events.jpg";
            var csharpLogo = "https://hanselmanformsstorage.blob.core.windows.net/hanselman-public/csharp101.jpg";
            var dotnetLogo = "https://hanselmanformsstorage.blob.core.windows.net/hanselman-public/dotnet101.jpg";

            const string azureFridays = "https://s.ch9.ms/Shows/Azure-Friday/feed/mp4";
            const string azureFridays2 = "https://s.ch9.ms/Shows/Azure-Friday/feed/mp4?page=2";
            const string hanselmanVids = "https://s.ch9.ms/Events/Speakers/scott-hanselman/RSS/mp4";
            const string csharp101 = "https://s.ch9.ms/Series/CSharp-101/feed/mp4";
            const string dotnet101 = "https://s.ch9.ms/Series/NET-Core-101/feed/mp4";

            switch (link)
            {

                case azureFridays:
                    videoFeeds.Add(link, (outFridays, azureFridayLogo));
                    break;
                case hanselmanVids:
                    videoFeeds.Add(link, (outEvents, eventsLogo));
                    break;
                case csharp101:
                    videoFeeds.Add(link, (outCSharp, csharpLogo));
                    break;
                case dotnet101:
                    videoFeeds.Add(link, (outDotNet, dotnetLogo));
                    break;
                default:
                    videoFeeds.Add(azureFridays, (outFridays, azureFridayLogo));
                    videoFeeds.Add(hanselmanVids, (outEvents, eventsLogo));
                    videoFeeds.Add(csharp101, (outCSharp, csharpLogo));
                    videoFeeds.Add(dotnet101, (outDotNet, dotnetLogo));
                    break;
            }

            foreach (var feed in videoFeeds)
            {
                var rss = await client.GetStringAsync(feed.Key);
                var parse = FeedItemHelpers.ParseVideoFeed(rss, feed.Value.photo);
                
                //Get Azure Fridays page 2
                if (rss == azureFridays)
                    parse.AddRange(FeedItemHelpers.ParseVideoFeed(azureFridays2, feed.Value.photo));

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
