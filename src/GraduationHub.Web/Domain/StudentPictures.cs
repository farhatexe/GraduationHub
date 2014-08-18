using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationHub.Web.Data;

namespace GraduationHub.Web.Domain
{
    public class StudentPicture
    {
        public int Id { get; set; }

        [ForeignKey("StudentId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string StudentId { get; set; }

        [Required, StringLength(FieldLengths.StudentPictures.ImageName)]
        public string ImageName { get; set; }
        
        [StringLength(FieldLengths.StudentPictures.Description)]
        public string Description { get; set; }

        public StudentPictureType ImageType { get; set; }

        public Byte[] ImageData { get; set; }

        public DateTime DateSubmitted { get; set; }
    }
}