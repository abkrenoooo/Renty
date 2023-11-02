using DAL.Entities;
using DAL.Models.HouseViewModel;
using Renty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IHouseServices
    {
        Task<Response<HouseVM>> DeleteHouseAsync(int houseId);
        Task<Response<HouseVM>> GetHouseAsync(int houseId);
        Task<Response<HouseVM>> GetAllHouseAsync(int paggingNumber);
        Task<Response<HouseVM>> Create_HouseAsync(HouseVMCreate houseVM, string userId);
        Task<Response<HouseVM>> EditHouseAsync(HouseVMEdit houseVM, string userId);
        Task<Response<HouseVM>> GetAll_HouseForUserAsync(int paggingNumber, string userId);
    }
}
