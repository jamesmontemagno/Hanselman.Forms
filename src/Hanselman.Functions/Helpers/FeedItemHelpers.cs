using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Hanselman.Models;

namespace Hanselman.Functions
{
    static class FeedItemHelpers
    {
        /// <summary>
        /// Parse the RSS Feed
        /// </summary>
        /// <param name="rss"></param>
        /// <returns></returns>
        internal static List<FeedItem> ParseBlogFeed(string rss)
        {
            var xdoc = XDocument.Parse(rss);
            var id = 0;

            return (from item in xdoc.Descendants("item")
                    select new FeedItem
                    {
                        Title = (string)item.Element("title"),
                        Caption = ((string)item.Element("description")).ExtractCaption(),
                        FirstImage = ((string)item.Element("description")).ExtractImage(isBlog: true),
                        Link = (string)item.Element("link"),
                        PublishDate = (string)item.Element("pubDate"),
                        Category = (string)item.Element("category"),
                        Id = (string)item.Element("guid"),
                    }).ToList();
        }

        internal static List<VideoFeedItem> ParseVideoFeed(string rss, string defaultImage)
        {
            var xdoc = XDocument.Parse(rss);
            var list = new List<VideoFeedItem>();
            foreach (var item in xdoc.Descendants("item"))
            {
                try
                {
                    var author = item.Element(ItunesExtensions.Namespace + "author")?.Value;
                    if(string.IsNullOrWhiteSpace(author))
                        author = item.Element(ItunesExtensions.Namespace + "summary")?.Value;

                    // only grab hanselman authors
                    if (!author?.ToLower()?.Contains("hanselman") ?? false)
                        continue;

                    var mediaGroup = item.Element(MediaExtensions.Namespace + "group");

                    var videoUrls = new List<VideoContentItem>();
                    if (mediaGroup != null)
                    {
                        foreach (var mediaUrl in mediaGroup.Elements())
                        {
                            videoUrls.Add(new VideoContentItem
                            {
                                Duration = TimeSpan.FromSeconds(Convert.ToInt32(mediaUrl.Attribute("duration")?.Value ?? "0")),
                                FileSize = long.Parse(mediaUrl.Attribute("fileSize").Value),
                                Url = mediaUrl.Attribute("url").Value,
                                Type = mediaUrl.Attribute("type").Value
                            });
                        }
                    }
                    else
                    {
                        var duration = item.Element(ItunesExtensions.Namespace + "duration")?.Value;
                        videoUrls.Add(new VideoContentItem
                        {
                            Duration = TimeSpan.FromSeconds(Convert.ToInt32(duration ?? "0")),
                            FileSize = long.Parse(item.Element("enclosure")?.Attribute("length")?.Value),
                            Url = item.Element("enclosure")?.Attribute("url")?.Value,
                            Type = item.Element("enclosure")?.Attribute("type")?.Value,
                        });
                    }

                    var videoFeedItem = new VideoFeedItem
                    {
                        Id = (string)item.Element("guid"),
                        VideoUrls = videoUrls.OrderByDescending(url => url.FileSize).ToList(),
                        Title = (string)item.Element("title"),
                        Description = item.Element(ItunesExtensions.Namespace + "summary")?.Value ?? (string)item.Element("description") ?? "",
                        Url = (string)item.Element("link"),
                        Date = (string)item.Element("pubDate"),
                        ThumbnailUrl = item.Element(MediaExtensions.Namespace + "thumbnail")?.Attribute("url")?.Value ?? defaultImage,
                        Duration = TimeSpan.FromSeconds(Convert.ToInt32(item.Element(ItunesExtensions.Namespace + "duration")?.Value ?? "0"))
                    };

                    var thumbs = item.Elements(MediaExtensions.Namespace + "thumbnail");
                    
                    // Find second largest thumb
                    if(thumbs?.Count() > 1)
                    {
                        var thumb = thumbs.ElementAt(thumbs.Count() - 1)?.Attribute("url")?.Value;
                        if(!string.IsNullOrWhiteSpace(thumb))
                            videoFeedItem.ThumbnailUrl = thumb;
                    }



                    list.Add(videoFeedItem);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Unable to parse rss for item");
                }
            }
            return list;
        }

        internal static List<PodcastEpisode> ParsePodcastFeed(string rss, string defaultImage)
        {
            var xdoc = XDocument.Parse(rss);
            var id = 0;
            return (from item in xdoc.Descendants("item")
                    let enclosure = item.Element("enclosure")
                    where enclosure != null
                    select new PodcastEpisode
                    {
                        Title = (string)item.Element("title") ?? "",
                        Description = (string)item.Element(ItunesExtensions.Namespace + "summary") ?? (string)item.Element("description") ?? "",
                        EpisodeUrl = (string)item.Element("link") ?? "",
                        Date = (string)item.Element("pubDate") ?? "",
                        Explicit = (string)item.Element(ItunesExtensions.Namespace + "explicit") ?? "",
                        Mp3Url = (string)enclosure.Attribute("url") ?? "",
                        ArtworkUrl = item.Element(ItunesExtensions.Namespace + "image")?.Attribute("href")?.Value as string ?? ((string)item.Element("description"))?.ExtractImage(defaultImage) ?? "",
                        Duration = (string)item.Element(ItunesExtensions.Namespace + "duration") ?? "",
                        EpisodeNumber = (string)item.Element(ItunesExtensions.Namespace + "episode") ?? "",
                        Id = (string)item.Element("guid")
                    }).ToList();
        }

        public static class MediaExtensions
        {
            static readonly XNamespace ns = XNamespace.Get(@"http://search.yahoo.com/mrss/");
            public static XNamespace Namespace => ns;

            public static XElement CustomElement(string name, string value)
            {
                return new XElement(ns + name, value);
            }

            public static XElement CustomElement(string name, params object[] objects)
            {
                return new XElement(ns + name, objects);
            }

            public static XElement CustomElement(string name, object value)
            {
                return new XElement(ns + name, value);
            }

        }

        public static class ItunesExtensions
        {
            static readonly XNamespace ns = XNamespace.Get(@"http://www.itunes.com/dtds/podcast-1.0.dtd");
            public static XNamespace Namespace => ns;

            public static XElement CustomElement(string name, string value)
            {
                return new XElement(ns + name, value);
            }

            public static XElement CustomElement(string name, params object[] objects)
            {
                return new XElement(ns + name, objects);
            }

            public static XElement CustomElement(string name, object value)
            {
                return new XElement(ns + name, value);
            }

        }



        internal static string ExtractCaption(this string caption)
        {
            //get rid of HTML tags
            caption = Regex.Replace(caption, "<[^>]*>", string.Empty);


            //get rid of multiple blank lines
            caption = Regex.Replace(caption, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

            return caption.Substring(0, Math.Min(caption.Length, 200)).Trim() + "...";            
        }

        internal static string ExtractImage(this string description, string defaultImage = ScottHead, bool isBlog = false)
        {
            var regx = new Regex("(https?)://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?.(?:jpg|bmp|gif|png)", RegexOptions.IgnoreCase);

            var matches = regx.Matches(description);

            string firstImage;
            if (matches.Count == 0 || (isBlog && !matches.Any(s => s.Value.ToLowerInvariant().Contains("hanselman.com"))))
            {
                if (isBlog)
                {
                    var random = new Random();
                    var next = random.Next(1, 4);
                    firstImage = $"https://hanselmanformsstorage.blob.core.windows.net/hanselman-public/hanselmanblog{next}.jpg";
                }
                else
                    firstImage = defaultImage;
            }
            else
                firstImage = matches[0].Value;

            return firstImage;
        }

        const string ScottHead = "http://www.hanselman.com/images/photo-scott-tall.jpg";
    }
}
