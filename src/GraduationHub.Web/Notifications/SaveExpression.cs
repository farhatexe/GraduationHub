using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using ShortBus;

namespace GraduationHub.Web.Notifications
{
    public class SaveExpression
    {
        public string StudentId { get; set; }

        public string Text { get; set; }

        public StudentExpressionType Type { get; set; }
    }

    public class SaveExpressionHandler : INotificationHandler<SaveExpression>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveExpressionHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(SaveExpression notification)
        {
            StudentExpression studentExpression = _dbContext.StudentExpressions
                .Where(e => e.StudentId.Equals(notification.StudentId))
                .SingleOrDefault(e => e.Type == notification.Type) ??
                                                  new StudentExpression {Type = notification.Type};

            studentExpression.StudentId = notification.StudentId;
            studentExpression.Text = notification.Text;

            if (studentExpression.Id == default(int))
            {
                _dbContext.StudentExpressions.Add(studentExpression);
            }
            else
            {
                ObjectStateManager objectStateManager =
                    ((IObjectContextAdapter) _dbContext).ObjectContext.ObjectStateManager;
                _dbContext.StudentExpressions.Attach(studentExpression);
                objectStateManager.ChangeObjectState(studentExpression, EntityState.Modified);
            }

            _dbContext.SaveChanges();
        }
    }
}