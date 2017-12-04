namespace ART.Domotica.Worker.AutoMapper.SI
{
    using ART.Domotica.Model.SI;
    using ART.Domotica.Repository.Entities.SI;

    using global::AutoMapper;

    public class NumericalScaleTypeCountryProfile : Profile
    {
        #region Constructors

        public NumericalScaleTypeCountryProfile()
        {
            CreateMap<NumericalScaleTypeCountry, NumericalScaleTypeCountryGetModel>()
                .ForMember(vm => vm.NumericalScaleTypeId, m => m.MapFrom(x => x.NumericalScaleTypeId))
                .ForMember(vm => vm.CountryId, m => m.MapFrom(x => x.CountryId));
        }

        #endregion Constructors
    }
}