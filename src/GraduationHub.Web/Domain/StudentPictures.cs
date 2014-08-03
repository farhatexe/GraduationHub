using System;
using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Data;

namespace GraduationHub.Web.Domain
{
    public class StudentPictures
    {
        public int Id { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required, StringLength(FieldLengths.StudentPictures.ImageName)]
        public string ImageName { get; set; }
        
        [StringLength(FieldLengths.StudentPictures.Description)]
        public string Description { get; set; }

        public StudentPictureType ImageType { get; set; }

        public Byte[] ImageData { get; set; }

        public DateTime DateSubmitted { get; set; }
    }
}