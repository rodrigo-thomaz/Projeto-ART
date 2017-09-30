using ART.MQ.Consumer.Entities;
using System.Threading.Tasks;

namespace ART.MQ.Consumer.IRepositories
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
