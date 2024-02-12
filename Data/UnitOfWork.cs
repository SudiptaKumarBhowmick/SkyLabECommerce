using Data.Interfaces;
using Data.Repositories;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext _applicationDBContext;
        public UnitOfWork(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
            AdminUserRepository = new AdminUserRepository(_applicationDBContext);
            OrderRepository = new OrderRepository(_applicationDBContext);
            OrderStatusRepository = new OrderStatusRepository(_applicationDBContext);
            ProductCategoryRepository = new ProductCategoryRepository(_applicationDBContext);
            ProductImageRepository = new ProductImageRepository(_applicationDBContext);
            ProductRepository = new ProductRepository(_applicationDBContext);
            ProductSubCategoryRepository = new ProductSubCategoryRepository(_applicationDBContext);
            UserRepository = new UserRepository(_applicationDBContext);
            UserTypeRepository = new UserTypeRepository(_applicationDBContext);
        }

        public IAdminUserRepository AdminUserRepository 
        {
            get;
            private set;
        }

        public IOrderRepository OrderRepository
        {
            get;
            private set;
        }

        public IOrderStatusRepository OrderStatusRepository
        {
            get;
            private set;
        }

        public IProductCategoryRepository ProductCategoryRepository
        {
            get;
            private set;
        }

        public IProductImageRepository ProductImageRepository
        {
            get;
            private set;
        }

        public IProductRepository ProductRepository
        {
            get;
            private set;
        }

        public IProductSubCategoryRepository ProductSubCategoryRepository
        {
            get;
            private set;
        }

        public IUserRepository UserRepository
        {
            get;
            private set;
        }

        public IUserTypeRepository UserTypeRepository
        {
            get;
            private set;
        }

        public void Dispose()
        {
            _applicationDBContext.Dispose();
        }

        public int Save()
        {
            return _applicationDBContext.SaveChanges();
        }
    }
}
