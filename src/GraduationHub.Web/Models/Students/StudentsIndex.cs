using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure.ModelMetadata;

namespace GraduationHub.Web.Models.Students
{
    public class StudentsIndex
    {
        public string StudentName { get; set; }

        [StudentExpressionInput(FieldLengths.StudentExpression.Biography)]
        public string Biography { get; set; }

        [StudentExpressionInput(FieldLengths.StudentExpression.Thanks)]
        public string ExpressionOfThanks { get; set; }

        [StudentExpressionInput(FieldLengths.StudentExpression.SlideShowCaption)]
        public string SlideshowCaption { get; set; }

        [StudentPicture(StudentPictureType.SeniorPortrait)]
        public int? SeniorPortraitId { get; set; }

        [StudentPicture(StudentPictureType.BabyPicture)]
        public int? BabyPictureId { get; set; }

        [StudentPicture(StudentPictureType.YouthfulFavorite1)]
        public int? YouthfulFavoriteOneId { get; set; }

        [StudentPicture(StudentPictureType.YouthfulFavorite2)]
        public int? YouthfulFavoriteTwoId { get; set; }

        [StudentPicture(StudentPictureType.YouthfulFavorite3)]
        public int? YouthfulFavoriteThreeId { get; set; }

        [StudentPicture(StudentPictureType.YouthfulFavorite4)]
        public int? YouthfulFavoriteFourId { get; set; }

    }
}