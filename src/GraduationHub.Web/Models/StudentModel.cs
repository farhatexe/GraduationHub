using System.Web.Mvc;
using GraduationHub.Web.Domain;

namespace GraduationHub.Web.Models
{
    public class StudentModel
    {
        [HiddenInput]
        public string Id { get; set; }

        public StudentPictureType StudentPictureType { get; set; }

        public StudentExpressionType StudentExpressionType { get; set; }

        public int TextMaxLength { get; set; }
    }
}