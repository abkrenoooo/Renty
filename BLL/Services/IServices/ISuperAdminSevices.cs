using DAL.Entities;
using DAL.Models.SuperAdmin;
using Renty.Models;
using Renty.Models.AuthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface ISuperAdminSevices
    {
        public Task<Response<SuperAdminUserVM>> CreateSuperAdminUserAsync(SuperAdminUserCreateVM model);
        public Task<Response<SuperAdminUserVM>> RemoveSuperAdminUserAsync(string SuperAdminUserId);
        public Task<Response<SuperAdminUserVM>> GetSuperAdminUserByIdAsync(string SuperAdminUserId);
        public Task<Response<SuperAdminUserVM>> GetSuperAdminUsersAsync(int paggingNumber);
        public Task<Response<SuperAdminUserVM>> EditSuperAdminUserAsync(SuperAdminUserVMEdit model);
    }
}
