namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorProfile : Profile
    {
        #region Constructors

        public SensorProfile()
        {
            CreateMap<Sensor, SensorGetModel>()
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.Label, m => m.MapFrom(x => x.Label))
                .ForMember(vm => vm.SensorTriggers, m => m.MapFrom(x => x.SensorTriggers))
                .ForMember(vm => vm.SensorTempDSFamily, m => m.MapFrom(x => x.SensorTempDSFamily))
                .ForMember(vm => vm.SensorUnitMeasurementScale, m => m.MapFrom(x => x.SensorUnitMeasurementScale));            

            CreateMap<Sensor, SensorSetLabelModel>()
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.Label, m => m.MapFrom(x => x.Label));

            CreateMap<Sensor, SensorSetLabelRequestIoTContract>()
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Label, m => m.MapFrom(x => x.Label));
        }

        #endregion Constructors
    }
}