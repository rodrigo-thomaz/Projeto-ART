namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class SensorTempDSFamilyGetModel
    {
        #region Properties

        public SensorDatasheetEnum SensorDatasheetId
        {
            get; set;
        }

        public Guid SensorTempDSFamilyId
        {
            get; set;
        }

        public byte SensorTempDSFamilyResolutionId
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
        {
            get; set;
        }

        #endregion Properties
    }
}