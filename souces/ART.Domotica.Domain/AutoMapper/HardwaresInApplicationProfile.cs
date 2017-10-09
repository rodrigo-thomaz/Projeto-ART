namespace ART.Domotica.Domain.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Utils;
    using global::AutoMapper;

    public class HardwaresInApplicationProfile : Profile
    {
        #region Constructors

        public HardwaresInApplicationProfile()
        {
            CreateMap<HardwaresInApplication, HardwaresInApplicationGetListModel>()
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)));

            CreateMap<HardwareBase, HardwaresInApplicationSearchPinModel>(); 
        }

        #endregion Constructors
    }
}