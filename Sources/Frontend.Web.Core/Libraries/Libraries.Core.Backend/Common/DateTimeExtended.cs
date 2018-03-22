using System;

namespace Libraries.Core.Backend.Common
{
    public static class DateTimeExtended
    {
        public static DateTime PruningMilisecond(this DateTime sender)
        {
            var valueUtc = sender.ToUniversalTime();
            return new DateTime(valueUtc.Year, valueUtc.Month, valueUtc.Day, valueUtc.Hour, valueUtc.Minute,
                valueUtc.Second);
        }
    }
}
