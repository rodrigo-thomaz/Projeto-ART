namespace ART.Domotica.Domain.Interfaces
{
    using System.Threading.Tasks;

    public interface IHardwareDomain
    {
        #region Methods

        Task UpdatePins();

        #endregion Methods
    }
}