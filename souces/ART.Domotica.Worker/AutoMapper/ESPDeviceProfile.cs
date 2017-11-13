namespace ART.Domotica.Worker.AutoMapper
{

    using ART.Domotica.Contract;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Infra.CrossCutting.Utils;

    using global::AutoMapper;
    using ART.Domotica.Domain.DTOs;

    public class ESPDeviceProfile : Profile
    {
        #region Constructors

        public ESPDeviceProfile()
        {
            CreateMap<ESPDeviceBaseDTO, ESPDeviceDetailModel>()
                .ForMember(vm => vm.HardwareInApplicationId, m => m.MapFrom(x => x.HardwaresInApplicationId))
                .ForMember(vm => vm.HardwareId, m => m.MapFrom(x => x.ESPDeviceId))
                .ForMember(vm => vm.ChipId, m => m.MapFrom(x => x.ChipId))
                .ForMember(vm => vm.FlashChipId, m => m.MapFrom(x => x.FlashChipId))
                .ForMember(vm => vm.MacAddress, m => m.MapFrom(x => x.MacAddress))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)));

            CreateMap<ESPDeviceBaseDTO, ESPDeviceInsertInApplicationResponseIoTContract>()
                .ForMember(vm => vm.HardwareInApplicationId, m => m.MapFrom(x => x.HardwaresInApplicationId));

            CreateMap<ESPDeviceBaseDTO, ESPDeviceDeleteFromApplicationModel>()
                .ForMember(vm => vm.HardwareInApplicationId, m => m.MapFrom(x => x.HardwaresInApplicationId))
                .ForMember(vm => vm.HardwareId, m => m.MapFrom(x => x.ESPDeviceId));

            CreateMap<ESPDeviceBaseDTO, ESPDeviceGetByPinModel>();

            CreateMap<ESPDeviceBaseDTO, ESPDeviceGetConfigurationsRPCResponseContract>()
                .ForMember(vm => vm.HardwareInApplicationId, m => m.MapFrom(x => x.HardwaresInApplicationId))
                .ForMember(vm => vm.HardwareId, m => m.MapFrom(x => x.ESPDeviceId));

            CreateMap<ESPDeviceBaseDTO, ESPDeviceUpdatePinsResponseIoTContract>()
                .ForMember(vm => vm.HardwareId, m => m.MapFrom(x => x.ESPDeviceId));
        }

        #endregion Constructors
    }
}