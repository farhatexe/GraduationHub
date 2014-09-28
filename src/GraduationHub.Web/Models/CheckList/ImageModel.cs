using System;
using System.IO;
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

            ImageModel model = new ImageModel();

            string extension = string.Empty;
            var s = Path.GetExtension(studentPicture.ImageName);
            if (s != null)
            {
                extension = s.Replace(".", "");
            }

            model.ImageString = string.Format("data:image/{0};base64,{1}", extension, Convert.ToBase64String(studentPicture.ImageData));
            return new ImageModel();
        }

        public string ImageString { get; set; }

        public bool HasImage

        {
            get { return !string.IsNullOrWhiteSpace(ImageString); }
        }
    }
}