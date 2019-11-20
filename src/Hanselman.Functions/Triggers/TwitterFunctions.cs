using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Hanselman.Functions.Helpers;

namespace Hanselman.Functions.Triggers
{
    public static class TwitterFunctions
    {
        [FunctionName(nameof(GetTweets))]
        public static HttpResponseMessage GetTweets(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            [Blob("hanselman/twitter.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inBlob,
            ILogger log)
        {
            return BlobHelpers.BlobToHttpResponseMessage(inBlob, log, "tweets");
        }

        [FunctionName(nameof(TwitterUpdate))]
        public static async Task TwitterUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
            [Blob("hanselman/twitter.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outTwitterBlob,
            [HttpClientFactory]HttpClient client,
            ILogger log)
        {
            try
            {

                log.LogInformation("Getting twitter feed.");

                var tweetsRaw = await TwitterHelpers.GetTweets(client);

                var json = JsonConvert.SerializeObject(tweetsRaw, Formatting.None);

                log.LogInformation("Writting feed to blob.");
                using (var writer = new StreamWriter(outTwitterBlob))
                {
                    writer.Write(json);
                }

                log.LogInformation("Twitter function finished.");

            }
            catch (Exception ex)
            {
                log.LogError(ex, "Unable to get twitter feed");
            }
        }
    }
}
