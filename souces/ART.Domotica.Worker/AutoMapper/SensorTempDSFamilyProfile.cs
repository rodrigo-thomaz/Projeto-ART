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
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Sensor.SensorInDevice.Single().DeviceSensorsId))
                .ForMember(vm => vm.SensorTempDSFamilyResolutionId, m => m.MapFrom(x => x.SensorTempDSFamilyResolutionId));

            CreateMap<SensorTempDSFamilyResolution, SensorTempDSFamilyResolutionGetModel>()
                .ForMember(vm => vm.SensorTempDSFamilyResolutionId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Name, m => m.MapFrom(x => x.Name))
                .ForMember(vm => vm.Bits, m => m.MapFrom(x => x.Bits))
                .ForMember(vm => vm.DecimalPlaces, m => m.MapFrom(x => x.DecimalPlaces))
                .ForMember(vm => vm.ConversionTime, m => m.MapFrom(x => x.ConversionTime))
                .ForMember(vm => vm.Description, m => m.MapFrom(x => x.Description));

            CreateMap<SensorTempDSFamilySetResolutionRequestContract, SensorTempDSFamilySetResolutionRequestIoTContract>();

            CreateMap<SensorInDevice, SensorTempDSFamilyGetModel>()
                .ForMember(vm => vm.SensorTempDSFamilyId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.SensorTempDSFamilyResolutionId, m => m.MapFrom(x => x.Sensor.SensorTempDSFamily.SensorTempDSFamilyResolutionId));

            CreateMap<SensorTempDSFamily, SensorTempDSFamilyGetModel>()
                .ForMember(vm => vm.SensorTempDSFamilyId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.SensorTempDSFamilyResolutionId, m => m.MapFrom(x => x.Sensor.SensorTempDSFamily.SensorTempDSFamilyResolutionId));
        }

        #endregion Constructors
    }
}