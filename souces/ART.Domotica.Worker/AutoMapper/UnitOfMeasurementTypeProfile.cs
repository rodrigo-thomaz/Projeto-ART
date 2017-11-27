namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class UnitOfMeasurementTypeProfile : Profile
    {
        #region Constructors

        public UnitOfMeasurementTypeProfile()
        {
            CreateMap<UnitOfMeasurementType, UnitOfMeasurementTypeDetailModel>();
        }

        #endregion Constructors
    }
}