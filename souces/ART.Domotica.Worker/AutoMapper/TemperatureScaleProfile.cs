namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Worker.Models;

    using global::AutoMapper;

    public class TemperatureScaleProfile : Profile
    {
        #region Constructors

        public TemperatureScaleProfile()
        {
            CreateMap<TemperatureScale, TemperatureScaleModel>();
        }

        #endregion Constructors
    }
}