namespace ART.Domotica.Repository.Entities
{
    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class ActuatorType : IEntity<ActuatorTypeEnum>
    {
        #region Properties

        public ActuatorTypeEnum Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        #endregion Properties
    }
}