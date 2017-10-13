namespace ART.Infra.CrossCutting.Setting
{
    using System;
    using System.Threading.Tasks;

    public interface ISettingManager : IDisposable
    {
        #region Methods

        Task<bool> Exist(string key);

        Task Delete(string key);

        Task<T> GetValue<T>(string key);

        Task Insert(string key);

        Task SetValue<T>(string key, T value);

        #endregion Methods
    }
}