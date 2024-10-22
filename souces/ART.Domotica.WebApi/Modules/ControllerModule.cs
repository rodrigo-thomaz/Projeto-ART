﻿namespace ART.Domotica.WebApi.Modules
{
    using System.Reflection;

    using ART.Infra.CrossCutting.Logging;

    using Autofac;
    using Autofac.Extras.DynamicProxy;
    using Autofac.Integration.WebApi;

    public class ControllerModule : Autofac.Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers("Controller", Assembly.GetExecutingAssembly())
                .EnableClassInterceptors()
                .InterceptedBy(typeof(CallDebugLogger));
        }

        #endregion Methods
    }
}