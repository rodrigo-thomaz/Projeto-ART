namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorInDeviceProfile : Profile
    {
        #region Constructors

        public SensorInDeviceProfile()
        {
            CreateMap<SensorInDevice, SensorInDeviceGetModel>()
                .ForMember(vm => vm.DeviceTypeId, m => m.MapFrom(x => x.DeviceTypeId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.DeviceSensorId))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId));

            CreateMap<SensorInDevice, SensorInDeviceSetOrdinationModel>()
                .ForMember(vm => vm.DeviceTypeId, m => m.MapFrom(x => x.DeviceTypeId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.DeviceSensorId))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.Ordination, m => m.MapFrom(x => x.Ordination));

            CreateMap<SensorInDevice, SensorInDeviceGetResponseIoTContract>()
                .ForMember(vm => vm.Sensor, m => m.MapFrom(x => x.Sensor))
                .ForMember(vm => vm.Ordination, m => m.MapFrom(x => x.Ordination));

            CreateMap<SensorInDevice, SetOrdinationRequestIoTContract>().ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.Ordination, m => m.MapFrom(x => x.Ordination));
        }

        #endregion Constructors
    }
}