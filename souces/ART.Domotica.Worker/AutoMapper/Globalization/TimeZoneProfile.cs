namespace ART.Domotica.Worker.AutoMapper.Globalization
{
    using ART.Domotica.Model.Globalization;
    using ART.Domotica.Repository.Entities.Globalization;

    using global::AutoMapper;

    public class TimeZoneProfile : Profile
    {
        #region Constructors

        public TimeZoneProfile()
        {
            CreateMap<TimeZone, TimeZoneGetModel>();
        }

        #endregion Constructors
    }
}