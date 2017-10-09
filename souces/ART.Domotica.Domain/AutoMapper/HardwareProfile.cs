namespace ART.Domotica.Domain.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Utils;
    using global::AutoMapper;

    public class HardwareProfile : Profile
    {
        #region Constructors

        public HardwareProfile()
        {
            CreateMap<HardwareBase, HardwareGetListModel>()
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)));
        }

        #endregion Constructors        
    }
}