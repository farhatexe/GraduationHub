using System.ComponentModel.DataAnnotations;

namespace GraduationHub.Web.Models.GraduateInformation
{
    public class AddressModel
    {
        [StringLength(75), Display(Name = "Address")]
        public string Street { get; set; }

        [StringLength(75), Display(Name = "City")]
        public string City { get; set; }

        [StringLength(2), Display(Name = "State")]
        public string State { get; set; }

        [StringLength(10), Display(Name = "Zip code")]
        public string Zipcode { get; set; } 
    }
}