namespace ART.Domotica.Domain.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DSFamilyTempSensorProfile : Profile
    {
        #region Constructors

        public DSFamilyTempSensorProfile()
        {
            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorGetAllModel>();
            CreateMap<DSFamilyTempSensorResolution, DSFamilyTempSensorResolutionGetAllModel>();
        }

        #endregion Constructors
    }
}