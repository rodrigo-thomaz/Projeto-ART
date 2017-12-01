﻿using System.Web.Http;
using System.Threading.Tasks;
using ART.Infra.CrossCutting.MQ.WebApi;
using ART.Domotica.Producer.Interfaces;

namespace ART.Domotica.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/unitMeasurementType")]    
    public class UnitMeasurementTypeController : AuthenticatedMQApiControllerBase
    {
        #region private readonly fields

        protected readonly IUnitMeasurementTypeProducer _unitMeasurementTypeProducer;

        #endregion

        #region constructors

        public UnitMeasurementTypeController(IUnitMeasurementTypeProducer unitMeasurementTypeProducer) 
        {
            _unitMeasurementTypeProducer = unitMeasurementTypeProducer;
        }

        #endregion

        #region public voids

        /// <summary>
        /// Retornar uma lista de tipos de unidade de medida
        /// </summary>        
        /// <remarks>
        /// Retornar uma lista de tipos de unidade de medida
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("getAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {           
            await _unitMeasurementTypeProducer.GetAll(CreateMessage());
            return Ok();
        }

        #endregion
    }
}