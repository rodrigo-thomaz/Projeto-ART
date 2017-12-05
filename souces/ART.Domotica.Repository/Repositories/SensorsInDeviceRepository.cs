using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public interface IRepository2<TDbContext, TEntity>
        where TDbContext : DbContext
        where TEntity : IEntity<Guid>
    {
        #region Methods

        Task Delete(TEntity entity);

        Task Delete(List<TEntity> entities);

        Task<TEntity> GetByKey(params object[] keyValues);

        Task Insert(TEntity entity);

        Task Insert(List<TEntity> entities);

        Task Update(TEntity entity);

        Task Update(List<TEntity> entities);

        #endregion Methods
    }

    public abstract class RepositoryBase2<TDbContext, TEntity>
        : IRepository2<TDbContext, TEntity>

        where TDbContext : DbContext
        where TEntity : class, IEntity<Guid>//, new()
    {
        protected TDbContext _context;

        public RepositoryBase2(TDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetByKey(params object[] keyValues)
        {
            var entity = await _context.Set<TEntity>().FindAsync(keyValues);
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
    }
    public class SensorsInDeviceRepository : ISensorsInDeviceRepository
    {
        private readonly ARTDbContext _context;

        #region Constructors

        public SensorsInDeviceRepository(ARTDbContext context)            
        {
            _context = context;
        }

        #endregion Constructors

        public async Task<List<SensorsInDevice>> GetAllByApplicationId(Guid applicationId)
        {
            IQueryable<SensorsInDevice> query = from sid in _context.SensorsInDevice
                                                join dia in _context.DeviceInApplication on sid.DeviceSensorsId equals dia.DeviceBaseId
                                                where dia.ApplicationId == applicationId
                                                select sid;

            return await query.ToListAsync();
        }
    }
}
