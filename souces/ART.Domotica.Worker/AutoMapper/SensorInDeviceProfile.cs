namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorInDeviceProfile : Profile
    {
        #region Constructors

        public SensorInDeviceProfile()
        {
            CreateMap<SensorInDevice, SensorInDeviceGetModel>()
                .ForMember(vm => vm.DeviceSensorsId, m => m.MapFrom(x => x.DeviceSensorsId))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId));

            CreateMap<SensorInDevice, SensorInDeviceSetOrdinationModel>()
                .ForMember(vm => vm.DeviceSensorsId, m => m.MapFrom(x => x.DeviceSensorsId))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.Ordination, m => m.MapFrom(x => x.Ordination));
        }

        #endregion Constructors
    }
}