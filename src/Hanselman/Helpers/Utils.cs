using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Humanizer;

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
                var time = date.ToShortTimeString();
                var monthDay = CultureInfo.CurrentCulture.DateTimeFormat.MonthDayPattern.Replace("MMMM", "MMM");
                // Twitter: 10:56 AM · Mar 7, 2019
                return $"{time} · {date.ToString($"{monthDay}, yyyy")}";
            }
        }
    }
}
