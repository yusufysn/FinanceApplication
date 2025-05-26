using DomainLayer.Entities;
using DomainLayer.Repositories;
using InfrastructureLayer.Context;
using InfrastructureLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer
{
    public static class DependencyIncetion
    {
        public static void RegisterInfrastructureDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<FinanceAppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLServer"));
            });

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<FinanceAppDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
