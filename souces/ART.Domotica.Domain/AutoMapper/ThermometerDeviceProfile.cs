namespace ART.Domotica.Domain.AutoMapper
{
    using System.Linq;

    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Utils;

    using global::AutoMapper;

    public class ThermometerDeviceProfile : Profile
    {
        #region Constructors

        public ThermometerDeviceProfile()
        {
            CreateMap<ThermometerDevice, ThermometerDeviceGetListModel>()
                .ForMember(vm => vm.InApplication, m => m.MapFrom(x => x.HardwaresInApplication.Any()))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)));
        }

        #endregion Constructors
    }
}