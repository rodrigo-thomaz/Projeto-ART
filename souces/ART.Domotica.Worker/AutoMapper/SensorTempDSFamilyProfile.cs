namespace ART.Domotica.Worker.AutoMapper
{
    using System.Linq;

    using ART.Domotica.Contract;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorTempDSFamilyProfile : Profile
    {
        #region Constructors

        public SensorTempDSFamilyProfile()
        {
            CreateMap<SensorTempDSFamily, SensorTempDSFamilySetResolutionModel>()
                .ForMember(vm => vm.SensorTempDSFamilyId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Sensor.SensorsInDevice.Single().DeviceSensorsId))
                .ForMember(vm => vm.SensorTempDSFamilyResolutionId, m => m.MapFrom(x => x.SensorTempDSFamilyResolutionId));

            CreateMap<SensorTempDSFamilyResolution, SensorTempDSFamilyResolutionGetModel>();

            CreateMap<SensorTempDSFamilySetResolutionRequestContract, SensorTempDSFamilySetResolutionRequestIoTContract>();

            CreateMap<SensorsInDevice, SensorTempDSFamilyGetModel>()
                .ForMember(vm => vm.SensorTempDSFamilyId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.SensorTempDSFamilyResolutionId, m => m.MapFrom(x => x.Sensor.SensorTempDSFamily.SensorTempDSFamilyResolutionId));
        }

        #endregion Constructors
    }
}