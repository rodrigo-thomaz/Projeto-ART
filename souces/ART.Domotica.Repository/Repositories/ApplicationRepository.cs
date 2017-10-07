namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class ApplicationRepository : RepositoryBase<ARTDbContext, Application, Guid>, IApplicationRepository
    {
        #region Constructors

        public ApplicationRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<List<Application>> GetAll(Guid applicationUserId)
        {
            //IQueryable<Application> query = from app in _context.Application
            //                                join userapp in _context.UsersInApplication on app.Id equals userapp.ApplicationId
            //                                where userapp.UserId == applicationUserId
            //                                select app;

            //return await query.ToListAsync();

            throw new NotImplementedException();
        }
    }
}