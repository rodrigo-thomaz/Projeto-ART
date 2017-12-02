namespace ART.Domotica.Repository.Interfaces.SI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities.SI;

    public interface IUnitMeasurementScaleRepository
    {
        #region Methods

        Task<List<UnitMeasurementScale>> GetAll();

        #endregion Methods
    }
}