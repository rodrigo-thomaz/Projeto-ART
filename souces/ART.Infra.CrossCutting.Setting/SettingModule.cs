namespace ART.Infra.CrossCutting.Setting
{
    using Autofac;

    public class SettingModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SettingManager>().As<ISettingManager>();
        }

        #endregion Methods
    }
}