namespace ART.Domotica.Domain.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Utils;

    using global::AutoMapper;

    public class ESPDeviceProfile : Profile
    {
        #region Constructors

        public ESPDeviceProfile()
        {
            CreateMap<HardwaresInApplication, ESPDeviceGetListModel>()
                .ForMember(vm => vm.HardwaresInApplicationId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.HardwareId, m => m.MapFrom(x => x.HardwareBaseId))
                .ForMember(vm => vm.ChipId, m => m.MapFrom(x => ((ESPDeviceBase)x.HardwareBase).ChipId))
                .ForMember(vm => vm.FlashChipId, m => m.MapFrom(x => ((ESPDeviceBase)x.HardwareBase).FlashChipId))
                .ForMember(vm => vm.MacAddress, m => m.MapFrom(x => ((ESPDeviceBase)x.HardwareBase).MacAddress))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)));

            CreateMap<HardwareBase, ESPDeviceGetByPinModel>();

            CreateMap<ESPDeviceBase, ESPDeviceUpdatePinsContract>()
                .ForMember(vm => vm.HardwareId, m => m.MapFrom(x => x.Id));

            CreateMap<HardwaresInApplication, ESPDeviceGetInApplicationForDeviceResponseContract>()
                .ForMember(vm => vm.HardwaresInApplicationId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.HardwareId, m => m.MapFrom(x => x.HardwareBaseId));
        }

        #endregion Constructors
    }
}