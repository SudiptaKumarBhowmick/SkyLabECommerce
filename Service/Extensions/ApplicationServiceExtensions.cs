using Data;
using Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Microsoft.AspNetCore.Mvc;
using Data.Repositories;
using Service.Interfaces;

namespace Service.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services)
        {
            ///Add automapper
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            ///Disable automatic validation of DTO and Entity models
            ///After disable, the errors can track at controller action method
            //services.Configure<ApiBehaviorOptions>(options=> options.SuppressModelStateInvalidFilter = true);

            return services;
        }

        public static IServiceCollection AddMyDependencyGroup(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAdminUserRepository, AdminUserRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTypeRepository, UserTypeRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductSubCategoryRepository, ProductSubCategoryRepository>();

            return services;
        }

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConn")));

            return services;
        }
    }
}
