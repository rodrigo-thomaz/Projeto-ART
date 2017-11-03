namespace ART.Infra.CrossCutting.MQ
{
    public interface IMQSettings
    {
        #region Properties

        string BrokerHost
        {
            get;
        }

        int BrokerPort
        {
            get;
        }

        string BrokerPwd
        {
            get;
        }

        string BrokerUser
        {
            get;
        }

        string BrokerVirtualHost
        {
            get;
        }

        int RpcClientTimeOutMilliSeconds
        {
            get;
        }

        #endregion Properties

        #region Methods

        void Initialize();

        #endregion Methods
    }
}