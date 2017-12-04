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
            CreateMap<SensorUnitMeasurementScale, SensorUnitMeasurementScaleGetModel>();
            CreateMap<SensorUnitMeasurementScaleSetValueRequestContract, SensorUnitMeasurementScaleSetValueRequestIoTContract>();
            CreateMap<SensorUnitMeasurementScaleSetValueRequestContract, SensorUnitMeasurementScaleSetValueModel>();
        }

        #endregion Constructors
    }
}