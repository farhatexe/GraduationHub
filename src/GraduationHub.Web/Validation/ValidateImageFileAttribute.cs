using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Web;

namespace GraduationHub.Web.Validation
{
    public class ValidateImageFileAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;

            if (file == null || file.ContentLength <=0)
            {
                return false;
            }

            try
            {
                Image.FromStream(file.InputStream);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}