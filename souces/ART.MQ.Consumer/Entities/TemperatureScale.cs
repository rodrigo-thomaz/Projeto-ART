using System.Collections.Generic;

namespace ART.MQ.Consumer.Entities
{
    public class TemperatureScale : IEntity<byte>
    {
        #region Primitive Properties

        public byte Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<DSFamilyTempSensor> DSFamilyTempSensors { get; set; }

        #endregion
    }
}