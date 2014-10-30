using System;
using System.Linq;
using GraduationHub.Web.Data;
using GraduationHub.Web.Models.GraduateInformation;
using ShortBus;

namespace GraduationHub.Web.Requests
{
    [Obsolete("Did not use. Can be deleted.")]
    public class GetGraduateInformation : IRequest<IQueryable<GraduateIndexModel>>
    {
        public GetGraduateInformation()
        {
            Sql = @"
SELECT 
	a.InviteeName StudentName,
	Completed = CASE WHEN c.Id Is Null THEN 0 ELSE 1 END,
	c.Name DiplomaName,
	c.Street,
	c.City,
	c.StudentEmail,
	c.ParentEmail,
	c.FineArts,
	c.AcademicClasses,
	c.WillParticipateInGraduation,
	c.TakenApprovedWorldView,
	c.TakenApprovedWorldView,
	c.WillSecureAnnouncements,
	c.NeedCapAndGown,
	c.Height
FROM Invitations a
LEFT JOIN AspNetUsers b on a.Email = b.Email
LEFT JOIN GraduateInformation c on b.Id = c.StudentId
WHERE a.IsTeacher = 0";
        }

        public string Sql { get; private set; }
    }

    public class GetGraduateInformationHandler :
        IRequestHandler<GetGraduateInformation, IQueryable<GraduateIndexModel>>
    {
        private readonly ApplicationDbContext _context;

        public GetGraduateInformationHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<GraduateIndexModel> Handle(GetGraduateInformation request)
        {
            {
                IQueryable<GraduateIndexModel> result =
                    _context.Database.SqlQuery<GraduateIndexModel>(request.Sql).AsQueryable();

                return result;
            }
        }
    }
}