namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model.SI;
    using ART.Domotica.Repository.Entities.SI;

    using global::AutoMapper;

    public class UnitMeasurementProfile : Profile
    {
        #region Constructors

        public UnitMeasurementProfile()
        {
            CreateMap<UnitMeasurement, UnitMeasurementDetailModel>()
                .ForMember(vm => vm.UnitMeasurementId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.UnitMeasurementTypeId, m => m.MapFrom(x => x.UnitMeasurementTypeId))
                .ForMember(vm => vm.Name, m => m.MapFrom(x => x.Name))
                .ForMember(vm => vm.Symbol, m => m.MapFrom(x => x.Symbol))
                .ForMember(vm => vm.Description, m => m.MapFrom(x => x.Description));

            CreateMap<UnitMeasurement, UnitMeasurementGetAllForIoTResponseContract>();
        }

        #endregion Constructors
    }
}