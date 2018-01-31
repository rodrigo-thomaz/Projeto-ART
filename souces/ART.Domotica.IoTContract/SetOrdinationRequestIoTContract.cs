namespace ART.Domotica.IoTContract
{
    using System;

    public class SetOrdinationRequestIoTContract
    {
        #region Properties

        public short Ordination
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