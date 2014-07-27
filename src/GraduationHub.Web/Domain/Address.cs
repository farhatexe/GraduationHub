using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationHub.Web.Data;

namespace GraduationHub.Web.Domain
{
    [ComplexType]
    public class Address
    {

        [StringLength(FieldLengths.Address.Street)]
        public string Street { get; set; }

        [StringLength(FieldLengths.Address.City)]
        public string City { get; set; }

        [StringLength(FieldLengths.Address.State)]
        public string State { get; set; }

        [StringLength(FieldLengths.Address.Zipcode)]
        public string Zipcode { get; set; }
    }
}