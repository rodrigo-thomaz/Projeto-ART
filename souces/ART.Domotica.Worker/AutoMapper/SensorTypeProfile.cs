namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorTypeProfile : Profile
    {
        #region Constructors

        public SensorTypeProfile()
        {
            CreateMap<SensorType, SensorTypeDetailModel>();
        }

        #endregion Constructors
    }
}