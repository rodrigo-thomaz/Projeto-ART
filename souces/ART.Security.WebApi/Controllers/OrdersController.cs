namespace ART.Security.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    public class Order
    {
        #region Properties

        public string CustomerName
        {
            get; set;
        }

        public Boolean IsShipped
        {
            get; set;
        }

        public int OrderID
        {
            get; set;
        }

        public string ShipperCity
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public static List<Order> CreateOrders()
        {
            List<Order> OrderList = new List<Order>
            {
                new Order {OrderID = 10248, CustomerName = "Taiseer Joudeh", ShipperCity = "Amman", IsShipped = true },
                new Order {OrderID = 10249, CustomerName = "Ahmad Hasan", ShipperCity = "Dubai", IsShipped = false},
                new Order {OrderID = 10250,CustomerName = "Tamer Yaser", ShipperCity = "Jeddah", IsShipped = false },
                new Order {OrderID = 10251,CustomerName = "Lina Majed", ShipperCity = "Abu Dhabi", IsShipped = false},
                new Order {OrderID = 10252,CustomerName = "Yasmeen Rami", ShipperCity = "Kuwait", IsShipped = true}
            };

            return OrderList;
        }

        #endregion Methods
    }

    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        #region Methods

        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            //ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            //var Name = ClaimsPrincipal.Current.Identity.Name;
            //var Name1 = User.Identity.Name;

            //var userName = principal.Claims.Where(c => c.Type == "sub").Single().Value;

            return Ok(Order.CreateOrders());
        }

        #endregion Methods
    }
}