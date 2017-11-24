namespace ART.Domotica.Contract
{
    public class ApplicationMQGetRPCResponseContract
    {
        #region Properties

        public string Password
        {
            get; set;
        }

        public string ApplicationTopic
        {
            get; set;
        }

        public string WebUITopic
        {
            get; set;
        }

        public string User
        {
            get; set;
        }

        public string Host
        {
            get; set;
        }

        public int Port
        {
            get; set;
        }

        #endregion Properties
    }
}