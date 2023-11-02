using DAL.Entities;
using DAL.Models.AddressViewModel;

namespace DAL.Mapping
{
    public static class AdreesMapping
    {

        public static async Task<Address> ToAddress(this Address Address, Address AddressEdit)
        {

            AddressEdit.AdditionalInformation = Address.AdditionalInformation;
            AddressEdit.BuildingNumber = Address.BuildingNumber;
            AddressEdit.City = Address.City;
            AddressEdit.Country = Address.Country;
            AddressEdit.Flat = Address.Flat;
            AddressEdit.Floor = Address.Floor;
            AddressEdit.PostalCode = Address.PostalCode;
            AddressEdit.Street = Address.Street;
            AddressEdit.AddressId = Address.AddressId;
            return AddressEdit;
        }
    }
}
