using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GraduationHub.Web.Data;

namespace GraduationHub.Web.Domain
{
    public class Invitation
    {
        public int Id { get; set; }

        [StringLength(FieldLengths.Invitation.InviteeName)]
        public string InviteeName { get; set; }

        [StringLength(FieldLengths.Invitation.Email)]
        public string Email { get; set; }

        public Guid? InviteCode { get; set; }

        public bool HasBeenRedeemed { get; set; }

        public bool HasBeenSent { get; set; }

        public bool IsTeacher { get; set; }


        public string DisplayName
        {
            get
            {
                try
                {
                    
                     return string.Join(" ", InviteeName.Split(',').Reverse()).Trim();
                }
                catch (Exception)
                {
                    return InviteeName.Replace(",", "");
                }
            }
        }
    }

}