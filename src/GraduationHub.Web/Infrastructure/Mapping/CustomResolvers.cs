using AutoMapper;

namespace GraduationHub.Web.Infrastructure.Mapping
{
    public class YesNoResolver : ValueResolver<bool, string>
    {
        protected override string ResolveCore(bool source)
        {
            return source ? "Yes" : "No";
        }
    }
}