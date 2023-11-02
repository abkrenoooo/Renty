using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.AddressViewModel
{
    public class CreateAddressVM
    {
        
        [Required]
        [Display(Name = "Country")]
        public string? Country { get; set; }
        [Display(Name = "City")]
        [Required]
        public string? City { get; set; }
        [Required]
        [Display(Name = "Street")]
        public string? Street { get; set; }
        [Display(Name = "Building Number")]
        [Required]
        public string? BuildingNumber { get; set; }
        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }
        [Display(Name = "Floor")]
        public string? Floor { get; set; }
        [Display(Name = "Flat")]
       public string? Flat { get; set; }
        [Display(Name = "Additional Information")]
        public string? AdditionalInformation { get; set; }
    }
}
