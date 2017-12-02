namespace ART.Domotica.Domain.Interfaces.SI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities.SI;

    public interface INumericalScaleDomain
    {
        #region Methods

        Task<List<NumericalScale>> GetAll();

        #endregion Methods
    }
}