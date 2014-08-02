using System.Web.Mvc;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.StudentExpressions
{
    public class StudentExpressionDeleteModel : IMapFrom<StudentExpression>
    {
        [HiddenInput]
        public int Id { get; set; }

        public string Text { get; set; }

        public StudentExpressionType StudentExpressionType { get; set; }
    }
}