namespace ART.Domotica.Domain.AutoMapper
{
    using System;
    using System.Linq;

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
            CreateMap<ESPDeviceBase, ESPDeviceUpdatePinsContract>()
                .ForMember(vm => vm.HardwareId, m => m.MapFrom(x => x.Id));

            CreateMap<ESPDeviceBase, ESPDeviceGetConfigurationsResponseContract>()
                .ForMember(vm => vm.HardwareInApplicationId, m => m.MapFrom(x => x.HardwaresInApplication.Any() ? x.HardwaresInApplication.First().Id : (Guid?)null))
                .ForMember(vm => vm.HardwareId, m => m.MapFrom(x => x.Id));            
        }

        #endregion Constructors
    }
}