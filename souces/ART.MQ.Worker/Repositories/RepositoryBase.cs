using ART.MQ.Worker.Entities;
using ART.MQ.Worker.IRepositories;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ART.MQ.Worker.Repositories
{
    public abstract class RepositoryBase<TEntity, TKey>
        : IRepository<TEntity, TKey>

        where TEntity : class, IEntity<TKey>, new()
        where TKey : struct

    {
        protected ARTDbContext _context;

        public RepositoryBase(ARTDbContext context)
        {
            _context = context;
        }        

        public async Task<TEntity> GetById(TKey key)
        {
            var entity = await _context.Set<TEntity>().FindAsync(key);
            return entity;
        }

        public async Task Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}