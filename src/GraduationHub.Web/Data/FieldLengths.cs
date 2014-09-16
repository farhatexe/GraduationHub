namespace GraduationHub.Web.Data
{
    public class FieldLengths
    {
        public class Invitation
        {
            public const int InviteeName = 75;
            public const int Email = 75;
        }

        public class LogAction
        {
            public const int Controller = 125;
            public const int Action = 125;
            public const int Description = 125;
        }

        public class ApplicationUser
        {
            public const int FirstName = 50;
            public const int LastName = 50;
        }

        public class Address
        {
            public const int Street = 75;
            public const int City = 75;
            public const int State = 2;
            public const int Zipcode = 10;
        }

        public class GraduateInformation
        {
            public const int Name = 75;
            public const int StudentEmail = 75;
            public const int ParentEmail = 75;
            public const int Height = 20;

        }

        public class ImportantDate
        {
            public const int Description = 500;
        }

        public class FrequentlyAskedQuestion
        {
            public const int Question = 150;
            public const int Answer = 1000;
        }

        public class StudentPictures
        {
            public const int ImageName = 75;
            public const int Description = 175;
        }

        public class StudentExpression
        {
            public const int Biography = 200;
            public const int Thanks = 100;
            public const int SlideShowCaption = 35;
        }

    }
}