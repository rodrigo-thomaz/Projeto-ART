using System;
using System.Web.Http;
using RThomaz.Domain.Financeiro.Services.InitialData;
using System.Threading.Tasks;
using RThomaz.DistributedServices.Core;

namespace RThomaz.DistributedServices.Financeiro.Controllers
{
    [Authorize]
    [RoutePrefix("api/initialdata")]
    public class InitialDataController : BaseApiController
    {
        /// <summary>
        /// Cria Storage do Sistema
        /// </summary>
        /// <remarks>Cria Storage do Sistema</remarks>
        /// <response code="500">Internal Server Error</response>
        [Route("consisteStorage")]
        public async Task<IHttpActionResult> ConsisteStorage()
        {
            try
            {
                var storageInitialData = new StorageInitialData();
                await storageInitialData.Seed();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        /// <summary>
        /// Localidade Seed
        /// </summary>
        /// <remarks>Localidade Seed</remarks>
        /// <response code="500">Internal Server Error</response>
        [Route("seedLocalidade")]
        public async Task<IHttpActionResult> SeedLocalidade()
        {
            try
            {
                var localidadeInitialData = new LocalidadeInitialData();
                await localidadeInitialData.Seed();
                localidadeInitialData.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        /// <summary>
        /// CBO Seed
        /// </summary>
        /// <remarks>CBO Seed</remarks>
        /// <response code="500">Internal Server Error</response>
        [Route("seedCBO")]
        public async Task<IHttpActionResult> SeedCBO()
        {
            try
            {
                var cboInitialData = new CBOInitialData();
                await cboInitialData.Seed();
                cboInitialData.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }
    }
}
