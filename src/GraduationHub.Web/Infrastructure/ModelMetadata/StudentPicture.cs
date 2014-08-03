using System;
using System.Web.Mvc;
using GraduationHub.Web.Domain;

namespace GraduationHub.Web.Infrastructure.ModelMetadata
{
    public class StudentPicture : Attribute, IMetadataAware
    {
        private readonly StudentPictureType _studentPictureType;

        public StudentPicture(StudentPictureType studentPictureType)
        {
            _studentPictureType = studentPictureType;
        }

        public void OnMetadataCreated(System.Web.Mvc.ModelMetadata metadata)
        {
            metadata.AdditionalValues.Add("_STUDENTPICTURETYPE", _studentPictureType);
        }
    }
}