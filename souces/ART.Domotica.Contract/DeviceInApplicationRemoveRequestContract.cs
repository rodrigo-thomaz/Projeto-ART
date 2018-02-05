namespace ART.Domotica.Contract
{
    using System;

    public class DeviceInApplicationRemoveRequestContract
    {
        #region Properties

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceId
        {
            get; set;
        }

        #endregion Properties
    }
}