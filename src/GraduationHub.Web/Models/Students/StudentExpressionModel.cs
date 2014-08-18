using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.Students
{
    public class StudentExpressionModel : IMapFrom<StudentExpression>
    {
        [Required]
        public string Text { get; set; }
    }
}