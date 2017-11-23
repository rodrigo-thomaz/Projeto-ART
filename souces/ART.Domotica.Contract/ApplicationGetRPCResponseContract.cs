namespace ART.Domotica.Contract
{
    using System;

    public class ApplicationGetRPCResponseContract
    {
        #region Properties

        public Guid ApplicationId
        {
            get; set;
        }

        public string BrokerApplicationTopic
        {
            get; set;
        }

        #endregion Properties
    }
}