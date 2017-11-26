namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DeviceNTPProfile : Profile
    {
        #region Constructors

        public DeviceNTPProfile()
        {
            CreateMap<DeviceNTP, DeviceNTPDetailModel>();

            CreateMap<DeviceNTP, ESPDeviceSetUtcTimeOffsetInSecondRequestIoTContract>()
                .ForMember(vm => vm.UtcTimeOffsetInSecond, m => m.MapFrom(x => x.TimeZone.UtcTimeOffsetInSecond));
        }

        #endregion Constructors
    }
}