using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Hanselman.Functions.Helpers;
using System.Collections.Generic;
using Hanselman.Models;

// LotanB cheered 5 May 17, 2019
// LotanB cheered 10 May 17, 2019
// ElectricHavoc cheered 5 May 17, 2019
// ElectricHavoc cheered 295 May 17, 2019
// LotanB gifted 3 subs May 17, 2019
// ClintonRocksmith cheered 100 June 14, 2019
// ClintonRocksmith cheered 3000 June 14, 2019
// ChrisNTR cheered 100 October 18, 2019
// codingwithluce cheered 200 October 18, 2019
// chicken_wing subscribed for 5 months December 20th, 2019
// MortelBox subscribed for 11 months December 20th, 2019
// AndrewSheley subscribed for 3 months December 20th, 2019
// ClintonRocksmith subscribed for 6 months December 20th, 2019
// alexhedley8 subscribed for 8 months December 20th, 2019


namespace Hanselman.Functions
{
    public static class TimerFunctions
    {

        [FunctionName(nameof(GetBlogFeed))]
        public static HttpResponseMessage GetBlogFeed(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            [Blob("hanselman/blog.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inBlob,
            ILogger log)
        {
            return BlobHelpers.BlobToHttpResponseMessage(inBlob, log, "blog");
        }


        [FunctionName(nameof(GetBlogLastUpdate))]
        public static HttpResponseMessage GetBlogLastUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            [Blob("hanselman/blog-lastupdate.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inBlob,
            ILogger log)
        {
            return BlobHelpers.BlobToHttpResponseMessage(inBlob, log, "blog");
        }

        

        [FunctionName(nameof(BlogUpdate))]
        public static async Task BlogUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
            [Blob("hanselman/blog.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outBlogBlob,
            [Blob("hanselman/blog.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inBlogBlob,
            [Blob("hanselman/blog-lastupdate.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outLastUpdateBlog,
            [Blob("hanselman/blog-lastupdate.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inLastUpdateBlog,
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

                var pubDate = blogItems.FirstOrDefault()?.PublishDate;
                if (TimeHelpers.CheckIfNewEntry(outLastUpdateBlog, inLastUpdateBlog, log, pubDate))
                {
                    //Send push notification
                    log.LogInformation("Parsing blog feed.");
                }

                var oldBlogs = BlobHelpers.BlobToItems<FeedItem>(inBlogBlob, log, "hanselmanblog");

                //Go through the old blogs and add them into the new blogs
                if (oldBlogs != null)
                {
                    foreach (var blog in oldBlogs)
                    {
                        //if the blogItems already has it 
                        if (blogItems.Any(b => b.Id == blog.Id))
                            continue;

                        // add blog and then max at 30 :)
                        blogItems.Add(blog);

                        if (blogItems.Count >= 30)
                            break;
                    }
                }

                var json = JsonConvert.SerializeObject(blogItems, Formatting.None);

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
