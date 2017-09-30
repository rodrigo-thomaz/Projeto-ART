using ART.MQ.Consumer.Entities;

namespace ART.MQ.Consumer.IRepositories
{
    public interface IRepository<TEntity, TKey>
        where TKey : struct
        where TEntity : IEntity<TKey>
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity GetById(TKey key);
    }
}
