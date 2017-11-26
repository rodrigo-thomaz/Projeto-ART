namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Contract;
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

            CreateMap<DeviceNTP, DeviceNTPSetUtcTimeOffsetInSecondRequestIoTContract>()
                .ForMember(vm => vm.UtcTimeOffsetInSecond, m => m.MapFrom(x => x.TimeZone.UtcTimeOffsetInSecond));

            CreateMap<DeviceNTP, DeviceNTPDetailResponseContract>()
                .ForMember(vm => vm.UpdateIntervalInMilliSecond, m => m.MapFrom(x => x.UpdateIntervalInMilliSecond))
                .ForMember(vm => vm.UtcTimeOffsetInSecond, m => m.MapFrom(x => x.TimeZone.UtcTimeOffsetInSecond));

            CreateMap<DeviceNTPSetUpdateIntervalInMilliSecondRequestContract, DeviceNTPSetUpdateIntervalInMilliSecondRequestIoTContract>();

            CreateMap<DeviceNTPSetTimeZoneRequestContract, DeviceNTPSetTimeZoneCompletedModel>();
            CreateMap<DeviceNTPSetUpdateIntervalInMilliSecondRequestContract, DeviceNTPSetUpdateIntervalInMilliSecondCompletedModel>();
        }

        #endregion Constructors
    }
}