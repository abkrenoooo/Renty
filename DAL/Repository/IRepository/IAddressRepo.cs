using DAL.Entities;
using DAL.Models.AddressViewModel;
using Renty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IRepository
{
    public interface IAddressRepo
    {
        Task<Response<Address>> Create_AddressAsync(Address address);
        Task<Response<Address>> Delete_AddressAsync(int addressId);
        Task<Response<Address>> Get_AddressAsync(int addressId);
        Task<Response<Address>> GetAll_AddressAsync(int paggingNumber);
        Task<Response<Address>> EditAddressAsync(Address address);

    }
}
