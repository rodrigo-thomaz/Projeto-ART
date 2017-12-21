namespace ART.Domotica.Model
{
    using ART.Domotica.Enums;

    public class DeviceDatasheetGetModel
    {
        #region Properties

        public string Name
        {
            get; set;
        }

        public DeviceDatasheetEnum DeviceDatasheetId
        {
            get; set;
        }

        #endregion Properties
    }
}