namespace ART.Domotica.Domain.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class TemperatureScaleProfile : Profile
    {
        #region Constructors

        public TemperatureScaleProfile()
        {
            CreateMap<TemperatureScale, TemperatureScaleGetAllModel>();
            CreateMap<TemperatureScale, TemperatureScaleGetAllForDeviceResponseContract>();
        }

        #endregion Constructors
    }
}