namespace ART.Domotica.Repository.Interfaces.SI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities.SI;

    public interface INumericalScaleRepository
    {
        #region Methods

        Task<List<NumericalScale>> GetAll();

        #endregion Methods
    }
}