using System;

namespace Elements.APNG.Serverless.Services.Extensions
{
    public static class DateTimeExtensions
    {
        public static double CalculateMinutesToUtcEndOfDay(this DateTime dateTime) 
        {
            return DateTime.UtcNow.Date.AddDays(1).AddTicks(-1).Subtract(dateTime).TotalMinutes;
        }

        public static DateTime EndOfDay(this DateTime dateTime)
        {
            return dateTime.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        public static string ToUSDateFormat(this DateTime dateTime)
        {
            return dateTime.ToString("MM/dd/yyyy");
        }
    }
}
