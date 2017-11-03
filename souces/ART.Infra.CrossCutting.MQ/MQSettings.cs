namespace ART.Infra.CrossCutting.MQ
{
    using ART.Infra.CrossCutting.Setting;

    public class MQSettings : IMQSettings
    {
        #region Fields

        private readonly ISettingManager _settingsManager;

        private string _brokerHost;
        private int _brokerPort;
        private string _brokerPwd;
        private string _brokerUser;
        private string _brokerVirtualHost;
        private int _rpcClientTimeOutMilliSeconds;

        #endregion Fields

        #region Constructors

        public MQSettings(ISettingManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        #endregion Constructors

        #region Properties

        public string BrokerHost
        {
            get { return _brokerHost; }
        }

        public int BrokerPort
        {
            get { return _brokerPort; }
        }

        public string BrokerPwd
        {
            get { return _brokerPwd; }
        }

        public string BrokerUser
        {
            get { return _brokerUser; }
        }

        public string BrokerVirtualHost
        {
            get { return _brokerVirtualHost; }
        }

        public int RpcClientTimeOutMilliSeconds
        {
            get { return _rpcClientTimeOutMilliSeconds; }
        }

        #endregion Properties

        #region Methods

        public void Initialize()
        {
            _brokerHost = _settingsManager.GetValue<string>(MQSettingsConstants.BrokerHostSettingsKey);
            _brokerVirtualHost = _settingsManager.GetValue<string>(MQSettingsConstants.BrokerVirtualHostSettingsKey);
            _brokerPort = _settingsManager.GetValue<int>(MQSettingsConstants.BrokerPortSettingsKey);
            _brokerUser = _settingsManager.GetValue<string>(MQSettingsConstants.BrokerUserSettingsKey);
            _brokerPwd = _settingsManager.GetValue<string>(MQSettingsConstants.BrokerPwdSettingsKey);

            _rpcClientTimeOutMilliSeconds = _settingsManager.GetValue<int>(MQSettingsConstants.RpcClientTimeOutMilliSecondsSettingsKey);
        }

        #endregion Methods
    }
}