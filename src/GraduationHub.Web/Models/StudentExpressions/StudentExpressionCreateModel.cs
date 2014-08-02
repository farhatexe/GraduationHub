using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Domain;

namespace GraduationHub.Web.Models.StudentExpressions
{
    public class StudentExpressionCreateModel
    {
        public StudentExpressionType Type { get; set; }

        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        
    }
}