namespace ART.Domotica.Worker.AutoMapper.SI
{
    using ART.Domotica.Model.SI;
    using ART.Domotica.Repository.Entities.SI;

    using global::AutoMapper;

    public class UnitMeasurementTypeProfile : Profile
    {
        #region Constructors

        public UnitMeasurementTypeProfile()
        {
            CreateMap<UnitMeasurementType, UnitMeasurementTypeGetModel>()
                .ForMember(vm => vm.UnitMeasurementTypeId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Name, m => m.MapFrom(x => x.Name));
        }

        #endregion Constructors
    }
}