using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure;
using ShortBus;

namespace GraduationHub.Web.Notifications
{
    public class SaveGradInfo
    {
        public string Name { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string StudentEmail { get; set; }

        public string ParentEmail { get; set; }

        public bool FineArts { get; set; }

        public bool AcademicClasses { get; set; }

        public bool WillParticipateInGraduation { get; set; }

        public bool? TakenKeysWorldView { get; set; }

        public bool? TakenApprovedWorldView { get; set; }

        public bool? WillSecureAnnouncements { get; set; }

        public bool? NeedCapAndGown { get; set; }

        public string Height { get; set; }
    }

    public class SaveGradInfoHandler : INotificationHandler<SaveGradInfo>
    {
        private readonly ICurrentUser _currentUser;
        private readonly ApplicationDbContext _dbContext;

        public SaveGradInfoHandler(ApplicationDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public void Handle(SaveGradInfo notification)
        {
            GraduateInformation information = _dbContext.GraduateInformation
                .SingleOrDefault(i => i.StudentId == _currentUser.User.Id)
                                              ?? new GraduateInformation {StudentId = _currentUser.User.Id};

            information.Name = notification.Name;
            information.Street = notification.Street;
            information.City = notification.City;
            information.StudentEmail = notification.StudentEmail;
            information.ParentEmail = notification.ParentEmail;
            information.FineArts = notification.FineArts;
            information.AcademicClasses = notification.AcademicClasses;
            information.WillParticipateInGraduation = notification.WillParticipateInGraduation;
            information.TakenKeysWorldView = notification.TakenKeysWorldView;
            information.TakenApprovedWorldView = notification.TakenApprovedWorldView;
            information.WillSecureAnnouncements = notification.WillSecureAnnouncements;
            information.NeedCapAndGown = notification.NeedCapAndGown;
            information.Height = notification.Height;

            if (information.Id == default(int))
            {
                _dbContext.GraduateInformation.Add(information);
            }
            else
            {
                ObjectStateManager objectStateManager =
                    ((IObjectContextAdapter)_dbContext).ObjectContext.ObjectStateManager;
                _dbContext.GraduateInformation.Attach(information);
                objectStateManager.ChangeObjectState(information, EntityState.Modified);
            }

            _dbContext.SaveChanges();
        }
    }
}