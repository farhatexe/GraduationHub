using System;
using System.Web.Mvc;

namespace GraduationHub.Web.Infrastructure.ModelMetadata
{
    public class WatermarkAttribute : Attribute, IMetadataAware
    {

        public string PlaceHolder { get; set; }

        public void OnMetadataCreated(System.Web.Mvc.ModelMetadata metadata)
        {
            metadata.Watermark = PlaceHolder;
        }
    }
}