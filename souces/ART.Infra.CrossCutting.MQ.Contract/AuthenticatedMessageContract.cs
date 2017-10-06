namespace ART.Infra.CrossCutting.MQ.Contract
{
    using System;

    public class AuthenticatedMessageContract
    {
        #region Properties

        public Guid ApplicationUserId
        {
            get; set;
        }

        public string SouceMQSession
        {
            get; set;
        }

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