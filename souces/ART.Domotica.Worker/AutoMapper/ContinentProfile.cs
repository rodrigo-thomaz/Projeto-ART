namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

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