namespace ART.Domotica.Contract
{
    public class ApplicationMQGetRPCResponseContract
    {
        #region Properties

        public string ApplicationTopic
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public string User
        {
            get; set;
        }

        public string WebUITopic
        {
            get; set;
        }

        #endregion Properties
    }
}