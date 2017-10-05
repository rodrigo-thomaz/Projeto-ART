namespace ART.MQ.Worker.AutoMapper
{
    using ART.Domotica.Repository.Entities;
    using ART.MQ.Worker.Models;

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