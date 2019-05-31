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
using System.Net;

namespace Hanselman.Functions.Triggers
{
    public static class TwitterFunctions
    {
        [FunctionName("GetTweets")]
        public static HttpResponseMessage RunGetTweets(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            [Blob("hanselman/twitter.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inBlob,
            ILogger log)
        {
            try
            {
                log.LogInformation("Reading feed to twitter.");
                var json = string.Empty;
                using (var reader = new StreamReader(inBlob))
                {
                    json = reader.ReadToEnd();
                }

                log.LogInformation("Finished reading twitter feed from stream.");

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };          

            }
            catch (Exception ex)
            {
                log.LogError(ex, "Unable to get twitter feed");
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        [FunctionName("TwitterUpdate")]
        public static async Task RunTwitterUpdate(
#if DEBUG
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
#else
            [TimerTrigger("0 */15 * * * *")]TimerInfo myTimer,
#endif
            [Blob("hanselman/twitter.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outTwitterBlob,
            [HttpClientFactory]HttpClient client,
            ILogger log)
        {
            try
            {

                log.LogInformation("Getting twitter feed.");

                var tweetsRaw = await TwitterHelpers.GetTweets(client);

                var json = JsonConvert.SerializeObject(tweetsRaw);

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
