using System.Linq;
using AutoMapper.QueryableExtensions;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure;
using GraduationHub.Web.Models.CheckList;
using ShortBus;

namespace GraduationHub.Web.Requests
{
    public class GetExpression : IRequest<StudentExpressionModel>
    {
        public int MaxLength { get; set; }

        public StudentExpressionType Type { get; set; }
    }

    public class GetExpressionHandler : IRequestHandler<GetExpression, StudentExpressionModel>
    {
        private readonly ICurrentUser _currentUser;
        private readonly ApplicationDbContext _dbContext;

        public GetExpressionHandler(ApplicationDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public StudentExpressionModel Handle(GetExpression request)
        {
            StudentExpressionModel expression = _dbContext.StudentExpressions
                .Where(e => e.StudentId.Equals(_currentUser.User.Id))
                .Where(e => e.Type == request.Type).Project().To<StudentExpressionModel>()
                .SingleOrDefault() ?? new StudentExpressionModel();

            expression.TextMaxLength = request.MaxLength;

            return expression;
        }
    }
}