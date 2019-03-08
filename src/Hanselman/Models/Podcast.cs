using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;

namespace Hanselman.Models
{
    public class Podcast : ObservableObject
    {
        public string Title { get; set; }
        public string Art { get; set; }
        public string Description { get; set; }

        public string FeedUrl { get; set; }
        
        public List<Host> Hosts { get; set; }

        public string Category { get; set; }
        public string WebsiteUrl { get; set; }
        public string TwitterUrl { get; set; }
    }
}
