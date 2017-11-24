namespace ART.Domotica.Contract
{
    public class DeviceMQDetailResponseContract
    {
        public string ApplicationTopic
        {
            get; set;
        }

        public string ClientId
        {
            get; set;
        }

        public string DeviceTopic
        {
            get; set;
        }

        public string Host
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public int Port
        {
            get; set;
        }

        public string User
        {
            get; set;
        }
    }
}
