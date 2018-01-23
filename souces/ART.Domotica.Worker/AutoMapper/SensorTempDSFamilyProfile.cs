namespace ART.Domotica.Worker.AutoMapper
{
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
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.SensorTempDSFamilyResolutionId, m => m.MapFrom(x => x.SensorTempDSFamilyResolutionId));

            CreateMap<SensorTempDSFamilyResolution, SensorTempDSFamilyResolutionGetModel>()
                .ForMember(vm => vm.SensorTempDSFamilyResolutionId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Name, m => m.MapFrom(x => x.Name))
                .ForMember(vm => vm.Bits, m => m.MapFrom(x => x.Bits))
                .ForMember(vm => vm.DecimalPlaces, m => m.MapFrom(x => x.DecimalPlaces))
                .ForMember(vm => vm.ConversionTime, m => m.MapFrom(x => x.ConversionTime))
                .ForMember(vm => vm.Description, m => m.MapFrom(x => x.Description));

            CreateMap<SensorTempDSFamilySetResolutionRequestContract, SensorTempDSFamilySetResolutionRequestIoTContract>()
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorTempDSFamilyId))
                .ForMember(vm => vm.SensorTempDSFamilyResolutionId, m => m.MapFrom(x => x.SensorTempDSFamilyResolutionId));

            CreateMap<SensorInDevice, SensorTempDSFamilyGetModel>()
                .ForMember(vm => vm.SensorTempDSFamilyId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.SensorTempDSFamilyResolutionId, m => m.MapFrom(x => x.Sensor.SensorTempDSFamily.SensorTempDSFamilyResolutionId));

            CreateMap<SensorTempDSFamily, SensorTempDSFamilyGetModel>()
                .ForMember(vm => vm.SensorTempDSFamilyId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.SensorTempDSFamilyResolutionId, m => m.MapFrom(x => x.Sensor.SensorTempDSFamily.SensorTempDSFamilyResolutionId));

            CreateMap<SensorTempDSFamily, SensorTempDSFamilyGetResponseIoTContract>()
                .ForMember(vm => vm.ResolutionBits, m => m.MapFrom(x => x.SensorTempDSFamilyResolution.Bits));
        }

        #endregion Constructors
    }
}