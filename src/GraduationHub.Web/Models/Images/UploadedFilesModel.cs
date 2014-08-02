using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.Mapping;

namespace GraduationHub.Web.Models.Images
{
    public class UploadedFilesModel : IMapFrom<StudentPictures>
    {
        public int Id { get; set; }

        public string ImageName { get; set; }
    }
}