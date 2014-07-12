using System;

namespace GraduationHub.Web.Domain
{
    public class Invitation
    {
        public int Id { get; set; }

        public string StudentName { get; set; }

        public int GraduatingClassId { get; set; }

        public virtual GraduatingClass GraduatingClass { get; set; }

        public string Email { get; set; }

        public Guid? InviteCode { get; set; }

        public bool HasBeenRedeemed { get; set; }

        public bool HasBeenSent { get; set; }
    }
}