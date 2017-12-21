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
            CreateMap<DeviceNTP, DeviceNTPGetModel>()
                .ForMember(vm => vm.DeviceNTPId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.TimeZoneId, m => m.MapFrom(x => x.TimeZoneId))
                .ForMember(vm => vm.UpdateIntervalInMilliSecond, m => m.MapFrom(x => x.UpdateIntervalInMilliSecond));

            CreateMap<DeviceNTP, DeviceNTPSetUtcTimeOffsetInSecondRequestIoTContract>()
                .ForMember(vm => vm.UtcTimeOffsetInSecond, m => m.MapFrom(x => x.TimeZone.UtcTimeOffsetInSecond));

            CreateMap<DeviceNTP, DeviceNTPDetailResponseContract>()
                .ForMember(vm => vm.UpdateIntervalInMilliSecond, m => m.MapFrom(x => x.UpdateIntervalInMilliSecond))
                .ForMember(vm => vm.UtcTimeOffsetInSecond, m => m.MapFrom(x => x.TimeZone.UtcTimeOffsetInSecond));

            CreateMap<DeviceNTPSetUpdateIntervalInMilliSecondRequestContract, DeviceNTPSetUpdateIntervalInMilliSecondRequestIoTContract>()
                .ForMember(vm => vm.UpdateIntervalInMilliSecond, m => m.MapFrom(x => x.UpdateIntervalInMilliSecond));

            CreateMap<DeviceNTPSetTimeZoneRequestContract, DeviceNTPSetTimeZoneModel>()
                .ForMember(vm => vm.DeviceNTPId, m => m.MapFrom(x => x.DeviceNTPId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.TimeZoneId, m => m.MapFrom(x => x.TimeZoneId));

            CreateMap<DeviceNTPSetUpdateIntervalInMilliSecondRequestContract, DeviceNTPSetUpdateIntervalInMilliSecondModel>()
                .ForMember(vm => vm.DeviceNTPId, m => m.MapFrom(x => x.DeviceNTPId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.UpdateIntervalInMilliSecond, m => m.MapFrom(x => x.UpdateIntervalInMilliSecond));
        }

        #endregion Constructors
    }
}