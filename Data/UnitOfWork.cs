using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
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
        public readonly ApplicationDBContext _applicationDBContext;

        public UnitOfWork(ApplicationDBContext applicationDBContext,
            IAdminUserRepository AdminUser,
            IOrderRepository Order,
            IOrderStatusRepository OrderStatus,
            IProductCategoryRepository ProductCategory,
            IProductImageRepository ProductImage,
            IProductRepository Product,
            IProductSubCategoryRepository ProductSubCategory,
            IUserRepository User,
            IUserTypeRepository UserType,
            IAccountRepository Account)
        {
            _applicationDBContext = applicationDBContext;
            //AdminUserRepository = new AdminUserRepository(_applicationDBContext);
            //OrderRepository = new OrderRepository(_applicationDBContext);
            //OrderStatusRepository = new OrderStatusRepository(_applicationDBContext);
            //ProductCategoryRepository = new ProductCategoryRepository(_applicationDBContext);
            //ProductImageRepository = new ProductImageRepository(_applicationDBContext);
            //ProductRepository = new ProductRepository(_applicationDBContext);
            //ProductSubCategoryRepository = new ProductSubCategoryRepository(_applicationDBContext);
            //UserRepository = new UserRepository(_applicationDBContext);
            //UserTypeRepository = new UserTypeRepository(_applicationDBContext);

            AdminUserRepository = AdminUser;
            OrderRepository = Order;
            OrderStatusRepository = OrderStatus;
            ProductCategoryRepository = ProductCategory;
            ProductImageRepository = ProductImage;
            ProductRepository = Product;
            ProductSubCategoryRepository = ProductSubCategory;
            UserRepository = User;
            UserTypeRepository = UserType;
            AccountRepository = Account;

        }

        public IAdminUserRepository AdminUserRepository 
        {
            get;
        }

        public IOrderRepository OrderRepository
        {
            get;
        }

        public IOrderStatusRepository OrderStatusRepository
        {
            get;
        }

        public IProductCategoryRepository ProductCategoryRepository
        {
            get;
        }

        public IProductImageRepository ProductImageRepository
        {
            get;
        }

        public IProductRepository ProductRepository
        {
            get;
        }

        public IProductSubCategoryRepository ProductSubCategoryRepository
        {
            get;
        }

        public IUserRepository UserRepository
        {
            get;
        }

        public IUserTypeRepository UserTypeRepository
        {
            get;
        }

        public IAccountRepository AccountRepository
        {
            get;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _applicationDBContext.Dispose();
            }
        }

        public int Save()
        {
            return _applicationDBContext.SaveChanges();
        }
    }
}
