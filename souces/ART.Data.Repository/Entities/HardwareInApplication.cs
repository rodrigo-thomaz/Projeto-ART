namespace ART.Data.Repository.Entities
{
    using System;

    public class HardwareInApplication
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