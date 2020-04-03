using System;
namespace Hanselman.Shared.Models
{
    public class TweetSentiment
    {
        public string Overall { get; set; }
        public double Positive { get; set; }
        public double Neutral { get; set; }
        public double Negative { get; set; }
    }
}
