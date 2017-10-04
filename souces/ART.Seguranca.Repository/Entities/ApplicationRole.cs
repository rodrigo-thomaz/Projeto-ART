namespace ART.Seguranca.Repository.Entities
{
    using System;

    using ART.Infra.CrossCutting.Repository;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole>, IEntity<Guid>
    {
    }
}