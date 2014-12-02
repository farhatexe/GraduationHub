using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.CheckList
{
    public class StudentExpressionModel : IMapFrom<StudentExpression>
    {
        [HiddenInput]
        public string StudentId { get; set; }

        [DataType(DataType.MultilineText), Required]
        public string Text { get; set; }

        [HiddenInput]
        public int TextMaxLength { get; set; }
    }
}