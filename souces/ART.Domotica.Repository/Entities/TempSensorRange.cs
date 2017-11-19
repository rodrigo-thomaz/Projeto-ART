namespace ART.Domotica.Repository.Entities
{
    using ART.Infra.CrossCutting.Repository;

    public class TempSensorRange : IEntity<byte>
    {
        #region Properties

        public byte Id
        {
            get; set;
        }

        #endregion Properties
    }
}