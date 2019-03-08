using System.Collections.Generic;
using System.Threading.Tasks;
using Hanselman.Interfaces;
using Hanselman.Models;
using Hanselman.Services;
using Xamarin.Forms;

[assembly:Dependency(typeof(MockDataService))]

namespace Hanselman.Services
{
    public class MockDataService : IDataService
    {
        public MockDataService()
        {

        }
        public Task<IEnumerable<Podcast>> GetPodcastsAsync()
        {
            var hanselman = new Host
            {
                Name = "Scott Hanselman"
            };

            var podcasts = new List<Podcast>();
            podcasts.Add(new Podcast
            {
                Title = "Hanselminutes",
                Art = "hm_full.jpg",
                Hosts = new List<Host> { hanselman },
                Category = "Tech News",
                FeedUrl = "https://rss.simplecast.com/podcasts/4669/rss",
                WebsiteUrl = "https://www.hanselminutes.com/",
                TwitterUrl = "https://twitter.com/hanselminutes",
                Description = "Hanselminutes is Fresh Air for Developers. A weekly commute-time podcast that promotes fresh technology and fresh voices."
            });
            podcasts.Add(new Podcast
            {
                Title = "Ratchet & The Geek",
                Art = "ratchet_full.jpg",
                Hosts = new List<Host> { hanselman },
                Category = "Comedy",
                FeedUrl = "http://feeds.feedburner.com/RatchetAndTheGeek?format=xml",
                WebsiteUrl = "http://www.ratchetandthegeek.com/",
                Description = "Ratchet and the Geek is a conversation between Social Media and Pop Culture blogger Luvvie Ajayi and Web Developer and Technologist Scott Hanselman. You can decide who is Ratchet and who is The Geek."
            });
            podcasts.Add(new Podcast
            {
                Title = "This Developer's Live",
                Art = "tdl_full.jpg",
                Hosts = new List<Host> { hanselman },
                Category = "Tech News",
                FeedUrl = "http://feeds.feedburner.com/ThisDevelopersLife?format=xml",
                WebsiteUrl = "http://thisdeveloperslife.com/",
                Description = "Bringing a human slant to the tech industry"
            });
            return Task.FromResult((IEnumerable<Podcast>)podcasts);
        }
    }
}
