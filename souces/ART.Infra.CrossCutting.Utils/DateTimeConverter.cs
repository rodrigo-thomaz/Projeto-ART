namespace ART.Infra.CrossCutting.Utils
{
    using System;

    public static class DateTimeConverter
    {
        #region Methods

        public static long ToUniversalTimestamp(DateTime value)
        {
            long epoch = (value.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return epoch;
        }

        #endregion Methods
    }
}