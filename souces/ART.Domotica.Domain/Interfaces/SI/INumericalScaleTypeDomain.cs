namespace ART.Domotica.Domain.Interfaces.SI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities.SI;

    public interface INumericalScaleTypeDomain
    {
        #region Methods

        Task<List<NumericalScaleType>> GetAll();

        #endregion Methods
    }
}