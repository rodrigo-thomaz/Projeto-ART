namespace ART.Domotica.IoTContract
{
    public class MessageIoTContract<TContract>
    {
        #region Fields

        private readonly TContract _contract;

        #endregion Fields

        #region Constructors

        public MessageIoTContract(TContract contract)
        {
            _contract = contract;
        }

        #endregion Constructors

        #region Properties

        public TContract Contract
        {
            get { return _contract; }
        }

        #endregion Properties
    }
}