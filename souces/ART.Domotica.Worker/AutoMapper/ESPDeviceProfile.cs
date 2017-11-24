namespace ART.Domotica.Worker.AutoMapper
{
    using System;
    using System.Linq;

    using ART.Domotica.Contract;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Utils;

    using global::AutoMapper;

    public class ESPDeviceProfile : Profile
    {
        #region Constructors

        public ESPDeviceProfile()
        {
            CreateMap<ESPDevice, ESPDeviceDetailModel>()
                .ForMember(vm => vm.DeviceInApplicationId, m => m.MapFrom(x => x.DevicesInApplication.Single().Id))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceNTP, m => m.MapFrom(x => x.DeviceNTP))
                .ForMember(vm => vm.Sensors, m => m.MapFrom(x => x.SensorsInDevice))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)));

            CreateMap<ESPDevice, ESPDeviceInsertInApplicationResponseIoTContract>()
                .ForMember(vm => vm.DeviceInApplicationId, m => m.MapFrom(x => x.DevicesInApplication.Single().Id));

            CreateMap<ESPDevice, ESPDeviceGetByPinModel>();

            CreateMap<DeviceMQ, DeviceMQDetailResponseContract>()
                .ForMember(vm => vm.User, m => m.MapFrom(x => x.User))
                .ForMember(vm => vm.Password, m => m.MapFrom(x => x.Password))
                .ForMember(vm => vm.ClientId, m => m.MapFrom(x => x.ClientId))
                .ForMember(vm => vm.DeviceTopic, m => m.MapFrom(x => x.Topic));

            CreateMap<DeviceNTP, DeviceNTPDetailResponseContract>()
                .ForMember(vm => vm.UpdateIntervalInMilliSecond, m => m.MapFrom(x => x.UpdateIntervalInMilliSecond))
                .ForMember(vm => vm.TimeOffsetInSecond, m => m.MapFrom(x => x.TimeOffsetInSecond));

            CreateMap<ESPDevice, ESPDeviceGetConfigurationsRPCResponseContract>()//
                .ForMember(vm => vm.DeviceInApplicationId, m => m.ResolveUsing(src => {
                    if (src.DevicesInApplication != null && src.DevicesInApplication.Any())
                    {
                        return src.DevicesInApplication.Single().Id;
                    }
                    return (Guid?)null;
                }))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceMQ, m => m.MapFrom(x => x.DeviceMQ))
                .ForMember(vm => vm.DeviceNTP, m => m.MapFrom(x => x.DeviceNTP));

            CreateMap<ESPDevice, ESPDeviceUpdatePinsResponseIoTContract>()
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id));

            CreateMap<ESPDevice, ESPDeviceAdminDetailModel>()
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)))
                .ForMember(vm => vm.InApplication, m => m.MapFrom(x => x.DevicesInApplication.Any()));

            CreateMap<ESPDeviceSetTimeOffsetInSecondRequestContract, ESPDeviceSetTimeOffsetInSecondRequestIoTContract>();
            CreateMap<ESPDeviceSetUpdateIntervalInMilliSecondRequestContract, ESPDeviceSetUpdateIntervalInMilliSecondRequestIoTContract>();

            CreateMap<ESPDeviceSetTimeOffsetInSecondRequestContract, ESPDeviceSetTimeOffsetInSecondCompletedModel>();
            CreateMap<ESPDeviceSetUpdateIntervalInMilliSecondRequestContract, ESPDeviceSetUpdateIntervalInMilliSecondCompletedModel>();
        }

        #endregion Constructors
    }
}