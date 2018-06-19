using BasicApiExample.Filter;
using BasicApiExample.Repository.TestModelRepository;
using Schroders.Contracts;
using Services.TestModelServices;
using System.Web.Http;
using Unity;
using Unity.AspNet.WebApi;
using Unity.Lifetime;

namespace BasicApiExample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new UniversalExceptionFilterAttribute());

            ConfigureIoC(config);
        }

        private static void ConfigureIoC(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<ITestModelService, TestModelService>();
            container.RegisterType<IRepository<TestModel>, InMemoryMockRepository>(new ContainerControlledLifetimeManager());
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
