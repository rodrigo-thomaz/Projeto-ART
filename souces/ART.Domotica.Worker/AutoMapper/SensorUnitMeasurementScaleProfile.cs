namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorUnitMeasurementScaleProfile : Profile
    {
        #region Constructors

        public SensorUnitMeasurementScaleProfile()
        {
            CreateMap<SensorUnitMeasurementScale, SensorUnitMeasurementScaleDetailModel>();
            CreateMap<SensorUnitMeasurementScaleSetValueRequestContract, SensorUnitMeasurementScaleSetValueRequestIoTContract>();
            CreateMap<SensorUnitMeasurementScaleSetValueRequestContract, SensorUnitMeasurementScaleSetValueCompletedModel>();
        }

        #endregion Constructors
    }
}