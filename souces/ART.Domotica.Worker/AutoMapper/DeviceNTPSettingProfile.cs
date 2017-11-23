namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;
    using global::AutoMapper;

    public class DeviceNTPSettingProfile : Profile
    {
        #region Constructors

        public DeviceNTPSettingProfile()
        {
            CreateMap<DeviceNTPSetting, DeviceNTPSettingDetailModel>();
        }

        #endregion Constructors
    }
}