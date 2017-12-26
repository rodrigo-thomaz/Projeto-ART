namespace ART.Domotica.Model
{
    using ART.Domotica.Enums;

    public class DeviceDatasheetGetModel
    {
        #region Properties

        public DeviceDatasheetEnum DeviceDatasheetId
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        #endregion Properties
    }
}