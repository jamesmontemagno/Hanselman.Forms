﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Hanselman.Models
{
    public class VideoContentItem
    {
        [JsonProperty("fs")]
        public long FileSize { get; set; }
        [JsonProperty("d")]
        public TimeSpan Duration { get; set; }
        [JsonProperty("u")]
        public string Url { get; set; }
        [JsonProperty("t")]
        public string Type { get; set; }
    }
}
