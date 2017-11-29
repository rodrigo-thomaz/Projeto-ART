using ART.Domotica.Enums;
using ART.Infra.CrossCutting.Repository;

namespace ART.Domotica.Repository.Entities
{
    public class SensorType : IEntity<SensorTypeEnum>
    {
        #region Properties
        
        public SensorTypeEnum Id
        {
            get; set;
        }

        public string Name { get; set; }

        #endregion Properties
    }
}