namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class UnitOfMeasurementTypeProfile : Profile
    {
        #region Constructors

        public UnitOfMeasurementTypeProfile()
        {
            CreateMap<UnitOfMeasurementType, UnitOfMeasurementTypeDetailModel>()
                .ForMember(vm => vm.UnitOfMeasurementTypeId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Name, m => m.MapFrom(x => x.Name));
        }

        #endregion Constructors
    }
}