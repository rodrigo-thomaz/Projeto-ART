namespace ART.Domotica.Model
{
    using System;

    public class HardwareGetListModel
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

        public string Pin
        {
            get; set;
        }

        #endregion Properties
    }
}