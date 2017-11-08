namespace ART.Domotica.Domain.AutoMapper
{
    using System.Linq;

    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Utils;

    using global::AutoMapper;
    using ART.Domotica.Contract;

    public class DSFamilyTempSensorProfile : Profile
    {
        #region Constructors

        public DSFamilyTempSensorProfile()
        {
            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorGetAllModel>();

            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorGetAllByHardwareInApplicationIdResponseContract>()
                .ForMember(vm => vm.ResolutionBits, m => m.MapFrom(x => x.DSFamilyTempSensorResolution.Bits))
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.Id));

            CreateMap<DSFamilyTempSensorResolution, DSFamilyTempSensorResolutionGetAllModel>();

            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorGetListModel>()
                .ForMember(vm => vm.InApplication, m => m.MapFrom(x => x.HardwaresInApplication.Any()))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)));
        }

        #endregion Constructors
    }
}