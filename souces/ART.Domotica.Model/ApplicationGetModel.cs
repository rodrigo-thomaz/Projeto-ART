namespace ART.Domotica.Model
{
    using System;

    public class ApplicationGetModel
    {
        #region Properties

        public Guid ApplicationId
        {
            get; set;
        }

        public string BrokerApplicationTopic { get; set; }

        #endregion Properties
    }
}