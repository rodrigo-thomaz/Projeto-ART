namespace ART.Domotica.Domain.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;
    using global::AutoMapper;

    public class HardwaresInApplicationProfile : Profile
    {
        #region Constructors

        public HardwaresInApplicationProfile()
        {
            CreateMap<HardwareBase, HardwaresInApplicationSearchPinModel>();
        }

        #endregion Constructors
    }
}