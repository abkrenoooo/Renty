using DAL.Entities;
using DAL.Models.Admin;
using Renty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IRepository
{
    public interface IAdminRepo
    {
        public Task<Response<ApplicationUser>> CreateAdminUserAsync(AdminUserVM model);
        public Task<Response<ApplicationUser>> EditAdminUserAsync(AdminUserVM model);
        public Task<bool> RemoveAdminUserAsync(string AdminUserId);
        public Task<Response<ApplicationUser>> GetAdminUserByIdAsync(string AdminUserId);
        public Task<Response<ApplicationUser>> GetAllAdminAsync(int paggingNumber);

    }
}
