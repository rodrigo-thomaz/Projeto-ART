using ART.Infra.CrossCutting.Setting;

namespace ART.Infra.CrossCutting.MQ
{
    public class MQSettings : IMQSettings
    {
        private readonly ISettingManager _settingsManager;

        private string _brokerHost;
        private string _brokerVirtualHost;
        private int _brokerPort;
        private string _brokerUser;
        private string _brokerPwd;

        private int _rpcClientTimeOutMilliSeconds;

        public MQSettings(ISettingManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public void Initialize()
        {
            _brokerHost = _settingsManager.GetValue<string>(MQSettingsConstants.BrokerHostSettingsKey);
            _brokerVirtualHost = _settingsManager.GetValue<string>(MQSettingsConstants.BrokerVirtualHostSettingsKey);
            _brokerPort = _settingsManager.GetValue<int>(MQSettingsConstants.BrokerPortSettingsKey);
            _brokerUser = _settingsManager.GetValue<string>(MQSettingsConstants.BrokerUserSettingsKey);
            _brokerPwd = _settingsManager.GetValue<string>(MQSettingsConstants.BrokerPwdSettingsKey);

            _rpcClientTimeOutMilliSeconds = _settingsManager.GetValue<int>(MQSettingsConstants.RpcClientTimeOutMilliSecondsSettingsKey);
        }

        public string BrokerHost { get { return _brokerHost; } }
        public string BrokerVirtualHost { get { return _brokerVirtualHost; } }
        public int BrokerPort { get { return _brokerPort; } }
        public string BrokerUser { get { return _brokerUser; } }
        public string BrokerPwd { get { return _brokerPwd; } }
        public int RpcClientTimeOutMilliSeconds { get { return _rpcClientTimeOutMilliSeconds; } }
    }
}
