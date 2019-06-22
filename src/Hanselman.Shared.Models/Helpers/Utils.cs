using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Humanizer;

// avagadavagam cheered 1000 March 8, 2019
//chadqueen cheered 1000 on March 15th, 2019

namespace Hanselman.Helpers
{
    public static class DateTimeUtils
    {
        // Twitter: 10:56 AM · Mar 7, 2019
        public static string TwitterHumanize(this DateTime date) => 
            date.Date == DateTime.Today.Date ? date.Humanize() : date.TwitterDateTime();

        public static string TwitterDateTime(this DateTime date, CultureInfo culture = null)
        {
            if (culture == null)
                culture = CultureInfo.CurrentCulture;

            var regex = new Regex("dddd[,]{0,1}");
            var shortDatePattern = regex.Replace(culture.DateTimeFormat.LongDatePattern.Replace("MMMM", "MMM"), string.Empty).Trim();
            return date.ToString($"{culture.DateTimeFormat.ShortTimePattern} · {shortDatePattern}", culture);
        }

        public static string PodcastEpisodeHumanize(this DateTimeOffset date) =>
            date.Date == DateTime.Today.Date ? date.Humanize() : date.PodcastEpisodeDateTime();

        public static string PodcastEpisodeDateTime(this DateTimeOffset date, CultureInfo culture = null)
        {
            if (culture == null)
                culture = CultureInfo.CurrentCulture;

            var regex = new Regex("dddd[,]{0,1}");
            var shortDatePattern = regex.Replace(culture.DateTimeFormat.LongDatePattern.Replace("MMMM", "MMM"), string.Empty).Trim();
            return date.ToString($"{shortDatePattern}", culture);
        }
    }
}
