using System;
using System.IO;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.Students
{
    public class StudentPictureModel : IMapFrom<StudentPicture>
    {
        public int Id { get; set; }

        public string ImageName { get; set; }

        public string Description { get; set; }

        public Byte[] ImageData { get; set; }

        public string ContentType
        {
            get { return string.Format("image/{0}", Path.GetExtension(ImageName).Replace(".","")); }
        }
    }
}