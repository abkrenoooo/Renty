using DAL.Entities;
using DAL.ExtensionMethods;
using DAL.Mapping;
using DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Renty.DAL.Data;
using Renty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Repository
{
    public class HouseRepo : IHouseRepo
    {
        #region Depend Injection

        private readonly ApplicationDbContext _db;
        private readonly IAddressRepo _addressRepo;

        public HouseRepo(ApplicationDbContext db, IAddressRepo addressRepo)
        {
            _db = db;
            _addressRepo = addressRepo;
        }
        #endregion

        #region Create
        public async Task<Response<House>> Create_HouseAsync(House house)
        {
            try
            {
                if (house.Address == null)
                {
                    return new Response<House>
                    {
                        Success = true,
                        Message = "can't create address of house",
                        status_code = "500",
                        ObjectData = null
                    };
                }
                var addreesResult = await _addressRepo.Create_AddressAsync(house.Address);
                if (addreesResult.ObjectData == null)
                {
                    return new Response<House>
                    {
                        Success = true,
                        Message = "can't create address of house",
                        status_code = "500",
                        ObjectData = null
                    };
                }
                _db.Entry(house).Property(p => p.AddressId).IsModified = true;
                house.AddressId = addreesResult.ObjectData.AddressId;
                await _db.Houses.AddAsync(house);
                var result = await _db.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response<House>
                    {
                        Success = true,
                        Message = "House Is Created",
                        status_code = "200",
                        ObjectData = house
                    };
                }
                return new Response<House>
                {
                    Success = true,
                    Message = "Can't Create the House",
                    status_code = "500",
                };
            }
            catch (Exception e)
            {
                return new Response<House>
                {
                    Success = false,
                    Message = e.Message,
                    status_code = "500"
                };
            }
        }
        #endregion

        #region Delete
        public async Task<Response<House>> Delete_HouseAsync(int HouseId)
        {
            try
            {
                var House = await _db.Houses.Where(n => n.HouseId == HouseId).SingleOrDefaultAsync();

                if (House == null)
                {
                    return new Response<House>
                    {
                        Success = true,
                        Message = "House Not Found",
                        status_code = "404"
                    };
                }
                _db.Houses.Remove(House);
                var result = await _db.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response<House>
                    {
                        Success = true,
                        Message = "House Is Deleted",
                        status_code = "200",
                    };
                }
                return new Response<House>
                {
                    Success = true,
                    Message = "Can't delete the House",
                    status_code = "500",
                };
            }
            catch (Exception e)
            {
                return new Response<House>
                {
                    Success = true,
                    error = e.Message,
                    status_code = "500"
                };
            }
        }
        #endregion

        #region Update
        public async Task<Response<House>> EditHouseAsync(House house)
        {
            try
            {
                var orders = await _db.Orders.Where(x => x.HouseId == house.HouseId).CountAsync();
                if (orders ==0)
                {
                    return new Response<House>
                    {
                        Success = true,
                        Message = "Can't Edit this House because this House is order by some Customer",
                        status_code = "404",
                    };
                }
                var oldHouse = await _db.Houses.Include(x => x.Address).FirstOrDefaultAsync(x => x.HouseId == house.HouseId);
                if (oldHouse != null)
                {
                    if (oldHouse.IsCompleted == true)
                    {
                        return new Response<House>
                        {
                            Success = true,
                            Message = "Can't Edit this House because this House is Completed",
                            status_code = "404",
                        };
                    }
                    var editHouse = await house.ToHouseToEdit(oldHouse);
                    _db.Houses.Update(editHouse);
                    var result = await _db.SaveChangesAsync();
                    if (result > 0)
                    {
                        return new Response<House>
                        {
                            Success = true,
                            Message = "House Is updated",
                            status_code = "200",
                            ObjectData = editHouse
                        };
                    }
                }
                return new Response<House>
                {
                    Success = true,
                    Message = "Can't find this House",
                    status_code = "404",
                };
            }
            catch (Exception e)
            {
                return new Response<House>
                {
                    Success = false,
                    error = e.Message,
                    status_code = "500"
                };
            }
        }
        #endregion

        #region Get All House
        public async Task<Response<House>> GetAll_HouseAsync(int paggingNumber)
        {
            try
            {
                int AllPatientcount = await _db.Houses.CountAsync();
                var AllPatient = await _db.Houses.Include(x => x.Address).Skip((paggingNumber - 1) * 10).Take(10).ToListAsync(); ;
                return new Response<House>
                {
                    Success = true,
                    Message = "All House",
                    Data = AllPatient,
                    CountOfData = AllPatientcount,
                    paggingNumber = paggingNumber,
                    status_code = "200"
                };

            }
            catch (Exception e)
            {
                return new Response<House>
                {
                    Success = false,
                    error = e.Message,
                    status_code = "500"
                };
            }
        }
        #endregion

        #region Get All House For User
        public async Task<Response<House>> GetAll_HouseForUserAsync(int paggingNumber, string userId)
        {
            try
            {
                int AllPatientcount = await _db.Houses.Where(x => x.OwnerId == userId).CountAsync();
                var AllPatient = await _db.Houses.Where(x => x.OwnerId == userId).Include(x => x.Address).Skip((paggingNumber - 1) * 10).Take(10).ToListAsync(); ;
                return new Response<House>
                {
                    Success = true,
                    Message = "All House",
                    Data = AllPatient,
                    CountOfData = AllPatientcount,
                    paggingNumber = paggingNumber,
                    status_code = "200"
                };

            }
            catch (Exception e)
            {
                return new Response<House>
                {
                    Success = false,
                    error = e.Message,
                    status_code = "500"
                };
            }
        }

        #endregion

        #region Get House
        public async Task<Response<House>> Get_HouseAsync(int HouseId)
        {
            try
            {
                var House = await _db.Houses.Where(n => n.HouseId == HouseId).Include(x => x.Address).FirstOrDefaultAsync();
                if (House == null)
                {
                    return new Response<House>
                    {
                        Success = false,
                        status_code = "200",
                        Message = "House Not found",

                    };
                }
                return new Response<House>
                {
                    Success = true,
                    Message = "the patient",
                    status_code = "200",
                    ObjectData = House
                };

            }
            catch (Exception e)
            {
                return new Response<House>
                {
                    Success = false,
                    error = e.Message,
                    status_code = "500"
                };

            }
        }
        #endregion

    }
}
