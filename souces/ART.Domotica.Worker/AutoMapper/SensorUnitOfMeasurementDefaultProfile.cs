namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorUnitOfMeasurementDefaultProfile : Profile
    {
        #region Constructors

        public SensorUnitOfMeasurementDefaultProfile()
        {
            CreateMap<SensorUnitOfMeasurementDefault, SensorUnitOfMeasurementDefaultDetailModel>()
                .ForMember(vm => vm.SensorUnitOfMeasurementDefaultId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.UnitOfMeasurementId, m => m.MapFrom(x => x.UnitOfMeasurementId))
                .ForMember(vm => vm.UnitOfMeasurementTypeId, m => m.MapFrom(x => x.UnitOfMeasurementTypeId))
                .ForMember(vm => vm.Max, m => m.MapFrom(x => x.Max))
                .ForMember(vm => vm.Min, m => m.MapFrom(x => x.Min));
        }

        #endregion Constructors
    }
}