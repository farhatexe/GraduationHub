using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Filters;
using GraduationHub.Web.Infrastructure;
using GraduationHub.Web.Infrastructure.Alerts;
using GraduationHub.Web.Models.CheckList;

namespace GraduationHub.Web.Controllers
{
    [GraduationHubAuthorize(Roles = SecurityConstants.Roles.Student)]
    public class CheckListController : AppBaseController
    {
        private readonly ICurrentUser _currentUser;
        private readonly ApplicationDbContext _dbContext;

        public CheckListController(ApplicationDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public ActionResult Biography()
        {
            return View();
        }

        public ActionResult ExpressionOfThanks()
        {
            StudentExpressionModel studentExpression = _dbContext.StudentExpressions
                .Where(e => e.StudentId.Equals(_currentUser.User.Id))
                .Where(e => e.Type == StudentExpressionType.ThankYou).Project().To<StudentExpressionModel>()
                .SingleOrDefault() ?? new StudentExpressionModel();

            studentExpression.TextMaxLength = 100;

            return View(studentExpression);
        }

        // POST: FrequentlyAskedQuestions/Create
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ExpressionOfThanks(StudentExpressionModel model)
        {
            if (!ModelState.IsValid) return View(model);

            StudentExpression studentExpression = _dbContext.StudentExpressions
                .Where(e => e.StudentId.Equals(_currentUser.User.Id))
                .SingleOrDefault(e => e.Type == StudentExpressionType.ThankYou) ??
                                                  new StudentExpression {Type = StudentExpressionType.ThankYou};

            studentExpression.StudentId = _currentUser.User.Id;
            studentExpression.Text = model.Text;

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

            return RedirectToAction<CheckListController>(c => c.ExpressionOfThanks())
                .WithSuccess("Your Expression of Thanks was Saved.");
        }

        public ActionResult SlideShowCaption()
        {
            return View();
        }

        public ActionResult SeniorPortrait()
        {
            StudentPicture studentPicture = _dbContext.StudentPictures
            .Where(e => e.StudentId.Equals(_currentUser.User.Id))
            .SingleOrDefault(e => e.ImageType == StudentPictureType.SeniorPortrait);


            var viewModel = ImageModel.Create(studentPicture);
     

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SeniorPortrait(UploadedFile model)
        {
            if (!ModelState.IsValid) return View();

            StudentPicture studentPicture = _dbContext.StudentPictures
                .Where(e => e.StudentId.Equals(_currentUser.User.Id))
                .SingleOrDefault(e => e.ImageType == StudentPictureType.SeniorPortrait) ?? new StudentPicture
                {ImageType = StudentPictureType.SeniorPortrait};


            byte[] imageData;

            using (var binaryReader = new BinaryReader(model.File.InputStream))
            {
                imageData = binaryReader.ReadBytes(model.File.ContentLength);
            }

            studentPicture.StudentId = _currentUser.User.Id;
            studentPicture.DateSubmitted = DateTime.Now;
            studentPicture.ImageName = model.File.FileName;
            studentPicture.ImageData = imageData;

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

            return RedirectToAction<CheckListController>(c => c.SeniorPortrait())
                .WithSuccess("Your Senior Portrait was Saved.");
        }


        public ActionResult BabyPicture()
        {
            return View(new ImageModel());
        }

        public ActionResult ToddlerPicture()
        {
            return View(new ImageModel());
        }

        public ActionResult ElementaryPicture()
        {
            return View(new ImageModel());
        }

        public ActionResult MiddleSchoolPicture()
        {
            return View(new ImageModel());
        }

        public ActionResult HighSchoolPicture()
        {
            return View(new ImageModel());
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

        public ActionResult Information()
        {

            var model = _dbContext.GraduateInformation
                .Where(i => i.StudentId.Equals(_currentUser.User.Id)).Project().To<InformationModel>()
                .SingleOrDefault() ?? new InformationModel();


            return View(model);
        }

        [HttpPost]
        public ActionResult Information(InformationModel model)
        {
            if (!ModelState.IsValid) return View(model);


            var information = _dbContext.GraduateInformation
                .SingleOrDefault(i => i.StudentId == _currentUser.User.Id)
                              ?? new GraduateInformation {StudentId = _currentUser.User.Id};

            information.Name = model.Name;
            information.Street = model.Street;
            information.City = model.City;
            information.StudentEmail = model.StudentEmail;
            information.ParentEmail = model.ParentEmail;
            information.FineArts = model.FineArts;
            information.AcademicClasses = model.AcademicClasses;
            information.WillParticipateInGraduation = model.WillParticipateInGraduation.Value;
            information.TakenKeysWorldView = model.TakenKeysWorldView;
            information.TakenApprovedWorldView = model.TakenApprovedWorldView;
            information.WillSecureAnnouncements = model.WillSecureAnnouncements;
            information.NeedCapAndGown = model.NeedCapAndGown;
            information.Height = model.Height;

            if (information.Id == default(int))
            {
                _dbContext.GraduateInformation.Add(information);
            }
            else
            {
                ObjectStateManager objectStateManager =
                    ((IObjectContextAdapter)_dbContext).ObjectContext.ObjectStateManager;
                _dbContext.GraduateInformation.Attach(information);
                objectStateManager.ChangeObjectState(information, EntityState.Modified);
            }

            _dbContext.SaveChanges();

            return RedirectToAction<CheckListController>(c => c.Information())
                .WithSuccess("Your Graduation Information has been saved.");


        }
    }
}