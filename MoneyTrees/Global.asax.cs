using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using MoneyTrees.Services;
using System.Reflection;
using Autofac.Integration.Mvc;
using MoneyTrees.DAL;
using MoneyTrees.Models;
using MoneyTrees.App_Start;
using System.Web.Optimization;
using Token = MoneyTrees.DAL.Token;
using MoneyTrees.Interfaces;

namespace MoneyTrees
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BuildContainer();

        }

        protected static void BuildContainer()
        {

            var containerBuilder = new ContainerBuilder();


            containerBuilder.RegisterType<Token>();
            containerBuilder.RegisterType<MonzoMapper>().As<IMonzoMapper>();
            containerBuilder.RegisterType<MonzoRepository>().As<IMonzoRepository>();
            containerBuilder.RegisterType<MonzoWebService>().As<IMonzoWebService>();
            containerBuilder.RegisterType<RestClientFactory>().As<IRestClientFactory>();
            containerBuilder.RegisterType<RestRequestFactory>().As<IRestRequestFactory>();
            containerBuilder.RegisterType<RestClientFactory>().As<IRestClientFactory>();


            containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where
                (t => t.Name.EndsWith("Controller"));

            var container = containerBuilder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }


    }
}
