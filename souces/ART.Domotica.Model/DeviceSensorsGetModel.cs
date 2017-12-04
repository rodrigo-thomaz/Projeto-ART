namespace ART.Domotica.Model
{
    using System;

    public class DeviceSensorsGetModel
    {
        #region Properties

        public Guid DeviceSensorsId
        {
            get; set;
        }

        public int PublishIntervalInSeconds
        {
            get; set;
        }

        #endregion Properties
    }
}