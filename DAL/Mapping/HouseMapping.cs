
using DAL.Entities;
using DAL.Models.HouseViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ExtensionMethods
{
    public static class HouseMapping
    {
        public static async Task<House> ToHouseToEdit(this House house,House editHouse)
        {
            editHouse.HouseType = house.HouseType;
            editHouse.Area = house.Area;
            editHouse.Date = house.Date;
            editHouse.Floor = house.Floor;
            editHouse.NumberOfBath = house.NumberOfBath;
            editHouse.NumberOfRoom = house.NumberOfRoom;
            editHouse.Photo1 = house.Photo1;
            editHouse.Photo2 = house.Photo2;
            editHouse.Photo3 = house.Photo3;
            editHouse.Photo4 = house.Photo4;
            editHouse.Name = house.Name;
            editHouse.OwnerId = house.OwnerId;
            editHouse.HouseTarget = house.HouseTarget;
            editHouse.Price = house.Price;
            editHouse.AddressId = house.AddressId;
            editHouse.HouseId = house.HouseId;
           return editHouse;
        }
    }
}
