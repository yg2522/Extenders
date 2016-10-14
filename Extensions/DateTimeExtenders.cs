using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Utility.Extenders
{
    public static class DateTimeExtenders
    {
        public static DateTime ConvertToTimeZone(this DateTime originaltime, TimezoneEnum timezoneid)
        {
            return originaltime.ConvertToTimeZone(timezoneid.GetDescription());
        }

        public static DateTime ConvertToTimeZone(this DateTime originaltime, string timezoneid)
        {
            try
            {
                TimeZone timezone = TimeZone.CurrentTimeZone;
                DateTime lastlogin = timezone.ToUniversalTime(originaltime);
                TimeZoneInfo timezoneinfo = null;

                if (string.IsNullOrEmpty(timezoneid))
                    return originaltime;
                else
                {
                    timezoneinfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneid);
                    return TimeZoneInfo.ConvertTimeFromUtc(lastlogin, timezoneinfo);
                }
            }
            catch
            {
                return originaltime;
            }
        }
    }
}