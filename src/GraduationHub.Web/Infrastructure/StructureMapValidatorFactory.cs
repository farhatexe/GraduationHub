using System;
using FluentValidation;
using StructureMap;

namespace GraduationHub.Web.Infrastructure
{
    public class StructureMapValidatorFactory : ValidatorFactoryBase
    {
        private readonly Func<IContainer> _containerFactory;

        public StructureMapValidatorFactory(Func<IContainer> containerFactory)
        {
            _containerFactory = containerFactory;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return _containerFactory().TryGetInstance(validatorType) as IValidator;
        }
    }
}