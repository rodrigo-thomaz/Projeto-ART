namespace ART.Infra.CrossCutting.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
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

        IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);

        #endregion Methods
    }
}