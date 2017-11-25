namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceSetLabelRequestContract
    {
        #region Properties

        public Guid DeviceId
        {
            get; set;
        }

        public string Label
        {
            get; set;
        }

        #endregion Properties
    }
}