using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.StudentExpressions
{
    public class StudentExpressionEditModel : IMapFrom<StudentExpression>
    {
        [HiddenInput]
        public int Id { get; set; }

        public StudentExpressionType Type { get; set; }

        [DataType(DataType.MultilineText)]
        public string Text { get; set; }



    }
}