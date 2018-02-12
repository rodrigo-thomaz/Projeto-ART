namespace ART.Domotica.Worker.AutoMapper
{
    using System.Linq;

    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DeviceSensorProfile : Profile
    {
        #region Constructors

        public DeviceSensorProfile()
        {
            CreateMap<DeviceSensor, DeviceSensorGetModel>()
                .ForMember(vm => vm.DeviceTypeId, m => m.MapFrom(x => x.DeviceTypeId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.ReadIntervalInMilliSeconds, m => m.MapFrom(x => x.ReadIntervalInMilliSeconds))
                .ForMember(vm => vm.PublishIntervalInMilliSeconds, m => m.MapFrom(x => x.PublishIntervalInMilliSeconds))
                .ForMember(vm => vm.SensorInDevice, m => m.MapFrom(x => x.SensorInDevice.OrderBy(y => y.Ordination)));

            CreateMap<DeviceSensor, DeviceSensorSetReadIntervalInMilliSecondsModel>()
                .ForMember(vm => vm.DeviceTypeId, m => m.MapFrom(x => x.DeviceTypeId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.ReadIntervalInMilliSeconds, m => m.MapFrom(x => x.ReadIntervalInMilliSeconds));

            CreateMap<DeviceSensor, DeviceSetPublishIntervalInMilliSecondsModel>()
                .ForMember(vm => vm.DeviceTypeId, m => m.MapFrom(x => x.DeviceTypeId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.PublishIntervalInMilliSeconds, m => m.MapFrom(x => x.PublishIntervalInMilliSeconds));

            CreateMap<DeviceSensor, DeviceSensorGetResponseIoTContract>()
                .ForMember(vm => vm.SensorsInDevice, m => m.MapFrom(x => x.SensorInDevice.OrderBy(y => y.Ordination)))
                .ForMember(vm => vm.ReadIntervalInMilliSeconds, m => m.MapFrom(x => x.ReadIntervalInMilliSeconds))
                .ForMember(vm => vm.PublishIntervalInMilliSeconds, m => m.MapFrom(x => x.PublishIntervalInMilliSeconds))
                .ForMember(vm => vm.SensorDatasheets, m => m.MapFrom(x => x.SensorInDevice.Select(y => y.Sensor.SensorDatasheet).Distinct()));
        }

        #endregion Constructors
    }
}