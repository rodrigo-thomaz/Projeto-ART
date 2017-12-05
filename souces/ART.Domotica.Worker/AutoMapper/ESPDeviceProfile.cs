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
            CreateMap<ESPDevice, ESPDeviceGetModel>()
                .ForMember(vm => vm.ApplicationId, m => m.MapFrom(x => x.DevicesInApplication.Single().ApplicationId))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.DevicesInApplication.Single().DeviceId))
                .ForMember(vm => vm.Label, m => m.MapFrom(x => x.Label))
                .ForMember(vm => vm.DeviceNTP, m => m.MapFrom(x => x.DeviceNTP))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)));

            CreateMap<ESPDevice, ESPDeviceInsertInApplicationResponseIoTContract>()
                .ForMember(vm => vm.ApplicationId, m => m.MapFrom(x => x.DevicesInApplication.Single().ApplicationId));

            CreateMap<ESPDevice, ESPDeviceGetByPinModel>();

            CreateMap<ESPDevice, ESPDeviceGetConfigurationsRPCResponseContract>()//
                .ForMember(vm => vm.ApplicationId, m => m.ResolveUsing(src => {
                    if (src.DevicesInApplication != null && src.DevicesInApplication.Any())
                    {
                        return src.DevicesInApplication.Single().ApplicationId;
                    }
                    return (Guid?)null;
                }))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.DevicesInApplication.Single().DeviceId))
                .ForMember(vm => vm.DeviceMQ, m => m.MapFrom(x => x.DeviceMQ))
                .ForMember(vm => vm.DeviceNTP, m => m.MapFrom(x => x.DeviceNTP));

            CreateMap<ESPDevice, ESPDeviceUpdatePinsResponseIoTContract>()
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id));

            CreateMap<ESPDevice, ESPDeviceAdminGetModel>()
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)))
                .ForMember(vm => vm.InApplication, m => m.MapFrom(x => x.DevicesInApplication.Any()));

            CreateMap<ESPDeviceSetLabelRequestContract, ESPDeviceSetLabelModel>();
        }

        #endregion Constructors
    }
}