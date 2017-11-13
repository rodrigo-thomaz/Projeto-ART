namespace ART.Domotica.Domain.AutoMapper
{
    using System;
    using System.Linq;

    using ART.Domotica.Domain.DTOs;
    using ART.Domotica.Repository.Entities;
    using global::AutoMapper;

    public class ESPDeviceProfile : Profile
    {
        #region Constructors

        public ESPDeviceProfile()
        {
            CreateMap<ESPDeviceBase, ESPDeviceBaseDTO>()
                .ForMember(vm => vm.ESPDeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.HardwaresInApplicationId, m => m.ResolveUsing(src => {
                    if(src.HardwaresInApplication != null && src.HardwaresInApplication.Any())
                    {
                        return src.HardwaresInApplication.SingleOrDefault().Id;
                    }
                    return (Guid?)null;
                }));
        }

        #endregion Constructors
    }
}