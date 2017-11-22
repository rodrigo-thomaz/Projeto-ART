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
                .ForMember(vm => vm.Sensors, m => m.MapFrom(x => x.SensorsInDevice))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)));

            CreateMap<ESPDevice, ESPDeviceInsertInApplicationResponseIoTContract>()
                .ForMember(vm => vm.DeviceInApplicationId, m => m.MapFrom(x => x.DevicesInApplication.Single().Id));

            CreateMap<ESPDevice, ESPDeviceGetByPinModel>();

            CreateMap<ESPDevice, ESPDeviceGetConfigurationsRPCResponseContract>()//
                .ForMember(vm => vm.DeviceInApplicationId, m => m.ResolveUsing(src => {
                    if (src.DevicesInApplication != null && src.DevicesInApplication.Any())
                    {
                        return src.DevicesInApplication.Single().Id;
                    }
                    return (Guid?)null;
                }))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.BrokerUser, m => m.MapFrom(x => x.DeviceBrokerSetting.User))
                .ForMember(vm => vm.BrokerPassword, m => m.MapFrom(x => x.DeviceBrokerSetting.Password))
                .ForMember(vm => vm.BrokerClientId, m => m.MapFrom(x => x.DeviceBrokerSetting.ClientId))
                .ForMember(vm => vm.BrokerDeviceTopic, m => m.MapFrom(x => x.DeviceBrokerSetting.Topic))
                .ForMember(vm => vm.NTPUpdateInterval, m => m.MapFrom(x => x.DeviceNTPSetting.UpdateIntervalInMilliSecond))
                .ForMember(vm => vm.TimeOffset, m => m.MapFrom(x => x.DeviceNTPSetting.TimeOffsetInSecond));

            CreateMap<ESPDevice, ESPDeviceUpdatePinsResponseIoTContract>()
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id));

            CreateMap<ESPDevice, ESPDeviceAdminDetailModel>()
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)))
                .ForMember(vm => vm.InApplication, m => m.MapFrom(x => x.DevicesInApplication.Any()));
        }

        #endregion Constructors
    }
}