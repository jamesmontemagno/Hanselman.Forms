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

namespace Hanselman.Functions.Triggers
{
    public class PodcastFunctions
    {

        [FunctionName("PodcastUpdate")]
        public static async Task RunPodcastUpdate(
#if DEBUG
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
#else
            [TimerTrigger("0 */180 * * * *")]TimerInfo myTimer,
#endif
            [Blob("hanselman/minutes.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outMinutes,
            [Blob("hanselman/ratchet.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outRatchet,
            [Blob("hanselman/life.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outLife,
            [HttpClientFactory]HttpClient client,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            log.LogInformation("Podcast function finished.");
        }
    }
}
