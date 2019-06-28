using System;
using System.Collections.Generic;
using System.Globalization;
using System.Json;
using System.Net.Http;
using System.Text;
using Hanselman.Functions.Models;
using System.Threading.Tasks;
using Hanselman.Models;
using System.Linq;

namespace Hanselman.Functions
{
    public class TwitterHelpers
    {

        static async Task<string> GetAccessToken(HttpClient client)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token ");
            var customerInfo = Convert.ToBase64String(new UTF8Encoding()
                                      .GetBytes("ZTmEODUCChOhLXO4lnUCEbH2I" + ":" + "Y8z2Wouc5ckFb1a0wjUDT9KAI6DUat5tFNdmIkPLl8T4Nyaa2J"));
            request.Headers.Add("Authorization", "Basic " + customerInfo);
            request.Content = new StringContent("grant_type=client_credentials",
                                                    Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonValue.Parse(json);

            return result["access_token"];
        }


        public static async Task<List<Tweet>> GetTweets(HttpClient client)
        {
            var accessToken = await GetAccessToken(client);

            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get,
                $"https://api.twitter.com/1.1/statuses/user_timeline.json?count=100&screen_name=shanselman&trim_user=0&exclude_replies=1");

            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);

            var responseUserTimeLine = await client.SendAsync(requestUserTimeline);
            var json = await responseUserTimeLine.Content.ReadAsStringAsync();

            var tweetsRaw = TweetRaw.FromJson(json);

            return tweetsRaw.Select(t => new Tweet
            {
                StatusID = t?.RetweetedStatus?.User?.ScreenName == "shanselman" ? t.RetweetedStatus.IdStr : t.IdStr,
                ScreenName = t?.RetweetedStatus?.User?.ScreenName ?? t.User.ScreenName,
                Text = t.Text,
                RetweetCount = t.RetweetCount,
                FavoriteCount = t.FavoriteCount,
                CreatedAt = GetDate(t.CreatedAt, DateTime.MinValue),
                Image = t.RetweetedStatus != null && t.RetweetedStatus.User != null ?
                                     t.RetweetedStatus.User.ProfileImageUrlHttps.ToString() : (t.User.ScreenName == "shanselman" ? "scott159.png" : t.User.ProfileImageUrlHttps.ToString()),
                MediaUrl = t?.Entities?.Media?.FirstOrDefault()?.MediaUrlHttps?.AbsoluteUri
            }).ToList();
        }

        public static readonly string[] DateFormats = { "ddd MMM dd HH:mm:ss %zzzz yyyy",
                                                         "yyyy-MM-dd\\THH:mm:ss\\Z",
                                                         "yyyy-MM-dd HH:mm:ss",
                                                         "yyyy-MM-dd HH:mm"};

        public static DateTime GetDate(string date, DateTime defaultValue)
        {
            return string.IsNullOrWhiteSpace(date) ||
                !DateTime.TryParseExact(date,
                        DateFormats,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var result)
                    ? defaultValue
                    : result;
        }
    }
}
