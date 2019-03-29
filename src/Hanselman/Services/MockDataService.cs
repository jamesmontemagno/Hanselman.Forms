﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Hanselman.Interfaces;
using Hanselman.Models;
using Hanselman.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly:Dependency(typeof(MockDataService))]

namespace Hanselman.Services
{
    public class MockDataService : IDataService
    {
        public MockDataService()
        {

        }

        public Task<IEnumerable<PodcastEpisode>> GetPodcastEpisodesAsync()
        {
            var episodes = new List<PodcastEpisode>();

            for (var i = 0; i < 4; i++)
            {
                episodes.Add(new PodcastEpisode
                {
                    Title = "Inside a Tribe of Hackers with cryptographer Marcus J Carey",
                    ArtworkUrl = "https://images.hanselminutes.com/images/676.jpg",
                    Date = "Mar 21 2019",
                    Description = "Marcus is renowned in the cybersecurity industry and has spent his more than 20-year career working in penetration testing, incident response, and digital forensics with federal agencies such as NSA, DC3, DIA, and DARPA. He started his career in cryptography in the U.S. Navy and holds a Master’s degree in Network Security from Capitol College. Scott and Marcus talk about his new book \"Tribe of Hackers\" that he wrote with Jennifer Jin.",
                    Duration = "34:54",
                    EpisodeNumber = "676",
                    EpisodeUrl = "https://hanselminutes.com/676/inside-a-tribe-of-hackers-with-cryptographer-marcus-j-carey",
                    Explicit = false,
                    Mp3Url = ""
                });

                episodes.Add(new PodcastEpisode
                {
                    Title = "A love letter to language (and programming) with Eva Ferreira",
                    ArtworkUrl = "https://images.hanselminutes.com/images/675.jpg",
                    Date = "Mar 14 2019",
                    Description = "Eva Ferreira organizes the non-profit CSSConf Argentina and teaches at Universidad Tecnológica Nacional in Argentina. She and Scott talk about learning and teaching on the web when the students' native language isn't English. What's the most effective way to teach an inclusive web?",
                    Duration = "31:16",
                    EpisodeNumber = "675",
                    EpisodeUrl = "https://hanselminutes.com/675/a-love-letter-to-language-and-programming-with-eva-ferreira",
                    Explicit = false,
                    Mp3Url = ""
                });

                episodes.Add(new PodcastEpisode
                {
                    Title = "How galaxies evolve with Dr. Molly Peeples",
                    ArtworkUrl = "https://images.hanselminutes.com/images/674.jpg",
                    Date = "Mar 07 2019",
                    Description = "Dr. Molly Peeples is an Aura Assistant Astronomer at the Space Telescope Science Institute in Baltimore, Maryland. She received her B.S. in Physics from MIT and went on to complete her MS and PhD in Astronomy at Ohio State University. Molly works at the Space Telescope Science Institute. Today she teaches Scott about the circumgalactic medium and her need for more and more compute power!",
                    Duration = "31:26",
                    EpisodeNumber = "674",
                    EpisodeUrl = "https://hanselminutes.com/674/how-galaxies-evolve-with-dr-molly-peeples",
                    Explicit = false,
                    Mp3Url = ""
                });
            }

            return Task.FromResult((IEnumerable<PodcastEpisode>)episodes);
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
                Description = "Hanselminutes is Fresh Air for Developers. A weekly commute-time podcast that promotes fresh technology and fresh voices.",
                PodcastServices = new List<PodcastService>
                {
                    new PodcastService
                    {
                        Title = "Apple Podcasts",
                        Url = "https://itunes.apple.com/us/podcast/hanselminutes/id117488860",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.iOS,
                            DevicePlatform.UWP
                        }
                    },
                    new PodcastService
                    {
                        Title = "Google Podcasts",
                        Url = "https://www.google.com/podcasts?feed=aHR0cHM6Ly9oYW5zZWxtaW51dGVzLmNvbS9zdWJzY3JpYmU%3D",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.Android
                        }
                    },
                    new PodcastService
                    {
                        Title = "Overcast",
                        Url = "https://overcast.fm/itunes117488860",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.iOS
                        }
                    },
                    new PodcastService
                    {
                        Title = "Pocket Casts",
                        Url = "https://pca.st/StbR",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.iOS,
                            DevicePlatform.Android,
                            DevicePlatform.UWP
                        }
                    },
                    new PodcastService
                    {
                        Title = "Spotify",
                        Url = "https://open.spotify.com/show/4SrTUZr1s5C4SJmUxDIUDc",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.iOS,
                            DevicePlatform.Android,
                            DevicePlatform.UWP
                        }
                    },
                    new PodcastService
                    {
                        Title = "Stitcher",
                        Url = "https://www.stitcher.com/podcast/hanselminutes",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.iOS,
                            DevicePlatform.Android,
                            DevicePlatform.UWP
                        }
                    },
                    new PodcastService
                    {
                        Title = "TuneIn",
                        Url = "https://tunein.com/podcasts/Technology-Podcasts/Hanselminutes-p244130/",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.iOS,
                            DevicePlatform.Android,
                            DevicePlatform.UWP
                        }
                    },
                    new PodcastService
                    {
                        Title = "Castro",
                        Url = "https://castro.fm/itunes/117488860",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.iOS
                        }
                    },
                }
            });
            podcasts.Add(new Podcast
            {
                Title = "Ratchet & The Geek",
                Art = "ratchet_full.jpg",
                Hosts = new List<Host> { hanselman },
                Category = "Comedy",
                FeedUrl = "http://feeds.feedburner.com/RatchetAndTheGeek?format=xml",
                WebsiteUrl = "http://www.ratchetandthegeek.com/",
                Description = "Ratchet and the Geek is a conversation between Social Media and Pop Culture blogger Luvvie Ajayi and Web Developer and Technologist Scott Hanselman. You can decide who is Ratchet and who is The Geek.",
                PodcastServices = new List<PodcastService>
                {
                    new PodcastService
                    {
                        Title = "Apple Podcasts",
                        Url = "https://itunes.apple.com/us/podcast/ratchet-and-the-geek/id573832936",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.iOS,
                            DevicePlatform.UWP
                        }
                    },
                    new PodcastService
                    {
                        Title = "Google Podcasts",
                        Url = "https://www.google.com/podcasts?feed=aHR0cHM6Ly9yc3Muc2ltcGxlY2FzdC5jb20vcG9kY2FzdHMvNTQ5Ni9yc3M%3D",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.Android
                        }
                    },
                    new PodcastService
                    {
                        Title = "Overcast",
                        Url = "https://overcast.fm/itunes573832936",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.iOS
                        }
                    },
                    new PodcastService
                    {
                        Title = "Pocket Casts",
                        Url = "https://pca.st/jdp4",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.iOS,
                            DevicePlatform.Android,
                            DevicePlatform.UWP
                        }
                    },
                    new PodcastService
                    {
                        Title = "Castro",
                        Url = "https://castro.fm/itunes/573832936",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.iOS
                        }
                    },
                }
            });
            podcasts.Add(new Podcast
            {
                Title = "This Developer's Life",
                Art = "tdl_full.jpg",
                Hosts = new List<Host> { hanselman },
                Category = "Tech News",
                FeedUrl = "http://feeds.feedburner.com/ThisDevelopersLife?format=xml",
                WebsiteUrl = "http://thisdeveloperslife.com/",
                Description = "Bringing a human slant to the tech industry",
                PodcastServices = new List<PodcastService>
                {
                    new PodcastService
                    {
                        Title = "Apple Podcasts",
                        Url = "http://itunes.apple.com/us/podcast/this-developers-life/id389727545",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.iOS, 
                            DevicePlatform.UWP
                        }
                    },
                    new PodcastService
                    {
                        Title = "Google Podcasts",
                        Url = "https://www.google.com/podcasts?feed=aHR0cDovL2ZlZWRzLmZlZWRidXJuZXIuY29tL3RoaXNkZXZlbG9wZXJzbGlmZQ%3D%3D",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.Android
                        }
                    },
                    new PodcastService
                    {
                        Title = "Overcast",
                        Url = "https://overcast.fm/itunes389727545",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.iOS
                        }
                    },
                    new PodcastService
                    {
                        Title = "Pocket Casts",
                        Url = "https://pca.st/JqSuBj",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.iOS,
                            DevicePlatform.Android,
                            DevicePlatform.UWP
                        }
                    },
                    new PodcastService
                    {
                        Title = "Castro",
                        Url = "https://castro.fm/itunes/389727545",
                        SupportedPlatforms = new List<DevicePlatform>
                        {
                            DevicePlatform.iOS
                        }
                    },
                }
            });
            return Task.FromResult((IEnumerable<Podcast>)podcasts);
        }
    }
}
