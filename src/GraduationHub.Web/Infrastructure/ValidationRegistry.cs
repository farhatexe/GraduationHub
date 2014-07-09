using System.Reflection;
using FluentValidation;
using StructureMap.Configuration.DSL;

namespace GraduationHub.Web.Infrastructure
{
    public class ValidationRegistry : Registry
    {
        public ValidationRegistry()
        {
            AssemblyScanner.FindValidatorsInAssembly(Assembly.GetCallingAssembly())
                .ForEach(result => For(result.InterfaceType).Singleton().Use(result.ValidatorType));
        }
    }
}