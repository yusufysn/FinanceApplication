using ApplicationLayer.Services;
using ApplicationLayer.Services.Abstract;
using ApplicationLayer.Services.Concrete;
using ApplicationLayer.Validators;
using DomainLayer.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using InfrastructureLayer.Context;
using InfrastructureLayer.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    public static class DependencyIncetion
    {
        public static void RegisterApplicationDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<MoneyTransferValidator>();

            services.AddScoped<AccountService>();
            services.AddScoped<MoneyTransferService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}
