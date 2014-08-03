using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure;
using GraduationHub.Web.Models.Students;

namespace GraduationHub.Web.Controllers
{
    //TODO: Add the Authorize Student Attribute...they must be in the student role.
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        private List<StudentExpression> _studentExpressions;


        public StudentController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        // GET: Student
        public async Task<ActionResult> Index()
        {

            return View();
        }

        [ChildActionOnly]
        public ActionResult MyBiography()
        {
            var currentUser = _currentUser.User;

            var studentBiography = _context.StudentExpressions
                .Where(i => i.Type == StudentExpressionType.Biography && i.StudentId == currentUser.Id)
                .Project().To<StudentExpressionModel>().SingleOrDefault() ?? new StudentExpressionModel();

            return View(studentBiography);
        }
    }
}