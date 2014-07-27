using System;
using System.Data.Entity;
using System.Threading.Tasks;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;

namespace GraduationHub.Web.Services
{
    public class InvitationManager : IInvitationManager
    {
        private readonly ApplicationDbContext _dbContext;

        public InvitationManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Invitation> GetInvitation(string email, int graduatingClassId, string inviteCode)
        {
            return await
                _dbContext.Invitations
                    .SingleOrDefaultAsync(i => i.InviteCode == new Guid(inviteCode) &&
                                               i.Email == email &&
                                               i.GraduatingClassId == graduatingClassId);
        }
    }

    public interface IInvitationManager
    {
        Task<Invitation> GetInvitation(string email, int graduatingClassId, string inviteCode);
    }
}