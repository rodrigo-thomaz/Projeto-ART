using System;

namespace ART.Domotica.Contract
{
    public class ApplicationGetRPCResponseContract
    {
        #region Properties

        public ApplicationMQGetRPCResponseContract ApplicationMQ
        {
            get; set;
        }

        public Guid ApplicationId
        {
            get; set;
        }

        #endregion Properties
    }
}