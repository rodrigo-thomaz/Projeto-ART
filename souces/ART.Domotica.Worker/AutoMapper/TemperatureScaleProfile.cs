namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class TemperatureScaleProfile : Profile
    {
        #region Constructors

        public TemperatureScaleProfile()
        {
            CreateMap<TemperatureScale, TemperatureScaleDetailModel>();
            CreateMap<TemperatureScale, TemperatureScaleGetAllForIoTResponseContract>();
        }

        #endregion Constructors
    }
}