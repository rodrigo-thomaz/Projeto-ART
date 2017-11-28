namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorRangeProfile : Profile
    {
        #region Constructors

        public SensorRangeProfile()
        {
            CreateMap<SensorRange, SensorRangeGetDetailModel>();
        }

        #endregion Constructors
    }
}