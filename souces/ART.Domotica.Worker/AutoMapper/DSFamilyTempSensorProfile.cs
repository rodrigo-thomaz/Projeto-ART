namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Worker.Models;

    using global::AutoMapper;

    public class DSFamilyTempSensorProfile : Profile
    {
        #region Constructors

        public DSFamilyTempSensorProfile()
        {
            CreateMap<DSFamilyTempSensorResolution, DSFamilyTempSensorResolutionModel>();
            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorModel>();
        }

        #endregion Constructors
    }
}