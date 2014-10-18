using System;
using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Infrastructure.ModelMetadata;

namespace GraduationHub.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }
        
        [Required, Help("The email address in the Invitation Email.")]
        public string Email { get; set; }

        [Required, Help("The code from the Invitation Email.")]
        public string InviteCode { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Help("Passwords must have at least one non letter or digit character, at least one lowercase ('a'-'z') and at least one uppercase ('A'-'Z').")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        
    }
}