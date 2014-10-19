using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Filters;
using GraduationHub.Web.Infrastructure;
using GraduationHub.Web.Infrastructure.Alerts;
using GraduationHub.Web.Models.CheckList;
using GraduationHub.Web.Notifications;
using GraduationHub.Web.Requests;
using ShortBus;

namespace GraduationHub.Web.Controllers
{
    [GraduationHubAuthorize(Roles = SecurityConstants.Roles.Student)]
    public class CheckListController : AppBaseController
    {

        private readonly ICurrentUser _currentUser;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMediator _mediator;

        public CheckListController(ApplicationDbContext dbContext, ICurrentUser currentUser, IMediator mediator)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
            _mediator = mediator;
        }

        public ActionResult Biography()
        {
            Response<StudentExpressionModel> response =
                _mediator.Request(new GetExpression {MaxLength = 200, Type = StudentExpressionType.Biography});

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

        public ActionResult ExpressionOfThanks()
        {
            Response<StudentExpressionModel> response =
                _mediator.Request(new GetExpression {MaxLength = 100, Type = StudentExpressionType.ThankYou});

            return View(response.Data);
        }

        // POST: FrequentlyAskedQuestions/Create
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ExpressionOfThanks(StudentExpressionModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                _mediator.Notify(new SaveExpression
                {
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

        public ActionResult SlideShowCaption()
        {
            Response<StudentExpressionModel> response =
                _mediator.Request(new GetExpression {MaxLength = 35, Type = StudentExpressionType.SlideshowCaption});

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

        public ActionResult SeniorPortrait()
        {
            return View(StudentPictureType.SeniorPortrait);
        }

        [HttpPost]
        public ActionResult UploadPicture(HttpPostedFileBase file, StudentPictureType type)
        {
            try
            {
                _mediator.Notify(new SavePicture(file, type));

                return RedirectToAction(type.ToString()).WithSuccess("Your Picture has been saved.");
            }
            catch (Exception)
            {
                return RedirectToAction(type.ToString()).WithError("There was a problem uploading your file. Your picture has not been saved.");
            }
        }

        public void GetPicture(StudentPictureType type)
        {
            Response<WebImage> response =
                _mediator.Request(new GetPicture {Type = type});

            response.Data.Write();
        }

        public ActionResult BabyPicture()
        {
            return View(StudentPictureType.BabyPicture);
        }

        public ActionResult ToddlerPicture()
        {
            return View(StudentPictureType.ToddlerPicture);
        }

        public ActionResult ElementaryPicture()
        {
            return View(StudentPictureType.ElementaryPicture);
        }

        public ActionResult MiddleSchoolPicture()
        {
            return View(StudentPictureType.MiddleSchoolPicture);
        }

        public ActionResult HighSchoolPicture()
        {
            return View(StudentPictureType.HighSchoolPicture);
        }

        public ActionResult ImportantDates()
        {
            List<CheckListImportantDate> dueDates =
                _dbContext.ImportantDates.Project().To<CheckListImportantDate>().OrderBy(x => x.DueDate).ToList();

            return View(dueDates);
        }

        public ActionResult Faqs()
        {
            List<Faq> faqs = _dbContext.FrequentlyAskedQuestions.OrderBy(x => x.Order).Project().To<Faq>().ToList();

            return View(faqs);
        }

        public ActionResult GraduateInformation()
        {
            InformationModel model = _dbContext.GraduateInformation
                .Where(i => i.StudentId.Equals(_currentUser.User.Id)).Project().To<InformationModel>()
                .SingleOrDefault() ?? new InformationModel();


            return View(model);
        }

        [HttpPost]
        public ActionResult GraduateInformation(InformationModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                _mediator.Notify(new SaveGradInfo
                {
                    Name = model.Name,
                    Street = model.Street,
                    City = model.City,
                    StudentEmail = model.StudentEmail,
                    ParentEmail = model.ParentEmail,
                    FineArts = model.FineArts,
                    AcademicClasses = model.AcademicClasses,
                    WillParticipateInGraduation = model.WillParticipateInGraduation.Value,
                    TakenKeysWorldView = model.TakenKeysWorldView,
                    TakenApprovedWorldView = model.TakenApprovedWorldView,
                    WillSecureAnnouncements = model.WillSecureAnnouncements,
                    NeedCapAndGown = model.NeedCapAndGown,
                    Height = model.Height
                });

                return RedirectToAction<CheckListController>(c => c.GraduateInformation())
                    .WithSuccess("Your Graduation Information has been saved.");
            }
            catch (Exception)
            {
                return RedirectToAction<CheckListController>(c => c.GraduateInformation())
                    .WithError("There was a problem. Your Graduation Information has been saved.");
            }
        }
    }
}