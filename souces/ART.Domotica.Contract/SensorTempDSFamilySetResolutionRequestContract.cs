namespace ART.Domotica.Contract
{
    using ART.Domotica.Enums;
    using System;

    public class SensorTempDSFamilySetResolutionRequestContract
    {
        #region Properties

        public Guid SensorTempDSFamilyId
        {
            get; set;
        }

        public byte SensorTempDSFamilyResolutionId
        {
            get; set;
        }

        public SensorDatasheetEnum SensorDatasheetId
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