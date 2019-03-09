using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Humanizer;

// avagadavagam cheered 1000 March 8, 2019

namespace Hanselman.Helpers
{
    public static class Utils
    {
        public static string HumanizeTodayOnly(this DateTime date)
        {
            if (date.Date == DateTime.Today.Date)
                return date.Humanize();
            else
            {
                return date.ToHumanizeDate();
            }
        }

        public static string ToHumanizeDate(this DateTime date, CultureInfo culture = null)
        {
            if (culture == null)
                culture = CultureInfo.CurrentCulture;

            var regex = new Regex("dddd[,]{0,1}");
            var shortDatePattern = regex.Replace(culture.DateTimeFormat.LongDatePattern.Replace("MMMM", "MMM"), "").Trim();
            return date.ToString($"{culture.DateTimeFormat.ShortTimePattern} · {shortDatePattern}", culture);
        }
    }
}
