namespace ART.MQ.Worker.Models
{
    public class DSFamilyTempSensorResolutionModel
    {
        #region Properties

        public byte Bits
        {
            get; set;
        }

        public decimal ConversionTime
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public byte Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public decimal Resolution
        {
            get; set;
        }

        public string ResolutionDecimalPlaces
        {
            get; set;
        }

        #endregion Properties
    }
}