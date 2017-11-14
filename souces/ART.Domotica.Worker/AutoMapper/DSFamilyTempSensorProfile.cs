using ART.Domotica.Model;
using ART.Domotica.Repository.Entities;
using AutoMapper;

namespace ART.Domotica.Worker.AutoMapper
{
    public class DSFamilyTempSensorProfile : Profile
    {
        #region Constructors

        public DSFamilyTempSensorProfile()
        {
            CreateMap<SensorsInDevice, DSFamilyTempSensorGetDetailModel>()
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.SensorBaseId))
                .ForMember(vm => vm.DeviceAddress, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).DeviceAddress))
                .ForMember(vm => vm.DSFamilyTempSensorResolutionId, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).DSFamilyTempSensorResolutionId))
                .ForMember(vm => vm.Family, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).Family))
                .ForMember(vm => vm.HighAlarm, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).HighAlarm))
                .ForMember(vm => vm.LowAlarm, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).LowAlarm))
                .ForMember(vm => vm.TemperatureScaleId, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).TemperatureScaleId));

            CreateMap<DSFamilyTempSensorResolution, DSFamilyTempSensorResolutionDetailModel>();
        }

        #endregion Constructors
    }
}