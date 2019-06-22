using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmHelpers;

namespace Hanselman.Models
{
    public class Podcast : ObservableObject
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Art { get; set; }
        public string Description { get; set; }

        public string FeedUrl { get; set; }

        public List<Host> Hosts { get; set; }

        public string HostsNames 
        {
            get
            {
                if (Hosts.Count == 0)
                    return string.Empty;

                if(Hosts.Count == 1)
                    return $"{Hosts.FirstOrDefault()?.Name ?? string.Empty}";

                return string.Join(", ", Hosts.Select(h => h.Name));
            }
        }

        public string Category { get; set; }
        public string WebsiteUrl { get; set; }
        public string TwitterUrl { get; set; }

        public List<PodcastService> PodcastServices { get; set; }
    }
}
