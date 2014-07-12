using System;
using System.Collections.Generic;

namespace GraduationHub.Web.Infrastructure.ModelMetadata.Filter
{
    public class GraduatingClassIdDropDownFilter : IModelMetadataFilter
    {
        public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes)
        {
            if (!string.IsNullOrEmpty(metadata.PropertyName) &&
                string.IsNullOrEmpty(metadata.DataTypeName) &&
                metadata.PropertyName.ToLower().Contains("graduatingclassid"))
            {
                metadata.DataTypeName = "GraduatingClassId";
            }
        }
    }
}