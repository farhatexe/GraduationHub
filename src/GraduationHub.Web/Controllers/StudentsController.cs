using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Helpers;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using DataTables.Mvc;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Filters;
using GraduationHub.Web.Infrastructure;
using GraduationHub.Web.Infrastructure.Alerts;
using GraduationHub.Web.Models;
using GraduationHub.Web.Models.CheckList;
using GraduationHub.Web.Models.Students;
using GraduationHub.Web.Notifications;
using GraduationHub.Web.Requests;
using ShortBus;

namespace GraduationHub.Web.Controllers
{
    [GraduationHubAuthorize(Roles = "Teacher, Admin")]
    public class StudentsController : AppBaseController
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMediator _mediator;

        public StudentsController(ApplicationDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexTable([ModelBinder(typeof (DataTablesBinder))] IDataTablesRequest requestModel)
        {
            IQueryable<StudentIndexModel> data =
                _dbContext.Users
                    .Include(i => i.StudentExpressions)
                    .Include(i => i.StudentPictures)
                    .Where(x => x.IsStudent)
                    .Project()
                    .To<StudentIndexModel>().OrderBy(requestModel.Sort());

            if (requestModel.HasSearchValues())
            {
                data = data.Where(requestModel.SearchValues(), requestModel.Search.Value);
            }

            int totalRecords = data.Count();

            var response = new DataTablesResponse(requestModel.Draw, data, totalRecords, totalRecords);

            return JsonSuccess(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Biography(StudentModel model)
        {
            model.StudentExpressionType = StudentExpressionType.Biography;
            model.TextMaxLength = 200;
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Biography(StudentExpressionModel model)
        {
            if (!ModelState.IsValid)
                return
                    View(new StudentModel
                    {
                        StudentExpressionType = StudentExpressionType.Biography,
                        Id = model.StudentId,
                        TextMaxLength = 200
                    });

            try
            {
                _mediator.Notify(new SaveExpression
                {
                    StudentId = model.StudentId,
                    Text = model.Text,
                    Type = StudentExpressionType.Biography
                });

                return RedirectToAction("Biography", new {Id = model.StudentId})
                    .WithSuccess("Your \"Biography\" has been saved.");
            }
            catch (Exception)
            {
                return RedirectToAction("Biography", new {Id = model.StudentId})
                    .WithError("There was a problem. Your \"Biography\" was not saved.");
            }
        }

        public ActionResult ExpressionOfThanks(StudentModel model)
        {
            model.StudentExpressionType = StudentExpressionType.ThankYou;
            model.TextMaxLength = 100;
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ExpressionOfThanks(StudentExpressionModel model)
        {
            if (!ModelState.IsValid)
                return
                    View(new StudentModel
                    {
                        StudentExpressionType = StudentExpressionType.ThankYou,
                        Id = model.StudentId,
                        TextMaxLength = 100
                    });


            try
            {
                _mediator.Notify(new SaveExpression
                {
                    StudentId = model.StudentId,
                    Text = model.Text,
                    Type = StudentExpressionType.ThankYou
                });

                return RedirectToAction("ExpressionOfThanks", new {Id = model.StudentId})
                    .WithSuccess("Your \"Expression of Thanks\" has been saved.");
            }
            catch (Exception)
            {
                return RedirectToAction("ExpressionOfThanks", new {Id = model.StudentId})
                    .WithError("There was a problem. Your \"Expression of Thanks\" was not saved.");
            }
        }


        public ActionResult SlideShowCaption(StudentModel model)
        {
            model.StudentExpressionType = StudentExpressionType.SlideshowCaption;
            model.TextMaxLength = 35;
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SlideShowCaption(StudentExpressionModel model)
        {
            if (!ModelState.IsValid)
                return View(new StudentModel
                {
                    StudentExpressionType = StudentExpressionType.SlideshowCaption,
                    Id = model.StudentId,
                    TextMaxLength = 35
                });

            try
            {
                _mediator.Notify(new SaveExpression
                {
                    StudentId = model.StudentId,
                    Text = model.Text,
                    Type = StudentExpressionType.SlideshowCaption
                });

                return RedirectToAction("SlideShowCaption", new {Id = model.StudentId})
                    .WithSuccess("Your \"Slide Show Caption\" has been saved.");
            }
            catch (Exception)
            {
                return RedirectToAction("SlideShowCaption", new {Id = model.StudentId})
                    .WithError("There was a problem. Your \"Slide Show Caption\" was not Saved.");
            }
        }

        public ActionResult SeniorPortrait(StudentModel model)
        {
            model.StudentPictureType = StudentPictureType.SeniorPortrait;
            return View(model);
        }

        public ActionResult BabyPicture(StudentModel model)
        {
            model.StudentPictureType = StudentPictureType.BabyPicture;
            return View(model);
        }

        public ActionResult ToddlerPicture(StudentModel model)
        {
            model.StudentPictureType = StudentPictureType.ToddlerPicture;
            return View(model);
        }

        public ActionResult ElementaryPicture(StudentModel model)
        {
            model.StudentPictureType = StudentPictureType.ElementaryPicture;
            return View(model);
        }

        public ActionResult MiddleSchoolPicture(StudentModel model)
        {
            model.StudentPictureType = StudentPictureType.MiddleSchoolPicture;
            return View(model);
        }

        public ActionResult HighSchoolPicture(StudentModel model)
        {
            model.StudentPictureType = StudentPictureType.HighSchoolPicture;
            return View(model);
        }

        public void GetPicture(StudentModel model)
        {
            Response<WebImage> response =
                _mediator.Request(new GetPicture {Type = model.StudentPictureType, UserId = model.Id});

            response.Data.Write();
        }

        [ChildActionOnly]
        public ActionResult GetText(StudentModel model)
        {
            Response<StudentExpressionModel> response =
                _mediator.Request(new GetExpression
                {
                    MaxLength = model.TextMaxLength,
                    Type = model.StudentExpressionType,
                    StudentId = model.Id
                });


            return PartialView("_StudentTextEditor", response.Data);
        }
    }
}