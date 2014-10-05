using System.Web;
using GraduationHub.Web.Validation;

namespace GraduationHub.Web.Models.CheckList
{
    public class UploadedFile
    {
        [ValidateImageFile(ErrorMessage = "File must be an Image.")]
        public HttpPostedFileBase[] File { get; set; } 
    }
}