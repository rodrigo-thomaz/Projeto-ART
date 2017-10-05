namespace ART.Domotica.Worker.Contracts
{
    public class DeviceMessageContract<TContract>
    {
        #region Fields

        private readonly TContract _contract;
        private readonly string _topic;

        #endregion Fields

        #region Constructors

        public DeviceMessageContract(string topic, TContract contract)
        {
            _topic = topic;
            _contract = contract;
        }

        #endregion Constructors

        #region Properties

        public TContract Contract
        {
            get { return _contract; }
        }

        public string Topic
        {
            get { return _topic; }
        }

        #endregion Properties
    }
}