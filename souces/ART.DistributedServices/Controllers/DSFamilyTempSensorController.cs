using ART.Infra.CrossCutting.WebApi;
using System.Web.Http;

namespace ART.DistributedServices.Controllers
{
    [Authorize]
    [RoutePrefix("api/dsFamilyTempSensor")]    
    public class DSFamilyTempSensorController : BaseApiController
    {
        //#region private readonly fields

        //private readonly IBancoAppService _bancoAppService;

        //#endregion

        //#region constructors

        //public DSFamilyTempSensorController(IBancoAppService bancoAppService)
        //{
        //    _bancoAppService = bancoAppService;
        //}

        //#endregion

        //#region public voids



        //#endregion
    }
}