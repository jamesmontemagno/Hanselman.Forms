using Newtonsoft.Json;
using System;

namespace Hanselman
{
    public class Tweet
    {
        public Tweet()
        {
        }


        public ulong StatusID { get; set; }

        public string ScreenName { get; set; }

        public string Text { get; set; }

        [JsonIgnore]
        public string Date => CreatedAt.ToString("g");
        [JsonIgnore]
        public string RTCount => CurrentUserRetweet == 0 ? string.Empty : CurrentUserRetweet + " RT";

        public string Image { get; set; }

        public DateTime CreatedAt
        {
            get;
            set;
        }

        public ulong CurrentUserRetweet
        {
            get;
            set;
        }
    }
}

