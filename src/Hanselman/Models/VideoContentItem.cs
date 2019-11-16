using System;

namespace Hanselman.Models
{
    public class VideoContentItem
    {
        public long FileSize { get; set; }
        public TimeSpan Duration { get; set; }
        public string? Url { get; set; }
    }
}