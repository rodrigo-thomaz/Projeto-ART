namespace ART.Domotica.Worker.AutoMapper.Locale
{
    using ART.Domotica.Model.Locale;
    using ART.Domotica.Repository.Entities.Locale;

    using global::AutoMapper;

    public class ContinentProfile : Profile
    {
        #region Constructors

        public ContinentProfile()
        {
            CreateMap<Continent, ContinentDetailModel>()
                .ForMember(vm => vm.ContinentId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Name, m => m.MapFrom(x => x.Name));
        }

        #endregion Constructors
    }
}