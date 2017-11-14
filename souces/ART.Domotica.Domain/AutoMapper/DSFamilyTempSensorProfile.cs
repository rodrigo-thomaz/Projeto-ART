namespace ART.Domotica.Domain.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Utils;

    using global::AutoMapper;

    public class DSFamilyTempSensorProfile : Profile
    {
        #region Constructors

        public DSFamilyTempSensorProfile()
        {
            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorGetListInApplicationModel>()
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.Id));
                //.ForMember(vm => vm.HardwareInApplicationId, m => m.MapFrom(x => x.HardwaresInApplication.Single().Id));

            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorGetAllByDeviceInApplicationIdResponseContract>()
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

            CreateMap<DSFamilyTempSensorResolution, DSFamilyTempSensorResolutionGetAllModel>();

            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorGetAllModel>()
                //.ForMember(vm => vm.InApplication, m => m.MapFrom(x => x.HardwaresInApplication.Any()))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)));
        }

        #endregion Constructors
    }
}