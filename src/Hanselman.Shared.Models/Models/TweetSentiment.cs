using System;
namespace Hanselman.Shared.Models
{
    public class TweetSentiment
    {
        public string Overall { get; set; }
        public double Positive { get; set; }
        public double Neutral { get; set; }
        public double Negative { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public string PositivePercentage =>
            $"{(int)(Positive * 100)}%";


        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public string NeutralPercentage =>
            $"{(int)(Neutral * 100)}%";


        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public string NegativePercentage =>
            $"{(int)(Negative * 100)}%";
    }
}
