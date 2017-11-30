namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorDatasheetProfile : Profile
    {
        #region Constructors

        public SensorDatasheetProfile()
        {
            CreateMap<SensorDatasheet, SensorDatasheetDetailModel>();
        }

        #endregion Constructors
    }
}