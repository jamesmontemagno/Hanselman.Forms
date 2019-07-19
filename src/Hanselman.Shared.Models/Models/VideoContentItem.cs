using System;
using System.Collections.Generic;
using System.Text;

namespace Hanselman.Models
{
    public class VideoContentItem
    {
        public long FileSize { get; set; }
        public TimeSpan Duration { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
    }
}
