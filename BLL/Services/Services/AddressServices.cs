using BLL.Services.IServices;
using DAL.Repository.IRepository;
using DAL.Entities;
using Renty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models.AddressViewModel;

namespace BLL.Services.Services
{
    public class AddressServices : IAddressServices
    {
        #region Depend Injection

        private readonly IAddressRepo _AddressRepo;

        public AddressServices(IAddressRepo AddressRepo)
        {
            _AddressRepo = AddressRepo;
        }
        #endregion

        #region Create
        public async Task<Response<Address>> Create_AddressAsync(CreateAddressVM Address)
        {
            Address Address1 = new Address()
            {
                BuildingNumber = Address.BuildingNumber,
                AdditionalInformation = Address.AdditionalInformation,
                City = Address.City,
                Street  = Address.Street,
                Country = Address.Country,
                Flat = Address.Flat,
                Floor = Address.Floor,
                PostalCode = Address.PostalCode,
            };
            //var Address01 = await _AddressRepo.Create_AddressAsync(Address1);

            return null;
        }
        #endregion

        #region Delete
        public async Task<Response<Address>> Delete_AddressAsync(int AddressId)
        {
            var Address01 = await _AddressRepo.Delete_AddressAsync(AddressId);
            return Address01;
        }
        #endregion

        #region Update
        public async Task<Response<Address>> EditAddressAsync(AddressVM Address)
        {
            Address Address1 = new Address()
            {
                AddressId = Address.AddressId,
                BuildingNumber = Address.BuildingNumber,
                AdditionalInformation = Address.AdditionalInformation,
                City = Address.City,
                Street = Address.Street,
                Country = Address.Country,
                Flat = Address.Flat,
                Floor = Address.Floor,
                PostalCode = Address.PostalCode,
            };
            var Address01 = await _AddressRepo.EditAddressAsync(Address1);

            return Address01;
        }
        #endregion

        #region Get All
        public async Task<Response<Address>> GetAll_AddressAsync(int paggingNumber)
        {
            var Address01 = await _AddressRepo.GetAll_AddressAsync(paggingNumber);

            return Address01;
        }
        #endregion

        #region Get 
        public async Task<Response<Address>> Get_AddressAsync(int Id)
        {
            var Address01 = await _AddressRepo.Get_AddressAsync(Id);

            return Address01;
        }
        #endregion

    }
}
