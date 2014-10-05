using System;
using ShortBus;
using ShortBus.StructureMap;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace GraduationHub.Web.Infrastructure
{
    public class MediatorRegistry : Registry
    {
        public MediatorRegistry(Func<IContainer> containerFactory)
        {
            IContainer container = containerFactory();
            Scan(s =>
            {
                s.AssemblyContainingType<IMediator>();
                s.TheCallingAssembly();
                s.WithDefaultConventions();
                s.AddAllTypesOf((typeof (IRequestHandler<,>)));
                s.AddAllTypesOf(typeof (INotificationHandler<>));
            });

            For<IDependencyResolver>().Use(() => DependencyResolver.Current);

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }
}