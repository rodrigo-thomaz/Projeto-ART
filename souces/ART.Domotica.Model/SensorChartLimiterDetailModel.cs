namespace ART.Domotica.Model
{
    using System;

    public class SensorChartLimiterDetailModel
    {
        #region Properties

        public Guid Id
        {
            get; set;
        }

        public decimal Max
        {
            get; set;
        }

        public decimal Min
        {
            get; set;
        }

        #endregion Properties
    }
}