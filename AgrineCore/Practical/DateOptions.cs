using System;
using System.Globalization;

namespace AgrineCore.Practical
{
    public static class DateOptions
    {
        private static readonly PersianCalendar persianCalendar = new PersianCalendar();
        private static readonly HijriCalendar hijriCalendar = new HijriCalendar();

        // Convert Gregorian DateTime to Persian date string yyyy/MM/dd
        public static string GregorianToPersian(DateTime date)
        {
            return string.Format("{0:0000}/{1:00}/{2:00}",
                persianCalendar.GetYear(date),
                persianCalendar.GetMonth(date),
                persianCalendar.GetDayOfMonth(date));
        }

        // Convert Persian date string yyyy/MM/dd to Gregorian DateTime
        public static DateTime? PersianToGregorian(string persianDate)
        {
            try
            {
                var parts = persianDate.Split('/');
                if (parts.Length != 3) return null;

                int year = int.Parse(parts[0]);
                int month = int.Parse(parts[1]);
                int day = int.Parse(parts[2]);

                return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            catch
            {
                return null;
            }
        }

        // Convert Gregorian DateTime to Hijri (Islamic) date string yyyy/MM/dd
        public static string GregorianToHijri(DateTime date)
        {
            return string.Format("{0:0000}/{1:00}/{2:00}",
                hijriCalendar.GetYear(date),
                hijriCalendar.GetMonth(date),
                hijriCalendar.GetDayOfMonth(date));
        }

        // Convert Hijri date string yyyy/MM/dd to Gregorian DateTime
        public static DateTime? HijriToGregorian(string hijriDate)
        {
            try
            {
                var parts = hijriDate.Split('/');
                if (parts.Length != 3) return null;

                int year = int.Parse(parts[0]);
                int month = int.Parse(parts[1]);
                int day = int.Parse(parts[2]);

                return hijriCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            catch
            {
                return null;
            }
        }

        // Get Persian day of week name for given DateTime
        public static string GetPersianDayOfWeek(DateTime date)
        {
            var dayOfWeek = persianCalendar.GetDayOfWeek(date);
            switch (dayOfWeek)
            {
                case DayOfWeek.Saturday: return "شنبه";
                case DayOfWeek.Sunday: return "یکشنبه";
                case DayOfWeek.Monday: return "دوشنبه";
                case DayOfWeek.Tuesday: return "سه‌شنبه";
                case DayOfWeek.Wednesday: return "چهارشنبه";
                case DayOfWeek.Thursday: return "پنجشنبه";
                case DayOfWeek.Friday: return "جمعه";
                default: return "";
            }
        }

        // Get Persian month name for given DateTime
        public static string GetPersianMonthName(DateTime date)
        {
            switch (persianCalendar.GetMonth(date))
            {
                case 1: return "فروردین";
                case 2: return "اردیبهشت";
                case 3: return "خرداد";
                case 4: return "تیر";
                case 5: return "مرداد";
                case 6: return "شهریور";
                case 7: return "مهر";
                case 8: return "آبان";
                case 9: return "آذر";
                case 10: return "دی";
                case 11: return "بهمن";
                case 12: return "اسفند";
                default: return "";
            }
        }

        // Calculate difference in days between two Gregorian dates
        public static int DaysDifference(DateTime date1, DateTime date2)
        {
            return (date2.Date - date1.Date).Days;
        }

        // Format Persian DateTime in a readable form: e.g. "شنبه 01 فروردین 1400"
        public static string FormatPersianDate(DateTime date)
        {
            return $"{GetPersianDayOfWeek(date)} {persianCalendar.GetDayOfMonth(date):00} {GetPersianMonthName(date)} {persianCalendar.GetYear(date)}";
        }
    }
}
