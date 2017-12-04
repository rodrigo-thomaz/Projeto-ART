namespace ART.Domotica.Model
{
    using System;
    using System.Collections.Generic;

    public class ESPDeviceGetModel
    {
        #region Properties

        public int ChipId
        {
            get; set;
        }

        public long CreateDate
        {
            get; set;
        }

        public Guid DeviceId
        {
            get; set;
        }

        public Guid DeviceInApplicationId
        {
            get; set;
        }

        public DeviceNTPGetModel DeviceNTP
        {
            get; set;
        }

        public int FlashChipId
        {
            get; set;
        }

        public string Label
        {
            get; set;
        }

        public string MacAddress
        {
            get; set;
        }

        public List<SensorTempDSFamilyGetModel> Sensors
        {
            get; set;
        }

        #endregion Properties
    }
}