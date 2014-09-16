using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GraduationHub.Web.Models.StudentExpressions
{
    public class StudentExpressionCreateModel
    {
        public int Id { get; set; }

        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [HiddenInput]
        public int TextMaxLength { get; set; }
    }
}