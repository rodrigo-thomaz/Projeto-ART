namespace ART.Domotica.Domain.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Utils;
    using global::AutoMapper;
    using System.Linq;

    public class HardwareProfile : Profile
    {
        #region Constructors

        public HardwareProfile()
        {
            CreateMap<HardwareBase, HardwareGetListModel>()
                .ForMember(vm => vm.InApplication, m => m.MapFrom(x => x.HardwaresInApplication.Any()))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)));
        }

        #endregion Constructors        
    }
}