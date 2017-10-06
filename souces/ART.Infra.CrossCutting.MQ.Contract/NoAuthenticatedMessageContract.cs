namespace ART.Infra.CrossCutting.MQ.Contract
{
    public class NoAuthenticatedMessageContract
    {
        #region Properties

        public string SouceMQSession
        {
            get; set;
        }

        #endregion Properties
    }

    public class NoAuthenticatedMessageContract<TContract> : NoAuthenticatedMessageContract
    {
        #region Properties

        public TContract Contract
        {
            get; set;
        }

        #endregion Properties
    }
}