using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace ART.Seguranca.DistributedServices.Entities
{
    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole>
    {
    }
}