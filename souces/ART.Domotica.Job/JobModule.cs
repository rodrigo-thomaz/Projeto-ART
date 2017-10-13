namespace ART.Domotica.Job
{
    using Autofac;
    using Autofac.Extras.Quartz;
    using Quartz;

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