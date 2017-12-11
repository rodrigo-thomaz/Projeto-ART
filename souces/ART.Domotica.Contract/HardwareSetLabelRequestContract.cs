namespace ART.Domotica.Contract
{
    using System;

    public class HardwareSetLabelRequestContract
    {
        #region Properties

        public Guid HardwareId
        {
            get; set;
        }

        public string Label
        {
            get; set;
        }

        #endregion Properties
    }
}