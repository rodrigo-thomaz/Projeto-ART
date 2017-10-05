namespace ART.Domotica.Repository.Entities
{
    using System;

    public class HardwaresInApplication
    {
        #region Properties

        public Application Application
        {
            get; set;
        }

        public Guid ApplicationId
        {
            get; set;
        }

        public HardwareBase HardwareBase
        {
            get; set;
        }

        public Guid HardwareBaseId
        {
            get; set;
        }

        #endregion Properties
    }
}