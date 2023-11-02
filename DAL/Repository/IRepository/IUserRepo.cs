using DAL.Entities;
using DAL.Models.User;
using Renty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IRepository
{
    public interface IUserRepo
    {
        public Task<Response<ApplicationUser>> EditUserUserAsync(UserVM model);
        public Task<bool> RemoveUserUserAsync(string UserUserId);
        public Task<Response<ApplicationUser>> GetUserUserByIdAsync(string UserUserId);
        public Task<Response<ApplicationUser>> GetAllUserAsync(int paggingNumber);

    }
}
