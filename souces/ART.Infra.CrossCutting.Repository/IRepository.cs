namespace ART.Infra.CrossCutting.Repository
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    public interface IRepository<TDbContext, TEntity, TKey>

        where TDbContext : DbContext
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