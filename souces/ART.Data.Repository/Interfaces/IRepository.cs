using ART.Data.Repository.Entities;
using System.Threading.Tasks;

namespace ART.Data.Repository.Interfaces
{
    public interface IRepository<TEntity, TKey>
        where TKey : struct
        where TEntity : IEntity<TKey>
    {
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task<TEntity> GetById(TKey key);
    }
}
