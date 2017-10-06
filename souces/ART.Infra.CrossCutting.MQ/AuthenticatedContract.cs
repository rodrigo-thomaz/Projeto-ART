namespace ART.Infra.CrossCutting.MQ
{
    using System;

    public class AuthenticatedContract : NoAuthenticatedContract
    {
        #region Properties

        public Guid ApplicationUserId
        {
            get; set;
        }

        #endregion Properties
    }

    public class AuthenticatedContract<TContract> : NoAuthenticatedContract<TContract>
    {
        #region Properties

        public Guid ApplicationUserId
        {
            get; set;
        }

        #endregion Properties
    }
}