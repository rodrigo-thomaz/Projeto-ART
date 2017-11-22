namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class ApplicationRepository : RepositoryBase<ARTDbContext, Application, Guid>, IApplicationRepository
    {
        #region Constructors

        public ApplicationRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}