using DAL.Entities;
using DAL.Models.AddressViewModel;
using Renty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IAddressServices
    {
        Task<Response<Address>> Create_AddressAsync(CreateAddressVM AddressVM);
        Task<Response<Address>> Delete_AddressAsync(int Id);
        Task<Response<Address>> Get_AddressAsync(int Id);
        Task<Response<Address>> GetAll_AddressAsync(int paggingNumber);
        Task<Response<Address>> EditAddressAsync(AddressVM AddressVM);
    }
}
