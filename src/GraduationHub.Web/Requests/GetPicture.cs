using System.Linq;
using System.Web.Helpers;
using System.Web.Hosting;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using ShortBus;

namespace GraduationHub.Web.Requests
{
    public class GetPicture : IRequest<WebImage>
    {
        public StudentPictureType Type { get; set; }
        public string UserId { get; set; }
    }

    public class GetPictureHandler : IRequestHandler<GetPicture, WebImage>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetPictureHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public WebImage Handle(GetPicture request)
        {
            StudentPicture studentPicture = _dbContext.StudentPictures
                .Where(e => e.StudentId.Equals(request.UserId))
                .SingleOrDefault(e => e.ImageType == request.Type);

            return studentPicture == null ? 
                new WebImage(HostingEnvironment.MapPath(@"~/Content/images/male_silhouette.png")) 
                : new WebImage(studentPicture.ImageData);
        }
    }
}