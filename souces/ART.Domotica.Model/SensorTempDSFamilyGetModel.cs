namespace ART.Domotica.Model
{
    using ART.Domotica.Enums;
    using System;

    public class SensorTempDSFamilyGetModel
    {
        #region Properties

        public Guid SensorTempDSFamilyId
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

        public byte SensorTempDSFamilyResolutionId
        {
            get; set;
        }


        #endregion Properties
    }
}