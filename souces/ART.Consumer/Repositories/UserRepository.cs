using ART.Consumer.Entities;
using System;
using System.Threading.Tasks;

namespace ART.Consumer.Repositories
{
    public class UserRepository : IDisposable
    {
        #region private readonly fields

        private readonly ARTDbContext _context;

        #endregion

        #region constructors

        public UserRepository()
        {
            _context = new ARTDbContext();
        }

        #endregion

        #region public voids

        public async Task Insert(User entity)
        {
            _context.User.Add(entity);
            await _context.SaveChangesAsync();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _context.Dispose();
        }        

        #endregion
    }
}