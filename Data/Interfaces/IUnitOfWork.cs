using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IAdminUserRepository AdminUserRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderStatusRepository OrderStatusRepository { get; }
        public IProductCategoryRepository ProductCategoryRepository { get; }
        public IProductImageRepository ProductImageRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IProductSubCategoryRepository ProductSubCategoryRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserTypeRepository UserTypeRepository { get; }
        public int Save();
    }
}
