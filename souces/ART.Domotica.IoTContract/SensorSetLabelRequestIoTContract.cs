namespace ART.Domotica.IoTContract
{
    using System;

    public class SensorSetLabelRequestIoTContract
    {
        #region Properties

        public string Label
        {
            get; set;
        }

        public Guid SensorId
        {
            get; set;
        }

        #endregion Properties
    }
}