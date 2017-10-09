using System;

namespace ART.Infra.CrossCutting.Utils
{
    public static class DateTimeConverter
    {
        public static long ToUniversalTimestamp(DateTime value)
        {
            long epoch = (value.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return epoch;
        }
    }
}
