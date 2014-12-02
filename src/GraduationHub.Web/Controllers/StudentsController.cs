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
            Response<StudentExpressionModel> response =
                _mediator.Request(new GetExpression { MaxLength = 200, Type = StudentExpressionType.Biography, StudentId = model.Id});

            return View(response.Data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Biography(StudentExpressionModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                _mediator.Notify(new SaveExpression
                {
                    StudentId = model.StudentId,
                    Text = model.Text,
                    Type = StudentExpressionType.Biography
                });

                return RedirectToAction<CheckListController>(c => c.Biography())
                    .WithSuccess("Your \"Biography\" has been saved.");
            }
            catch (Exception)
            {
                return RedirectToAction<CheckListController>(c => c.Biography())
                    .WithError("There was a problem. Your \"Biography\" was not saved.");
            }
        }

        public ActionResult ExpressionOfThanks(StudentModel model)
        {
            Response<StudentExpressionModel> response =
                _mediator.Request(new GetExpression { MaxLength = 200, Type = StudentExpressionType.ThankYou, StudentId = model.Id });

            return View(response.Data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ExpressionOfThanks(StudentExpressionModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                _mediator.Notify(new SaveExpression
                {
                    StudentId = model.StudentId,
                    Text = model.Text,
                    Type = StudentExpressionType.ThankYou
                });

                return RedirectToAction<CheckListController>(c => c.ExpressionOfThanks())
                    .WithSuccess("Your \"Expression of Thanks\" has been saved.");
            }
            catch (Exception)
            {
                return RedirectToAction<CheckListController>(c => c.ExpressionOfThanks())
                    .WithError("There was a problem. Your \"Expression of Thanks\" was not saved.");
            }
        }


        public ActionResult SlideShowCaption(StudentModel model)
        {
            Response<StudentExpressionModel> response =
                _mediator.Request(new GetExpression { MaxLength = 200, Type = StudentExpressionType.SlideshowCaption, StudentId = model.Id });

            return View(response.Data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SlideShowCaption(StudentExpressionModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                _mediator.Notify(new SaveExpression
                {
                    StudentId = model.StudentId,
                    Text = model.Text,
                    Type = StudentExpressionType.SlideshowCaption
                });

                return RedirectToAction<CheckListController>(c => c.SlideShowCaption())
                    .WithSuccess("Your \"Slide Show Caption\" has been saved.");
            }
            catch (Exception)
            {
                return RedirectToAction<CheckListController>(c => c.SlideShowCaption())
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
                _mediator.Request(new GetPicture { Type = model.StudentPictureType, UserId = model.Id});

            response.Data.Write();
        }
    }
}