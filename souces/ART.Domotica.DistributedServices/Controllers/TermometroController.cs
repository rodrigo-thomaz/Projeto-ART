using ART.Domotica.DistributedServices.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace ART.Domotica.DistributedServices.Controllers
{
    [Authorize]
    [RoutePrefix("api/termometro")]
    public class TermometroController : ApiController
    {
        [Route("")]
        public IEnumerable<object> Get()
        {
            var context = new ARTDbContext();           

            var sensor1 = context.DSFamilyTempSensor.SingleOrDefault(x => x.DeviceAddress.ToLower() == "28ffe76da2163d3".ToLower());
            var sensor2 = context.DSFamilyTempSensor.SingleOrDefault(x => x.DeviceAddress.ToLower() == "28fffe6593164b6".ToLower());

            var tempscale = context.TemperatureScale.FirstOrDefault();

            if (sensor1 == null)
            {
                sensor1 = new DSFamilyTempSensor
                {
                    DeviceAddress = "28ffe76da2163d3",
                    Family = "DS18B20",
                    TemperatureScale = tempscale,                   
                };
                context.DSFamilyTempSensor.Add(sensor1);
            }

            if (sensor2 == null)
            {
                sensor2 = new DSFamilyTempSensor
                {
                    DeviceAddress = "28fffe6593164b6",
                    Family = "DS18B20",
                    TemperatureScale = tempscale,
                };
                context.DSFamilyTempSensor.Add(sensor2);
            }
            context.SaveChanges();
            //string token = "";
            //Microsoft.Owin.Security.AuthenticationTicket ticket = Startup.OAuthBearerOptions.AccessTokenFormat.Unprotect(token);

            var identity = User.Identity as ClaimsIdentity;
           
            return identity.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            });
        }
    }
}
