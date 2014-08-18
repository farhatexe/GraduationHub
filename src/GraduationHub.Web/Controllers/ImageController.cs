using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure;
using GraduationHub.Web.Infrastructure.Alerts;
using GraduationHub.Web.Models.Images;

namespace GraduationHub.Web.Controllers
{
    public class ImagesController : AppBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public ImagesController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUploadedFiles()
        {
            List<UploadedFilesModel> model =
                _context.StudentPictures
                    .Project().To<UploadedFilesModel>().ToList();


            return View(model);
        }

        public ActionResult GetFile(int id)
        {
            var image = _context.StudentPictures
                .Select(s => new {s.Id, s.ImageData})
                .Single(s => s.Id == id);

            return new FileStreamResult(new MemoryStream(image.ImageData), "image/jpg");
        }

        [HttpPost]
        public ActionResult SaveUploadedFile(HttpPostedFileBase file)
        {
            bool isSavedSuccessfully = false;

            if (file.ContentLength > 0)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    imageData = binaryReader.ReadBytes(file.ContentLength);
                }

                var slideShowImage = new StudentPicture();

                slideShowImage.ImageData = imageData;
                slideShowImage.ImageName = file.FileName;
                slideShowImage.ApplicationUser = _currentUser.User;

                _context.StudentPictures.Add(slideShowImage);
                _context.SaveChanges();
            }

            return JsonSuccess("").WithSuccess("Files Uploaded");
        }

        [HttpPost]
        public ActionResult SaveSeniorPicture(HttpPostedFileBase file)
        {
            bool isSavedSuccessfully = false;

            return SavePicture(file, StudentPictureType.SeniorPortrait);
        }

        private ActionResult SavePicture(HttpPostedFileBase file, StudentPictureType studentPictureType)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    byte[] imageData = null;

                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(file.ContentLength);
                    }

                    var slideShowImage = new StudentPicture();

                    slideShowImage.ImageData = imageData;
                    slideShowImage.ImageName = file.FileName;
                    slideShowImage.ApplicationUser = _currentUser.User;

                    _context.StudentPictures.Add(slideShowImage);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return JsonError(e.Message);
            }

            return JsonSuccess(string.Empty).WithSuccess("Senior Picture Loaded");
        }
    }
}