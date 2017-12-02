namespace ART.Domotica.Worker.AutoMapper.Locale
{
    using ART.Domotica.Model.Locale;
    using ART.Domotica.Repository.Entities.Locale;

    using global::AutoMapper;

    public class CountryProfile : Profile
    {
        #region Constructors

        public CountryProfile()
        {
            CreateMap<Country, CountryDetailModel>()
                .ForMember(vm => vm.CountryId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Name, m => m.MapFrom(x => x.Name))
                .ForMember(vm => vm.ContinentId, m => m.MapFrom(x => x.ContinentId));
        }

        #endregion Constructors
    }
}