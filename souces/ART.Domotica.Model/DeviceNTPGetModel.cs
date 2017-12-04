namespace ART.Domotica.Model
{
    public class DeviceNTPGetModel
    {
        #region Properties

        public byte TimeZoneId
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