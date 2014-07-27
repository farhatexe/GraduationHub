using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentValidation.Mvc;
using GraduationHub.Web.Data;
using GraduationHub.Web.Infrastructure;
using GraduationHub.Web.Infrastructure.ModelMetadata;
using GraduationHub.Web.Infrastructure.Tasks;
using GraduationHub.Web.Migrations;
using StructureMap;

namespace GraduationHub.Web
{
    public class MvcApplication : HttpApplication
    {
        public IContainer Container
        {
            get { return (IContainer) HttpContext.Current.Items["_Container"]; }
            set { HttpContext.Current.Items["_Container"] = value; }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

            DependencyResolver.SetResolver(
                new StructureMapDependencyResolver(() => Container ?? ObjectFactory.Container));

            ObjectFactory.Configure(cfg =>
            {
                cfg.AddRegistry(new StandardRegistry());
                cfg.AddRegistry(new ControllerRegistry());
                cfg.AddRegistry(new ActionFilterRegistry(
                    () => Container ?? ObjectFactory.Container));
                cfg.AddRegistry(new MvcRegistry(() => Container ?? ObjectFactory.Container));
                cfg.AddRegistry(new TaskRegistry());
                cfg.AddRegistry(new ModelMetadataRegistry());
                cfg.AddRegistry(new ValidationRegistry());
            });

            var validatorFactory = new StructureMapValidatorFactory(() => Container ?? ObjectFactory.Container);
            var fluentValidationModelValidatorProvider = new FluentValidationModelValidatorProvider(validatorFactory);


            ModelValidatorProviders.Providers.Add(fluentValidationModelValidatorProvider);
            //DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = 
                fluentValidationModelValidatorProvider.AddImplicitRequiredValidator = false;

            using (IContainer container = ObjectFactory.Container.GetNestedContainer())
            {
                foreach (IRunAtInit task in container.GetAllInstances<IRunAtInit>())
                {
                    task.Execute();
                }

                foreach (IRunAtStartup task in container.GetAllInstances<IRunAtStartup>())
                {
                    task.Execute();
                }
            }
        }


        public void Application_BeginRequest()
        {
            Container = ObjectFactory.Container.GetNestedContainer();

            foreach (IRunOnEachRequest task in Container.GetAllInstances<IRunOnEachRequest>())
            {
                task.Execute();
            }
        }

        public void Application_Error()
        {
            foreach (IRunOnError task in Container.GetAllInstances<IRunOnError>())
            {
                task.Execute();
            }
        }

        public void Application_EndRequest()
        {
            try
            {
                foreach (IRunAfterEachRequest task in
                    Container.GetAllInstances<IRunAfterEachRequest>())
                {
                    task.Execute();
                }
            }
            finally
            {
                Container.Dispose();
                Container = null;
            }
        }
    }
}