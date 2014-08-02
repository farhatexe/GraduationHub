using GraduationHub.Web.Domain;

namespace GraduationHub.Web.Infrastructure
{
    public interface ICurrentUser
    {
        ApplicationUser User { get; }
    }
}