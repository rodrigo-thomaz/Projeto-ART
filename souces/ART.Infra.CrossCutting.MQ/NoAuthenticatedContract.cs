namespace ART.Infra.CrossCutting.MQ
{
    public class NoAuthenticatedContract
    {
        #region Properties

        public string SouceMQSession
        {
            get; set;
        }

        #endregion Properties
    }

    public class NoAuthenticatedContract<TContract>
    {
        #region Properties

        public TContract Contract
        {
            get; set;
        }

        public string SouceMQSession
        {
            get; set;
        }

        #endregion Properties
    }    
}