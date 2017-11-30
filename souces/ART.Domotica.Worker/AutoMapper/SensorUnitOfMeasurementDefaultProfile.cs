namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorUnitOfMeasurementDefaultProfile : Profile
    {
        #region Constructors

        public SensorUnitOfMeasurementDefaultProfile()
        {
            CreateMap<SensorUnitOfMeasurementDefault, SensorUnitOfMeasurementDefaultDetailModel>();
        }

        #endregion Constructors
    }
}