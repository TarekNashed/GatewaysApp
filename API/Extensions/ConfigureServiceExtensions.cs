using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
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
            services.AddSwaggerGen();
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
        private static void AddSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateways", Version = "v1" });
                // To Enable authorization using Swagger (JWT)    
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                    }
                });
            });
        }
    }
}
