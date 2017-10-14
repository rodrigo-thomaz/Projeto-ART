namespace ART.Domotica.Worker.Modules
{
    using ART.Domotica.Worker.Jobs;

    using Autofac;
    using Autofac.Extras.Quartz;

    public class JobModule : Autofac.Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(UpdatePinJob).Assembly));
        }

        #endregion Methods
    }
}