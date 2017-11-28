namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class TempSensorRangeProfile : Profile
    {
        #region Constructors

        public TempSensorRangeProfile()
        {
            CreateMap<SensorRange, TempSensorRangeGetDetailModel>();
        }

        #endregion Constructors
    }
}