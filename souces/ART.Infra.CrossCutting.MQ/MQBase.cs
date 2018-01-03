namespace ART.Infra.CrossCutting.MQ
{
    using System.Collections.Generic;

    using RabbitMQ.Client;

    public abstract class MQBase
    {
        #region Fields

        protected readonly IConnection _connection;
        protected readonly IMQSettings _mqSettings;

        #endregion Fields

        #region Constructors

        public MQBase(IConnection connection, IMQSettings mqSettings)
        {
            _connection = connection;
            _mqSettings = mqSettings;
        }

        #endregion Constructors

        #region Methods

        protected Dictionary<string, object> CreateBasicArguments()
        {
            var arguments = new Dictionary<string, object>();

            arguments.Add("x-expires", _mqSettings.QueueExpiresMilliSecondsSettingsKey);

            return arguments;
        }

        #endregion Methods
    }
}