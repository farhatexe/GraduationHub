using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GraduationHub.Web.Controllers
{
    //TODO: Add the Authorize Student Attribute...they must be in the student role.
    public class StudentController : Controller
    {


        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult MyBiography()
        {
            return View();
        }
    }
}