using System;

namespace ART.Domotica.Contract
{
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