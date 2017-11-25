namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class TimeZoneProfile : Profile
    {
        #region Constructors

        public TimeZoneProfile()
        {
            CreateMap<TimeZone, TimeZoneDetailModel>();
        }

        #endregion Constructors
    }
}