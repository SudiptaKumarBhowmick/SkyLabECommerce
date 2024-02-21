using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AdminUserRepository : GenericRepository<AdminUser>, IAdminUserRepository
    {
        public AdminUserRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            
        }

        public override async Task<IEnumerable<AdminUser>> GetAllAsync()
        {
            return await _applicationDBContext.AdminUser.Include(adminUser => adminUser.UserType).ToListAsync();
        }
    }
}
