using Data.DTOs;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public AccountRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public async Task CreateUserAsync(User user)
        {
            await _applicationDBContext.User.AddAsync(user);
        }

        public async Task<bool> IsEmailExists(string email)
        {
            return await _applicationDBContext.User.AnyAsync(user => user.UserEmail != "" && user.UserEmail!.Equals(email));
        }

        public async Task<User?> IsUserExists(bool isUserName, LoginDto loginDto)
        {
            if (isUserName)
            {
                var userEntity = await _applicationDBContext.User.FirstAsync(user => user.UserName.Equals(loginDto.UserName));
                if(!userEntity.Password.Equals(loginDto.Password))
                {
                    return null;
                }
                return userEntity;
            }
            else
            {
                var userEntity = await _applicationDBContext.User.FirstAsync(user => user.UserEmail!.Equals(loginDto.Email));
                if (!userEntity.Password.Equals(loginDto.Password))
                {
                    return null;
                }
                return userEntity;
            }
        }

        public async Task<bool> IsUsernameExists(string username)
        {
            return await _applicationDBContext.User.AnyAsync(user => user.UserName.Equals(username));
        }

        public async Task<AdminUserLoginDto?> AdminLogin(LoginDto loginDto)
        {
            var data = await (from adminUser in _applicationDBContext.AdminUser
                            join userType in _applicationDBContext.UserType
                            on adminUser.UserTypeId equals userType.Id
                            where adminUser.UserEmail == loginDto.Email
                            select new AdminUserLoginDto()
                            {
                                Id = adminUser.Id,
                                UserName = adminUser.UserName,
                                Password = string.Empty,
                                UserEmail = adminUser.UserEmail,
                                UserTypeId = userType.Id,
                                TypeName = userType.TypeName
                            }).FirstOrDefaultAsync();

            return data;
        }
    }
}
