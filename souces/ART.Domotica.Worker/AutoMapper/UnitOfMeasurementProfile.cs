namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class UnitOfMeasurementProfile : Profile
    {
        #region Constructors

        public UnitOfMeasurementProfile()
        {
            CreateMap<UnitOfMeasurement, UnitOfMeasurementDetailModel>();
            CreateMap<UnitOfMeasurement, UnitOfMeasurementGetAllForIoTResponseContract>();
        }

        #endregion Constructors
    }
}