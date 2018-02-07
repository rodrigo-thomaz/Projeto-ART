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

        public long UpdateIntervalInMilliSecond
        {
            get; set;
        }

        public int UtcTimeOffsetInSecond
        {
            get; set;
        }

        #endregion Properties
    }
}