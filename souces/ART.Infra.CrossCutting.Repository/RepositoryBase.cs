using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ART.Infra.CrossCutting.Repository
{
    public abstract class RepositoryBase<TDbContext, TEntity, TKey>
        : IRepository<TDbContext, TEntity, TKey>

        where TDbContext : DbContext
        where TEntity : class, IEntity<TKey>//, new()
        where TKey : struct

    {
        protected TDbContext _context;
        
        public RepositoryBase(TDbContext context)
        {
            _context = context;
        }        

        public async Task<TEntity> GetByKey(TKey key)
        {
            var entity = await _context.Set<TEntity>().FindAsync(key);
            return entity;
        }

        public async Task Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Insert(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Set<TEntity>().Add(entity);
            }            
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Update(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }            
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }
    }
}