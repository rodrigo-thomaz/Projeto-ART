namespace ART.Infra.CrossCutting.Repository
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;

    public interface IRepository<TDbContext, TEntity, TKey>
        where TDbContext : DbContext
        where TEntity : IEntity<TKey>
        where TKey : struct
    {
        #region Methods

        Task Delete(TEntity entity);

        Task Delete(List<TEntity> entities);

        Task<TEntity> GetByKey(TKey key);

        Task Insert(TEntity entity);

        Task Insert(List<TEntity> entities);

        Task Update(TEntity entity);

        Task Update(List<TEntity> entities);

        #endregion Methods
    }
}