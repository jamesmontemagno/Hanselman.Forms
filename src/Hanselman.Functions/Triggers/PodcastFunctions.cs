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

namespace Hanselman.Functions.Triggers
{
    public class PodcastFunctions
    {
        [FunctionName(nameof(GetPodcastEpisodes))]
        public static HttpResponseMessage GetPodcastEpisodes(
           [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
            [Blob("hanselman/minutes.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inMinutes,
            [Blob("hanselman/ratchet.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inRatchet,
            [Blob("hanselman/life.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inLife,
           ILogger log)
        {
            string name = req.Query["id"];
            switch (name)
            {
                case "minutes":
                    return BlobHelpers.BlobToHttpResponseMessage(inMinutes, log, name);
                case "ratchet":
                    return BlobHelpers.BlobToHttpResponseMessage(inRatchet, log, name);
                case "life":
                    return BlobHelpers.BlobToHttpResponseMessage(inLife, log, name);
                default:
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [FunctionName(nameof(PodcastUpdate))]
        public static async Task PodcastUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
            [Blob("hanselman/minutes.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outMinutes,
            [Blob("hanselman/ratchet.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outRatchet,
            [Blob("hanselman/life.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outLife,
            [HttpClientFactory]HttpClient client,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var podcasts = new Dictionary<string, (Stream blob, string photo)>();

            string link = req.Query["id"];

            var hanselmanLogo = "https://hanselmanformsstorage.blob.core.windows.net/hanselman-public/hm_full.jpg";
            var ratchetLogo = "https://hanselmanformsstorage.blob.core.windows.net/hanselman-public/ratchet_full.jpg";
            var lifeLogo = "https://hanselmanformsstorage.blob.core.windows.net/hanselman-public/tdl_full.jpg";

            switch (link)
            {
                case "http://feeds.podtrac.com/9dPm65vdpLL1":
                    podcasts.Add(link, (outMinutes, hanselmanLogo));
                    break;
                case "http://feeds.feedburner.com/RatchetAndTheGeek?format=xml":
                    podcasts.Add(link, (outRatchet, ratchetLogo));
                    break;
                case "http://feeds.feedburner.com/ThisDevelopersLife?format=xml":
                    podcasts.Add(link, (outLife, lifeLogo));
                    break;
                default:
                    podcasts.Add("http://feeds.podtrac.com/9dPm65vdpLL1", (outMinutes, hanselmanLogo));
                    podcasts.Add("http://feeds.feedburner.com/RatchetAndTheGeek?format=xml", (outRatchet, ratchetLogo));
                    podcasts.Add("http://feeds.feedburner.com/ThisDevelopersLife?format=xml", (outLife, lifeLogo));
                    break;
            }

            foreach (var pod in podcasts)
            {
                var rss = await client.GetStringAsync(pod.Key);
                var parse = FeedItemHelpers.ParsePodcastFeed(rss, pod.Value.photo);

                log.LogInformation("Writting feed to blob.");
                using (var writer = new StreamWriter(pod.Value.blob))
                {
                    var json = JsonConvert.SerializeObject(parse, Formatting.None);
                    writer.Write(json);
                }
            }
           

            log.LogInformation("Podcast function finished.");
        }
    }
}
