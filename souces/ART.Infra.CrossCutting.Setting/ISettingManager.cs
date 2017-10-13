namespace ART.Infra.CrossCutting.Setting
{
    using System;
    using System.Threading.Tasks;

    public interface ISettingManager : IDisposable
    {
        #region Methods

        void Delete(string key);

        Task DeleteAsync(string key);

        bool Exist(string key);

        Task<bool> ExistAsync(string key);

        T GetValue<T>(string key);

        Task<T> GetValueAsync<T>(string key);

        void Insert<T>(string key, T value);

        Task InsertAsync<T>(string key, T value);

        void SetValue<T>(string key, T value);

        Task SetValueAsync<T>(string key, T value);

        #endregion Methods
    }
}