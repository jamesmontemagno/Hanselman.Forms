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
using System.Collections.Generic;
using Hanselman.Models;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using Azure.AI.TextAnalytics;

// March 27th 2020
// Sushinateur cheered 400 bits
// LachlanWGordon subscribed for 6th month
// alessio_ri subscribed for 9th month
// MortelBox subscribed for 14th month
// KymPhillpotts subscribed for 15th month

namespace Hanselman.Functions.Triggers
{
    public static class TwitterFunctions
    {
        [FunctionName(nameof(GetTweetSentiment))]
        public static HttpResponseMessage GetTweetSentiment(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            [Blob("hanselman/twitter-sentiment.json", FileAccess.Read, Connection = "AzureWebJobsStorage")]Stream inBlob,
            ILogger log)
        {
            return BlobHelpers.BlobToHttpResponseMessage(inBlob, log, "tweets-sentiment");
        }

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
            [Blob("hanselman/twitter-sentiment.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outSentiment,
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

                var document = GetSentimentOnTweets(tweetsRaw);
                if (document != null)
                {
                    using (var writer = new StreamWriter(outSentiment))
                    {
                        json = JsonConvert.SerializeObject(new Shared.Models.TweetSentiment
                        {
                            Negative = document.ConfidenceScores.Negative,
                            Neutral = document.ConfidenceScores.Neutral,
                            Positive = document.ConfidenceScores.Positive,
                            Overall = document.Sentiment.ToString()
                        });
                        writer.Write(json);
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Unable to get twitter feed");
            }
        }


        static DocumentSentiment GetSentimentOnTweets(List<Tweet> tweets)
        {
            var analyticsKey = Environment.GetEnvironmentVariable("TEXT_ANALYTICS_KEY");
            var analyticsEndpoint = Environment.GetEnvironmentVariable("TEXT_ANALYTICS_ENDPOINT");
            var credentials = new TextAnalyticsApiKeyCredential(analyticsKey);
            var endpoint = new Uri(analyticsEndpoint);

            var client = new TextAnalyticsClient(endpoint, credentials);

            //only tweets from today.
            var todayTweets = tweets.Where(t => t.ScreenName == "shanselman" &&
                t.CreatedAt > DateTime.UtcNow.AddDays(-1));

            var count = todayTweets.Count();

            var builder = new StringBuilder();
            foreach (var tweet in todayTweets)
            {
                builder.Append(SanitizeTweet(tweet.Text).Replace("RT", string.Empty));
                builder.Append(" ");
            }

            try
            {
                var textToAnalyze = builder.ToString();
                var documentSentiment = client.AnalyzeSentiment(textToAnalyze);

                var sentiment = documentSentiment.Value;
                Console.WriteLine($"Sentiment: {sentiment.Sentiment}");
                Console.WriteLine($"Negative: {sentiment.ConfidenceScores.Negative}" +
                    $"Neutral: {sentiment.ConfidenceScores.Neutral}" +
                    $"Positive: {sentiment.ConfidenceScores.Positive}");

                return sentiment;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to get sentiment");
                return null;
            }

        }

        static string SanitizeTweet(string raw) =>
            Regex.Replace(raw, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", "").ToString().Replace("\n", " ");
    }
}
