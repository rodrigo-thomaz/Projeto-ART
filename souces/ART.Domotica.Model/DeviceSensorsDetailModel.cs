using System;

namespace ART.Domotica.Model
{
    public class DeviceSensorsDetailModel
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