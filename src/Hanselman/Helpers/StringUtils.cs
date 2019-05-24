using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Hanselman.Helpers
{
    public static class StringUtils
    {

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
