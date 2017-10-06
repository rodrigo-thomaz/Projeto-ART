using System;

namespace ART.Infra.CrossCutting.MQ
{
    public class AuthenticatedMessageContract
    {
        #region Properties

        public string SouceMQSession
        {
            get; set;
        }

        public Guid ApplicationUserId { get; set; }

        #endregion Properties
    }

    public class AuthenticatedMessageContract<TContract> : AuthenticatedMessageContract
    {
        #region Properties

        public TContract Contract
        {
            get; set;
        }

        #endregion Properties
    }    
}