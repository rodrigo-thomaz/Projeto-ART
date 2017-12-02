namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

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