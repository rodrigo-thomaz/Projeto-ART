namespace ART.Domotica.Model
{
    using ART.Domotica.Enums;

    public class DeviceTypeGetModel
    {
        #region Properties

        public DeviceTypeEnum DeviceTypeId
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