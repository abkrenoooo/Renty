using DAL.Entities;
using DAL.Models.User;
using Renty.Models;
using Renty.Models.AuthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IUserSevices
    {
        public Task<Response<UserVM>> RemoveUserUserAsync(string UserUserId);
        public Task<Response<UserVM>> GetUserUserByIdAsync(string UserUserId);
        public Task<Response<UserVM>> GetUserUsersAsync(int paggingNumber);
        public Task<Response<UserVM>> EditUserUserAsync(UserVMEdit model);
    }
}
