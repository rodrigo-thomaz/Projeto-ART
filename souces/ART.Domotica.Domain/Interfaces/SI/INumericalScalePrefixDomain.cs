namespace ART.Domotica.Domain.Interfaces.SI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities.SI;

    public interface INumericalScalePrefixDomain
    {
        #region Methods

        Task<List<NumericalScalePrefix>> GetAll();

        #endregion Methods
    }
}