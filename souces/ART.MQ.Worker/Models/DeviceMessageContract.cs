namespace ART.MQ.Worker.Models
{
    public class DeviceMessageContract<TContract>
    {
        private readonly string _topic;
        private readonly TContract _contract;

        public DeviceMessageContract(string topic, TContract contract)
        {
            _topic = topic;
            _contract = contract;
        }
        public string Topic
        {
            get { return _topic; }
        }

        public TContract Contract
        {
            get { return _contract; }
        }
    }
}
