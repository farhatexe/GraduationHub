using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Hosting;
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
            var response = _mediator.Request(new GetExpression { MaxLength = 200, Type = StudentExpressionType.Biography });

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
            var response = _mediator.Request(new GetExpression { MaxLength = 100, Type = StudentExpressionType.ThankYou });

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
            var response = _mediator.Request(new GetExpression { MaxLength = 35, Type = StudentExpressionType.SlideshowCaption });

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

            return View();
        }

        [HttpPost]
        public ActionResult SeniorPortrait(HttpPostedFileBase file)
        {
            if (!ModelState.IsValid) return View();

            StudentPicture studentPicture = _dbContext.StudentPictures
                .Where(e => e.StudentId.Equals(_currentUser.User.Id))
                .SingleOrDefault(e => e.ImageType == StudentPictureType.SeniorPortrait) ?? new StudentPicture
                {ImageType = StudentPictureType.SeniorPortrait};

            if (file != null && file.ContentLength > 0)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    imageData = binaryReader.ReadBytes(file.ContentLength);
                }

                studentPicture.StudentId = _currentUser.User.Id;
                studentPicture.DateSubmitted = DateTime.Now;
                studentPicture.ImageName = file.FileName;
                studentPicture.ImageData = imageData;
                studentPicture.MimeType = MimeMapping.GetMimeMapping(file.FileName);
            }


            if (studentPicture.Id == default(int))
            {
                _dbContext.StudentPictures.Add(studentPicture);
            }
            else
            {
                ObjectStateManager objectStateManager =
                    ((IObjectContextAdapter) _dbContext).ObjectContext.ObjectStateManager;
                _dbContext.StudentPictures.Attach(studentPicture);
                objectStateManager.ChangeObjectState(studentPicture, EntityState.Modified);
            }
  
            _dbContext.SaveChanges();

            return RedirectToAction<CheckListController>(c => c.SeniorPortrait())
                .WithSuccess("Your Senior Portrait was Saved.");
        }

        public void GetSeniorPortrait()
        {
                        StudentPicture studentPicture = _dbContext.StudentPictures
                .Where(e => e.StudentId.Equals(_currentUser.User.Id))
                .SingleOrDefault(e => e.ImageType == StudentPictureType.SeniorPortrait);

            if (studentPicture == null)
            {
                new WebImage(HostingEnvironment.MapPath(@"~/Content/images/male_silhouette.png")).Resize(350, 400, true, true).Write();
                return;
            }
                

            var webImage = new WebImage(studentPicture.ImageData);

            int height = webImage.Height;
            int width = webImage.Width;

            webImage.Resize(Convert.ToInt32(height * .50), Convert.ToInt32(width * .50), true, true)
                .Crop(1, 1)
                .Write();
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
            InformationModel model = _dbContext.GraduateInformation
                .Where(i => i.StudentId.Equals(_currentUser.User.Id)).Project().To<InformationModel>()
                .SingleOrDefault() ?? new InformationModel();


            return View(model);
        }

        [HttpPost]
        public ActionResult Information(InformationModel model)
        {
            if (!ModelState.IsValid) return View(model);


            GraduateInformation information = _dbContext.GraduateInformation
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
                    ((IObjectContextAdapter) _dbContext).ObjectContext.ObjectStateManager;
                _dbContext.GraduateInformation.Attach(information);
                objectStateManager.ChangeObjectState(information, EntityState.Modified);
            }

            _dbContext.SaveChanges();

            return RedirectToAction<CheckListController>(c => c.Information())
                .WithSuccess("Your Graduation Information has been saved.");
        }
    }
}