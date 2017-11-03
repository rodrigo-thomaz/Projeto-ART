namespace ART.Infra.CrossCutting.MQ
{
    public interface IMQSettings
    {
        string BrokerHost { get; }
        int BrokerPort { get; }
        string BrokerPwd { get; }
        string BrokerUser { get; }
        string BrokerVirtualHost { get; }

        int RpcClientTimeOutMilliSeconds { get; }

        void Initialize();
    }
}