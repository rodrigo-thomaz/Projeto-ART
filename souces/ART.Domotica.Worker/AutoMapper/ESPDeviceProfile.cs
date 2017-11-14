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
                .ForMember(vm => vm.HardwareInApplicationId, m => m.MapFrom(x => x.HardwaresInApplication.Single().Id))
                .ForMember(vm => vm.HardwareId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)));

            CreateMap<ESPDevice, ESPDeviceInsertInApplicationResponseIoTContract>()
                .ForMember(vm => vm.HardwareInApplicationId, m => m.MapFrom(x => x.HardwaresInApplication.Single().Id));

            CreateMap<ESPDevice, ESPDeviceGetByPinModel>();

            CreateMap<ESPDevice, ESPDeviceGetConfigurationsRPCResponseContract>()//
                .ForMember(vm => vm.HardwareInApplicationId, m => m.ResolveUsing(src => {
                    if(src.HardwaresInApplication != null && src.HardwaresInApplication.Any())
                    {
                        return src.HardwaresInApplication.Single().Id;
                    }
                    return (Guid?)null;
                }))
                .ForMember(vm => vm.HardwareId, m => m.MapFrom(x => x.Id));

            CreateMap<ESPDevice, ESPDeviceUpdatePinsResponseIoTContract>()
                .ForMember(vm => vm.HardwareId, m => m.MapFrom(x => x.Id));

            CreateMap<ESPDevice, ESPDeviceAdminDetailModel>()
                .ForMember(vm => vm.HardwareId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)))
                .ForMember(vm => vm.InApplication, m => m.MapFrom(x => x.HardwaresInApplication.Any()));
        }

        #endregion Constructors
    }
}