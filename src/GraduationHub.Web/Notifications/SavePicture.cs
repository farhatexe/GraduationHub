using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure;
using ShortBus;

namespace GraduationHub.Web.Notifications
{
    public class SavePicture
    {
        public SavePicture(HttpPostedFileBase file, StudentPictureType type)
        {
            File = file;
            Type = type;
        }

        public HttpPostedFileBase File { get; private set; }

        public StudentPictureType Type { get; private set; }

        public bool HasImage()
        {
            return File != null && File.ContentLength > 0;
        }

        public byte[] ImageData
        {
            get
            {
                if (!HasImage())
                    return null;

                using (var binaryReader = new BinaryReader(File.InputStream))
                {
                    return binaryReader.ReadBytes(File.ContentLength);
                }
            }
        }

        public string MimeType
        {
            get
            {
                return MimeMapping.GetMimeMapping(File.FileName);
            }
        }

        public string FileName
        {
            get { return File.FileName; }
        }
    }

    public class SavePictureHandler : INotificationHandler<SavePicture>
    {
        private readonly ICurrentUser _currentUser;
        private readonly ApplicationDbContext _dbContext;

        public SavePictureHandler(ApplicationDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public void Handle(SavePicture notification)
        {
            if (!notification.HasImage())
            {
                throw new InvalidOperationException("No Image was uploaded.");
            }

            StudentPicture studentPicture = _dbContext.StudentPictures
                .Where(e => e.StudentId.Equals(_currentUser.User.Id))
                .SingleOrDefault(e => e.ImageType == notification.Type) ??
                                            new StudentPicture {ImageType = notification.Type};

            studentPicture.StudentId = _currentUser.User.Id;
            studentPicture.DateSubmitted = DateTime.Now;
            studentPicture.ImageName = notification.FileName;
            studentPicture.ImageData = notification.ImageData;
            studentPicture.MimeType = notification.MimeType;

            if (studentPicture.Id == default(int))
            {
                _dbContext.StudentPictures.Add(studentPicture);
            }
            else
            {
                ObjectStateManager objectStateManager =
                    ((IObjectContextAdapter)_dbContext).ObjectContext.ObjectStateManager;
                _dbContext.StudentPictures.Attach(studentPicture);
                objectStateManager.ChangeObjectState(studentPicture, EntityState.Modified);
            }

            _dbContext.SaveChanges();
        }
    }
}