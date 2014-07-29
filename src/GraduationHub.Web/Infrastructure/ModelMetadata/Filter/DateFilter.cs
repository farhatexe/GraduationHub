using System;
using System.Collections.Generic;

namespace GraduationHub.Web.Infrastructure.ModelMetadata.Filter
{
    // see: http://bit.ly/1o80jkF for explanation
    public class DateFilter : IModelMetadataFilter
    {
        public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes)
        {
            if (metadata.ModelType.Name == "DateTime" &&
                string.IsNullOrEmpty(metadata.DisplayFormatString))
            {
                metadata.DisplayFormatString = "{0:yyyy-MM-dd}";
            }

            if (metadata.ModelType.Name == "DateTime" &&
                string.IsNullOrEmpty(metadata.EditFormatString))
            {
                metadata.EditFormatString = "{0:yyyy-MM-dd}";
            }
        }
    }
}