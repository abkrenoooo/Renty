using DAL.Entities;
using DAL.Models.AddressViewModel;

namespace BLL.Mapping
{
    public static class AdreesMapping
    {
        public static async Task<AddressVM> ToAddressVM(this CreateAddressVM Address)
        {
            return new()
            {
                AdditionalInformation = Address.AdditionalInformation,
                BuildingNumber = Address.BuildingNumber,
                City = Address.City,
                Country = Address.Country,
                Flat = Address.Flat,
                Floor = Address.Floor,
                PostalCode= Address.PostalCode,
                Street = Address.Street,     
            };
        }
        public static async Task<CreateAddressVM> FromAddressVM(this AddressVM Address)
        {
            return new()
            {
                AdditionalInformation = Address.AdditionalInformation,
                BuildingNumber = Address.BuildingNumber,
                City = Address.City,
                Country = Address.Country,
                Flat = Address.Flat,
                Floor = Address.Floor,
                PostalCode= Address.PostalCode,
                Street = Address.Street,    
            };
        } 
        public static async Task<AddressVM> FromAddress(this Address Address)
        {
            return new()
            {
                AdditionalInformation = Address.AdditionalInformation,
                BuildingNumber = Address.BuildingNumber,
                City = Address.City,
                Country = Address.Country,
                Flat = Address.Flat,
                Floor = Address.Floor,
                PostalCode= Address.PostalCode,
                Street = Address.Street,
                AddressId= Address.AddressId,
            };
        }
        public static async Task<Address> ToAddress(this AddressVM Address)
        {
            return new()
            {
                AdditionalInformation = Address.AdditionalInformation,
                BuildingNumber = Address.BuildingNumber,
                City = Address.City,
                Country = Address.Country,
                Flat = Address.Flat,
                Floor = Address.Floor,
                PostalCode= Address.PostalCode,
                Street = Address.Street,
                AddressId= Address.AddressId,
            };
        }
    }
}
