using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationHub.Web.Domain
{
    public class StudentExpression
    {
        public int Id { get; set; }

        public string StudentId { get; set; }

        [ForeignKey("StudentId")]
        public virtual ApplicationUser Student { get; set; }

        public StudentExpressionType Type { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime DateSubmitted { get; set; }
    }

    public enum StudentExpressionType
    {
        Biography,
        ThankYou,
        SlideshowCaption
    }




}