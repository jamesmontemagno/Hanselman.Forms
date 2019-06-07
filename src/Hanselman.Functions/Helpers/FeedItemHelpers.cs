using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Hanselman.Models;

namespace Hanselman.Functions
{
    internal static class FeedItemHelpers
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
                        FirstImage = ((string)item.Element("description")).ExtractImage(),
                        Link = (string)item.Element("link"),
                        PublishDate = (string)item.Element("pubDate"),
                        Category = (string)item.Element("category"),
                        Id = id++
                    }).ToList();
        }

        internal static List<PodcastEpisode> ParsePodcastFeed(string rss)
        {            var xdoc = XDocument.Parse(rss);
            var id = 0;
            return (from item in xdoc.Descendants("item")
                    let enclosure = item.Element("enclosure")
                    where enclosure != null
                    select new PodcastEpisode
                    {
                        Title = (string)item.Element("title") ?? "",
                        Description = (string)item.Element("description") ?? "",
                        EpisodeUrl = (string)item.Element("link") ?? "",
                        Date = (string)item.Element("pubDate") ?? "",
                        Explicit = (string)item.Element(ItunesExtensions.Namespace + "explicit") ?? "",
                        Mp3Url = (string)enclosure.Attribute("url") ?? "",
                        ArtworkUrl = item.Element(ItunesExtensions.Namespace + "image")?.Attribute("href")?.Value as string ?? "",
                        Duration = (string)item.Element(ItunesExtensions.Namespace + "duration") ?? "",
                        EpisodeNumber = (string)item.Element(ItunesExtensions.Namespace + "episode") ?? "",
                        Id = id++
                    }).ToList();
        }

        public static class ItunesExtensions
        {
            private readonly static XNamespace ns = XNamespace.Get(@"http://www.itunes.com/dtds/podcast-1.0.dtd");
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

        internal static string ExtractImage(this string description)
        {
            var regx = new Regex("https://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?.(?:jpg|bmp|gif|png)", RegexOptions.IgnoreCase);

            var matches = regx.Matches(description);

            string firstImage;
            if (matches.Count == 0)
                firstImage = ScottHead;
            else
                firstImage = matches[0].Value;

            return firstImage;
        }

        static string ScottHead => "http://www.hanselman.com/images/photo-scott-tall.jpg";
    }
}
