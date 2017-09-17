using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RThomaz.Infra.Data.Core
{
    public class RepositoryBase<TEntity, TDbContext> : IDisposable, IRepositoryBase<TEntity> 
        where TEntity : class
        where TDbContext : DbContext
    {
        protected readonly TDbContext _context;

        public RepositoryBase()
        {
            _context = Activator.CreateInstance<TDbContext>();
        }

        public void Add(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public Guid AplicacaoId { get; set; }
    }
}
