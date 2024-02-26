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

            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
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
            services.AddScoped<ICloudinaryFileManager, CloudinaryFileManager>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConn")));

            return services;
        }

        public static CloudinarySettings? GetCloudinarySettings(IConfiguration configuration)
        {
            return configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
        }
    }
}
