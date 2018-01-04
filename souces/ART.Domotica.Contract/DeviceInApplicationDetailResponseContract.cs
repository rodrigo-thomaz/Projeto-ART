namespace ART.Domotica.Contract
{
    using System;

    public class DeviceInApplicationDetailResponseContract
    {
        #region Properties

        public Guid ApplicationId
        {
            get; set;
        }

        public string ApplicationTopic
        {
            get; set;
        }

        #endregion Properties
    }
}