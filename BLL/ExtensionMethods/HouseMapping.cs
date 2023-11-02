
using DAL.Entities;
using DAL.Models.HouseViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ExtensionMethods
{
    public static class HouseMapping
    {
        public static async Task<House> ToHouse(this HouseVM houseVM,string OwnerId)
        {
            return new()
            {
                HouseType = houseVM.HouseType,
                Area = houseVM.Area,
                Date = houseVM.Date,
                Floor = houseVM.Floor,
                NumberOfBath = houseVM.NumberOfBath,
                NumberOfRoom = houseVM.NumberOfRoom,
                Photo1 = houseVM.Photo1,
                Photo2 = houseVM.Photo2,
                Photo3 = houseVM.Photo3,
                Photo4 = houseVM.Photo4,
                Name = houseVM.Name,
                OwnerId = OwnerId,
                IsCompleted = houseVM.IsCompleted,
                HouseTarget = houseVM.HouseTarget,
                Price = houseVM.Price,
                AddressId = houseVM.AddressId,
                HouseId = houseVM.HouseId,
                Address= houseVM.Address,

            };
        }
        public static async Task<HouseVM> ToHouseVM(this HouseVMEdit houseVM,string OwnerId)
        {
            return new()
            {
                HouseType = houseVM.HouseType,
                Area = houseVM.Area,
                Floor = houseVM.Floor,
                NumberOfBath = houseVM.NumberOfBath,
                NumberOfRoom = houseVM.NumberOfRoom,
                Name = houseVM.Name,
                OwnerId = OwnerId,
                HouseTarget = houseVM.HouseTarget,
                Price = houseVM.Price,
                HouseId = houseVM.HouseId,
                AddressId= houseVM.AddressId,
            };
        }
        public static async Task<HouseVM> ToHouseVM(this HouseVMCreate houseVM,string OwnerId)
        {
            return new()
            {
                HouseType = houseVM.HouseType,
                Area = houseVM.Area,
                Date = DateTime.UtcNow,
                Floor = houseVM.Floor,
                NumberOfBath = houseVM.NumberOfBath,
                NumberOfRoom = houseVM.NumberOfRoom,
                Name = houseVM.Name,
                OwnerId = OwnerId,
                HouseTarget = houseVM.HouseTarget,
                Price = houseVM.Price,
                IsCompleted=false
            };
        }

        public static async Task<HouseVM> FromHouse(this House house)
        {
            return new()
            {
                HouseType = house.HouseType,
                Area = house.Area,
                Date = house.Date,
                Floor = house.Floor,
                NumberOfBath = house.NumberOfBath,
                NumberOfRoom = house.NumberOfRoom,
                Photo1 = house.Photo1,
                Photo2 = house.Photo2,
                Photo3 = house.Photo3,
                Photo4 = house.Photo4,
                Name = house.Name,
                OwnerId = house.OwnerId,
                IsCompleted = house.IsCompleted,
                HouseTarget = house.HouseTarget,
                Price = house.Price,
                AddressId = house.AddressId,
                HouseId= house.HouseId,
                Address=house.Address
            };
        }
    }
}
