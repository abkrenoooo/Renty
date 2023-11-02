using DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Renty.DAL.Data;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renty.Models;
using DAL.Models.AddressViewModel;
using DAL.Mapping;

namespace DAL.Repository.Repository
{
    public class AddressRepo : IAddressRepo
    {
        #region Depend Injection

        private readonly ApplicationDbContext _db;

        public AddressRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Create
        public async Task<Response<Address>> Create_AddressAsync(Address Address)
        {
            try
            {
                await _db.Address.AddAsync(Address);
                var result = await _db.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response<Address>
                    {
                        Success = true,
                        Message = "Address Is Created",
                        status_code = "200",
                        ObjectData=Address,
                    };
                }
                return new Response<Address>
                {
                    Success = true,
                    Message = "Can't Create the Address",
                    status_code = "500",
                };
            }
            catch (Exception e)
            {
                return new Response<Address>
                {
                    Success = false,
                    error = e.Message,
                    status_code = "500"
                };
            }
        }
        #endregion

        #region Delete
        public async Task<Response<Address>> Delete_AddressAsync(int AddressId)
        {
            try
            {
                var Address = await _db.Address.Where(n => n.AddressId == AddressId).SingleOrDefaultAsync();

                if (Address == null)
                {
                    return new Response<Address>
                    {
                        Success = true,
                        Message = "Address Not Found",
                        status_code = "404"
                    };
                }
                 _db.Address.Remove(Address);
                var result =await _db.SaveChangesAsync();
                if (result>0)
                {
                    return new Response<Address>
                    {
                        Success = true,
                        Message = "Address Is Deleted",
                        status_code = "200",
                    };
                }
                return new Response<Address>
                {
                    Success = true,
                    Message = "Can't delete the Address",
                    status_code = "500",
                };
            }
            catch (Exception e)
            {
                return new Response<Address>
                {
                    Success = true,
                    error = e.Message,
                    status_code = "500"
                };
            }
        }
        #endregion

        #region Update
        public async Task<Response<Address>> EditAddressAsync(Address Address)
        {
            try
            {
                var oldAddress = await _db.Address.FirstOrDefaultAsync(x => x.AddressId == Address.AddressId);
                if (oldAddress is null)
                {
                    return new Response<Address>
                    {
                        Success = true,
                        Message = "This Address is Not Found",
                        status_code = "500",
                    };
                }
                var adress = Address.ToAddress(oldAddress);

                oldAddress.AdditionalInformation = Address.AdditionalInformation;
                oldAddress.Flat = Address.Flat;
                oldAddress.Floor = Address.Floor;
                _db.Address.Update(oldAddress);
                var result = await _db.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response<Address>
                    {
                        Success = true,
                        Message = "Address Is Edit",
                        status_code = "200",
                    };
                }
                return new Response<Address>
                {
                    Success = true,
                    Message = "Can't Edit the Address",
                    status_code = "500",
                };
            }
            catch (Exception e)
            {
                return new Response<Address>
                {
                    Success = false,
                    error = e.Message,
                    status_code = "500"
                };
            }
        }
        #endregion

        #region Get All
        public async Task<Response<Address>> GetAll_AddressAsync(int paggingNumber)
        {
            try
            {
                int AllPatientcount = await _db.Address.CountAsync();
                var AllPatient = await _db.Address.Skip((paggingNumber - 1) * 10).Take(10).ToListAsync(); ;
                return new Response<Address>
                {
                    Success = true,
                    Message = "All Address",
                    Data = AllPatient,
                    CountOfData = AllPatientcount,
                    paggingNumber = paggingNumber,
                    status_code = "200"
                };

            }
            catch (Exception e)
            {
                return new Response<Address>
                {
                    Success = false,
                    error = e.Message,
                    status_code = "500"
                };
            }
        }
        #endregion

        #region Get 
        public async Task<Response<Address>> Get_AddressAsync(int AddressId)
        {
            try
            {
                var Address = await _db.Address.Where(n => n.AddressId == AddressId).FirstOrDefaultAsync();
                if (Address == null)
                {
                    return new Response<Address>
                    {
                        Success = false,
                        status_code = "200",
                        Message = "Address Not found",

                    };
                }
                return new Response<Address>
                {
                    Success = true,
                    Message = "the patient",
                    status_code = "200",
                    ObjectData = Address
                };

            }
            catch (Exception e)
            {
                return new Response<Address>
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
