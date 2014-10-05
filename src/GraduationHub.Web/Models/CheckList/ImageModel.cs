using System;
using System.IO;
using System.Web.Helpers;
using GraduationHub.Web.Domain;

namespace GraduationHub.Web.Models.CheckList
{
    public class ImageModel
    {

        public static ImageModel Create(StudentPicture studentPicture)
        {
            if (studentPicture == null)
            {
                return new ImageModel();
            }



            var model = new ImageModel
            {
                ImageString =
                    string.Format("data:{0};base64,{1}", studentPicture.MimeType,
                        Convert.ToBase64String(studentPicture.ImageData))
            };

            return model;
        }

        public string ImageString { get; set; }

        public bool HasImage

        {
            get { return !string.IsNullOrWhiteSpace(ImageString); }
        }
    }
}