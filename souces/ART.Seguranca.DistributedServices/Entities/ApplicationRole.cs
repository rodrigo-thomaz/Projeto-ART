namespace ART.Seguranca.DistributedServices.Entities
{
    using System;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole>
    {
    }
}