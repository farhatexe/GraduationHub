using System;
using System.Globalization;
using System.Reflection.Emit;

namespace GraduationHub.Web.Infrastructure
{
    public static class FormatExtensions
    {
        public static string GetDateSuffix(this DateTime value)
        {
            // Get day...
            var day = value.Day;

            // Get day modulo...
            var dayModulo = day % 10;

            // Convert day to string...
            var suffix = day.ToString(CultureInfo.InvariantCulture);

            // Combine day with correct suffix...
            suffix += (day == 11 || day == 12 || day == 13) ? "th" :
                (dayModulo == 1) ? "st" :
                (dayModulo == 2) ? "nd" :
                (dayModulo == 3) ? "rd" :
                "th";

            // Return result...
            return suffix;
        }

        public static string ToImportantDate(this DateTime value)
        {
            string suffix = GetDateSuffix(value);

            return string.Format("{0} {1}", value.ToString("MMMM"), suffix);



        }
    }
}