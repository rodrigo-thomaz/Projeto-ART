namespace ART.Domotica.Domain.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class HardwareProfile : Profile
    {
        #region Constructors

        public HardwareProfile()
        {
            CreateMap<HardwareBase, HardwareGetListModel>();
        }

        #endregion Constructors
    }
}