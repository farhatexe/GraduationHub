using System.Linq;
using System.Web.Helpers;
using System.Web.Hosting;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure;
using ShortBus;

namespace GraduationHub.Web.Requests
{
    public class GetPicture : IRequest<WebImage>
    {
        public StudentPictureType Type { get; set; }
    }

    public class GetPictureHandler : IRequestHandler<GetPicture, WebImage>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICurrentUser _currentUser;

        public GetPictureHandler(ApplicationDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public WebImage Handle(GetPicture request)
        {
            StudentPicture studentPicture = _dbContext.StudentPictures
                .Where(e => e.StudentId.Equals(_currentUser.User.Id))
                .SingleOrDefault(e => e.ImageType == request.Type);

            return studentPicture == null ? 
                new WebImage(HostingEnvironment.MapPath(@"~/Content/images/male_silhouette.png")) 
                : new WebImage(studentPicture.ImageData);
        }
    }
}