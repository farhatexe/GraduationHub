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

        /*
         * Display Name
         * Display Silohotted (Left)
         * Display Dates 
         * Display FAQ
         * Display Bio
         * Display Thanks
         * Display Caption
         * Display Baby Picture
         * Display Youthful 1
         * Display Youthful 2
         * Display Youthful 3
         * Display Youthful 4
         */

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
    }
}