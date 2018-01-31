namespace ART.Domotica.IoTContract
{
    using System;

    public class SetOrdinationRequestIoTContract
    {
        #region Properties

        public Guid SensorId
        {
            get; set;
        }

        public short Ordination
        {
            get; set;
        }

        #endregion Properties
    }
}