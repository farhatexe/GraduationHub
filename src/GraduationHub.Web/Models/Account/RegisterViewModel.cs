using System;
using System.ComponentModel.DataAnnotations;

namespace GraduationHub.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        public string InviteCode { get; set; }

        [Required, Display(Name = "Graduating Class")]
        public int GraduatingClassId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        
    }
}