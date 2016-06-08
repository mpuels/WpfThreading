using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfThreadingTests.Reports
{
    /// <summary>
    /// Stellt statische Methoden zum Einlesen von Zeitstempeln bereit.
    /// </summary>
    public static class Date
    {
        private const string dateformat = "yyyy-MM-dd";
        private const string datetimeformat = "yyyy-MM-dd HH:mm:ss";

        private static readonly string[] formats =
            new string[] { dateformat, datetimeformat };

        private static readonly CultureInfo cultureInfoNeutral =
            new CultureInfo(string.Empty);

        public static DateTime ParseExact(string timestamp)
        {            
            return DateTime.ParseExact(timestamp, formats, cultureInfoNeutral,
                DateTimeStyles.None);
        }

        public static DateTime? ParseIfNotNull(string timestamp)
        {
            if (timestamp == null)
            {
                return null;
            }
            else
            {
                return ParseExact(timestamp);
            }
        }
    }
}
