using ART.MQ.Consumer.Entities;
using ART.MQ.Consumer.IRepositories;
using System.Data.Entity;

namespace ART.MQ.Consumer.Repositories
{
    public abstract class RepositoryBase<TEntity, TKey>
        : IRepository<TEntity, TKey>

        where TEntity : class, IEntity<TKey>, new()
        where TKey : struct

    {
        private ARTDbContext _context;

        public RepositoryBase(ARTDbContext context)
        {
            _context = context;
        }        

        public TEntity GetById(TKey key)
        {
            var entity = _context.Set<TEntity>().Find(key);
            return entity;
        }

        public void Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }
        
        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
