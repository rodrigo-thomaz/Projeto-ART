namespace ART.Domotica.Domain.Interfaces.SI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities.SI;

    public interface IUnitMeasurementScaleDomain
    {
        #region Methods

        Task<List<UnitMeasurementScale>> GetAll();

        #endregion Methods
    }
}