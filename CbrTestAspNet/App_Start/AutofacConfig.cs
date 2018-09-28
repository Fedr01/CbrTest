using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using CbrTestAspNet.Domain;

namespace CbrTestAspNet
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<CbrClient>().As<ICbrClient>().SingleInstance();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}