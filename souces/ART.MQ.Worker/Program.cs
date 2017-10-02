using ART.Data.Domain;
using ART.Data.Repository;
using ART.MQ.Worker.Modules;
using Autofac;
using Topshelf;
using Topshelf.Autofac;

namespace ART.MQ.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();            

            builder.RegisterType<ARTDbContext>().InstancePerLifetimeScope();

            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<BusModule>();
            builder.RegisterModule<ConsumerModule>();

            IContainer container = builder.Build();

            HostFactory.Run(c =>
            {
                c.SetServiceName("ART.MQ.Worker");
                c.SetDisplayName("ART MQ Worker");
                c.SetDescription("A ART MQ Worker.");
                
                c.UseAutofacContainer(container);

                c.Service<WorkerService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted((service, control) => service.Start());
                    s.WhenStopped((service, control) => service.Stop());
                });              
            });            
        }
    }    
}
