using DAL.Entities;
using DAL.Models.SuperAdmin;
using Renty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IRepository
{
    public interface ISuperAdminRepo
    {
        public Task<Response<ApplicationUser>> CreateSuperAdminUserAsync(SuperAdminUserVM model);
        public Task<Response<ApplicationUser>> EditSuperAdminUserAsync(SuperAdminUserVM model);
        public Task<bool> RemoveSuperAdminUserAsync(string SuperAdminUserId);
        public Task<Response<ApplicationUser>> GetSuperAdminUserByIdAsync(string SuperAdminUserId);
        public Task<Response<ApplicationUser>> GetAllSuperAdminAsync(int paggingNumber);

    }
}
