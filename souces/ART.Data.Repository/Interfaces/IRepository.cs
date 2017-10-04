namespace ART.Data.Repository.Interfaces
{
    using System.Threading.Tasks;

    using ART.Data.Repository.Entities;

    public interface IRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
        where TKey : struct
    {
        #region Methods

        Task Delete(TEntity entity);

        Task<TEntity> GetById(TKey key);

        Task Insert(TEntity entity);

        Task Update(TEntity entity);

        #endregion Methods
    }
}