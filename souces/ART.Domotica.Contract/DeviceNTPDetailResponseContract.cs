namespace ART.Domotica.Contract
{
    public class DeviceNTPDetailResponseContract
    {
        #region Properties

        public string Host
        {
            get; set;
        }

        public int Port
        {
            get; set;
        }

        public int TimeOffsetInSecond
        {
            get; set;
        }

        public int UpdateIntervalInMilliSecond
        {
            get; set;
        }

        #endregion Properties
    }
}