using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        //Country represented by ISO-3166-2 2 symbol code of the countries.Must be EG for internal business issuers.
        public string? Country { get; set; }

        //Governorate information as textual value
        public string? City { get; set; }
        //street information
        public string? Street { get; set; }

        // building information
        public string? BuildingNumber { get; set; }

        //Optional: Postal code
        public string? PostalCode { get; set; }

        //Optional: the floor number
        public string? Floor { get; set; }

        // Optional: the room/flat number in the floor
        public string? Flat { get; set; }

        // Optional: any additional information to the address 
        public string? AdditionalInformation { get; set; }

    }
}
