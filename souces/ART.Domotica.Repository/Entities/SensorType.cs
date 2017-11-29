namespace ART.Domotica.Repository.Entities
{
    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;
    using System.Collections.Generic;

    public class SensorType : IEntity<SensorTypeEnum>
    {
        #region Properties

        public SensorTypeEnum Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public ICollection<SensorDatasheet> SensorDatasheets { get; set; }

        #endregion Properties
    }
}