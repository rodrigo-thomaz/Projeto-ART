namespace ART.Domotica.Model
{
    using System;

    public class ThermometerDeviceGetListModel
    {
        #region Properties

        public long CreateDate
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        public bool InApplication
        {
            get; set;
        }

        public string Pin
        {
            get; set;
        }

        #endregion Properties
    }
}