namespace ART.Domotica.Worker.AutoMapper
{
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
            CreateMap<HardwareInApplication, ESPDeviceDetailModel>()
                .ForMember(vm => vm.HardwareInApplicationId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.HardwareId, m => m.MapFrom(x => x.HardwareBaseId))
                .ForMember(vm => vm.ChipId, m => m.MapFrom(x => ((ESPDeviceBase)x.HardwareBase).ChipId))
                .ForMember(vm => vm.FlashChipId, m => m.MapFrom(x => ((ESPDeviceBase)x.HardwareBase).FlashChipId))
                .ForMember(vm => vm.MacAddress, m => m.MapFrom(x => ((ESPDeviceBase)x.HardwareBase).MacAddress))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)));
            
            CreateMap<HardwareInApplication, ESPDeviceInsertInApplicationResponseIoTContract>()
                .ForMember(vm => vm.HardwareInApplicationId, m => m.MapFrom(x => x.Id));

            CreateMap<HardwareInApplication, ESPDeviceDeleteFromApplicationModel>()
                .ForMember(vm => vm.HardwareInApplicationId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.HardwareId, m => m.MapFrom(x => x.HardwareBaseId));

            CreateMap<HardwareBase, ESPDeviceGetByPinModel>();
        }

        #endregion Constructors
    }
}
