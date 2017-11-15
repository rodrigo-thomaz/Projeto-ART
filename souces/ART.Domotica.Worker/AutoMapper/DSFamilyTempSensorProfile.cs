namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DSFamilyTempSensorProfile : Profile
    {
        #region Constructors

        public DSFamilyTempSensorProfile()
        {
            CreateMap<SensorsInDevice, DSFamilyTempSensorGetDetailModel>()
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.SensorBaseId))
                .ForMember(vm => vm.DSFamilyTempSensorResolutionId, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).DSFamilyTempSensorResolutionId))
                .ForMember(vm => vm.HighAlarm, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).HighAlarm))
                .ForMember(vm => vm.LowAlarm, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).LowAlarm))
                .ForMember(vm => vm.TemperatureScaleId, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).TemperatureScaleId));

            CreateMap<DSFamilyTempSensorResolution, DSFamilyTempSensorResolutionDetailModel>();

            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorGetAllByDeviceInApplicationIdResponseIoTContract>()
                .ForMember(vm => vm.DeviceAddress, m => m.ResolveUsing(src => {
                    var split = src.DeviceAddress.Split(':');
                    var result = new short[8];
                    for (int i = 0; i < 8; i++)
                    {
                        result[i] = short.Parse(split[i]);
                    }
                    return result;
                }))
                .ForMember(vm => vm.ResolutionBits, m => m.MapFrom(x => x.DSFamilyTempSensorResolution.Bits))
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.Id));

            CreateMap<DSFamilyTempSensorSetResolutionRequestContract, DSFamilyTempSensorSetResolutionRequestIoTContract>();
            CreateMap<DSFamilyTempSensorSetLowAlarmRequestContract, DSFamilyTempSensorSetLowAlarmRequestIoTContract>();
            CreateMap<DSFamilyTempSensorSetHighAlarmRequestContract, DSFamilyTempSensorSetHighAlarmRequestIoTContract>();

            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorSetResolutionCompletedModel>()
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DSFamilyTempSensorResolutionId, m => m.MapFrom(x => x.DSFamilyTempSensorResolutionId));

            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorSetLowAlarmCompletedModel>()
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.LowAlarm, m => m.MapFrom(x => x.LowAlarm));

            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorSetHighAlarmCompletedModel>()
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.HighAlarm, m => m.MapFrom(x => x.HighAlarm));
        }

        #endregion Constructors
    }
}