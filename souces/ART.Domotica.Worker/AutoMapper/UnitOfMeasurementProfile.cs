namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class UnitOfMeasurementProfile : Profile
    {
        #region Constructors

        public UnitOfMeasurementProfile()
        {
            CreateMap<UnitOfMeasurement, UnitOfMeasurementDetailModel>()
                .ForMember(vm => vm.UnitOfMeasurementId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.UnitOfMeasurementTypeId, m => m.MapFrom(x => x.UnitOfMeasurementTypeId))
                .ForMember(vm => vm.Name, m => m.MapFrom(x => x.Name))
                .ForMember(vm => vm.Symbol, m => m.MapFrom(x => x.Symbol))
                .ForMember(vm => vm.Description, m => m.MapFrom(x => x.Description));

            CreateMap<UnitOfMeasurement, UnitOfMeasurementGetAllForIoTResponseContract>();
        }

        #endregion Constructors
    }
}