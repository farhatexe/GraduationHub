using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
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

        public async Task<Invitation> GetInvitation(string email, string inviteCode)
        {
            return await
                _dbContext.Invitations
                    .SingleOrDefaultAsync(i => i.InviteCode == new Guid(inviteCode) &&
                                   i.Email == email);
        }

        public async void RedeemCode(string inviteCode)
        {
            var invite = await _dbContext.Invitations
                    .SingleOrDefaultAsync(i => i.InviteCode == new Guid(inviteCode));

            if (invite == null) return;
            
            invite.HasBeenRedeemed = true;

            _dbContext.SaveChanges();
        }
    }

    public interface IInvitationManager
    {
        Task<Invitation> GetInvitation(string email, string inviteCode);
        void RedeemCode(string inviteCode);
    }
}