using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> IsUsernameExists(string username);
        Task<bool> IsEmailExists(string email);
        Task CreateUserAsync(User user);
        Task<User?> IsUserExists(bool isUserName, LoginDto loginDto);
        Task<AdminUserLoginDto?> AdminLogin(AdminUserLoginDto loginDto);
    }
}
