using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetworkData;
using NetworkData.Entities;
using NetworkData.Interfaces;
using NetworkData.Repository;
using NetworkData.UnitOfWork;
using NetworkDomain.BusineessData;
using NetworkDomain.IBusinessData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class ConfigureServiceExtensions
    {
        public static IServiceCollection AddRegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbServices(configuration);
            services.AddCustomSertvices();
            services.AddDomainSertvices();
            services.AddUnitOfWorkSertvices();
            services.AddRepositorySertvices();
            return services;
        }
        private static void AddDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
        private static void AddCustomSertvices(this IServiceCollection services)
        {
        }
        private static void AddRepositorySertvices(this IServiceCollection services)
        {
            //services.AddTransient<IRepository<Gateway>, Repository<Gateway>>();
        }
        private static void AddUnitOfWorkSertvices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWorkData<>), typeof(UnitOfWorkData<>));
        }
        private static void AddDomainSertvices(this IServiceCollection services)
        {
            services.AddTransient<IGatewayBusinessData, GatewayBusinessData>();
            services.AddTransient<IDeviceBusinessData, DeviceBusinessData>();
            services.AddTransient<IGatewayDevicesBusinessData, GatewayDevicesBusinessData>();
        }
    }
}
