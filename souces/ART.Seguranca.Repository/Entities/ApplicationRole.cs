namespace ART.Seguranca.Repository.Entities
{
    using System;

    using Microsoft.AspNet.Identity.EntityFramework;
    using ART.Infra.CrossCutting.Repository;

    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole>, IEntity<Guid>
    {
    }
}