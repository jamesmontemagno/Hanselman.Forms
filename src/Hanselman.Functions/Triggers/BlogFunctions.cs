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

// LotanB cheered 5 May 17, 2019
// LotanB cheered 10 May 17, 2019
// ElectricHavoc cheered 5 May 17, 2019
// ElectricHavoc cheered 295 May 17, 2019
// LotanB gifted 3 subs May 17, 2019

namespace Hanselman.Functions
{
    public static class TimerFunctions
    {

        [FunctionName("GetBlogFeed")]
        public static HttpResponseMessage RunGetBlogFeed(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            [Blob("hanselman/blog.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inBlob,
            ILogger log)
        {
            try
            {
                log.LogInformation("Reading feed to blog.");
                var json = string.Empty;
                using (var reader = new StreamReader(inBlob))
                {
                    json = reader.ReadToEnd();
                }

                log.LogInformation("Finished reading blog feed from stream.");

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Unable to get blog feed");
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }


        [FunctionName("BlogUpdate")]
        public static async Task RunBlogUpdate(
#if DEBUG
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
#else
            [TimerTrigger("0 */30 * * * *")]TimerInfo myTimer,
#endif
            [Blob( "hanselman/blog.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outBlogBlob,
            [HttpClientFactory]HttpClient client,
            ILogger log)
        {
            var feedurl = "http://feeds.hanselman.com/ScottHanselman";
            
            try
            {
                log.LogInformation("Getting blog feed.");
                var feed = await client.GetStringAsync(feedurl);

                log.LogInformation("Parsing blog feed.");
                var blogItems = FeedItemHelpers.ParseBlogFeed(feed);

                var json = JsonConvert.SerializeObject(blogItems);

                log.LogInformation("Writting feed to blob.");
                using (var writer = new StreamWriter(outBlogBlob))
                {
                    writer.Write(json);
                }


                log.LogInformation("Blog function finished.");

            }
            catch (Exception ex)
            {
                log.LogError(ex, "Unable to get blog feed");
            }
        }
    }
}
