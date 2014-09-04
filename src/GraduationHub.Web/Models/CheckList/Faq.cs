using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.CheckList
{
    public class Faq : IMapFrom<FrequentlyAskedQuestion>
    {
        public string Question { get; set; }

        public string Answer { get; set; }
    }
}