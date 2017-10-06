namespace ART.Domotica.Contract
{
    using System;

    public class DSFamilyTempSensorGetAllContract
    {
        #region Properties

        public Guid ApplicationId
        {
            get; set;
        }

        public string Session
        {
            get; set;
        }

        #endregion Properties
    }
}