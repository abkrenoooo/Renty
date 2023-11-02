using DAL.Entities;
using DAL.Models.Admin;
using Renty.Models;
using Renty.Models.AuthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IAdminSevices
    {
        public Task<Response<AdminUserVM>> CreateAdminUserAsync(AdminUserCreateVM model);
        public Task<Response<AdminUserVM>> RemoveAdminUserAsync(string AdminUserId);
        public Task<Response<AdminUserVM>> GetAdminUserByIdAsync(string AdminUserId);
        public Task<Response<AdminUserVM>> GetAdminUsersAsync(int paggingNumber);
        public Task<Response<AdminUserVM>> EditAdminUserAsync(AdminUserVMEdit model);
    }
}
