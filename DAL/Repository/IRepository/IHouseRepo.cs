using DAL.Entities;
using Renty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IRepository
{
    public interface IHouseRepo
    {
        Task<Response<House>> Create_HouseAsync(House house);
        Task<Response<House>> Delete_HouseAsync(int houseId);
        Task<Response<House>> Get_HouseAsync(int houseId);
        Task<Response<House>> GetAll_HouseAsync(int paggingNumber);
        Task<Response<House>> GetAll_HouseForUserAsync(int paggingNumber, string userId);
        Task<Response<House>> EditHouseAsync(House house);
    }
}
