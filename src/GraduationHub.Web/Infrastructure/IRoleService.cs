namespace GraduationHub.Web.Infrastructure
{
    public interface IRoleService
    {
        bool IsAuthenticated();

        bool IsInRole(string role);

        bool IsTeacher();

        bool IsStudent();
    }
}