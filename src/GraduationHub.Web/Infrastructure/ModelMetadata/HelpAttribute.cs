using System;
using System.Web.Mvc;

namespace GraduationHub.Web.Infrastructure.ModelMetadata
{
    public class HelpAttribute : Attribute, IMetadataAware
    {
        private readonly string _helpMessage;

        public HelpAttribute(string helpMessage)
        {
            _helpMessage = helpMessage;
        }

        public void OnMetadataCreated(System.Web.Mvc.ModelMetadata metadata)
        {
            metadata.AdditionalValues.Add("_HELP", _helpMessage);
        }
    }
}