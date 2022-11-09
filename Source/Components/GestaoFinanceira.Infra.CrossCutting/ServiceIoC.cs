using GestaoFinanceira.Domain.Core.Data;
using GestaoFinanceira.Domain.Core.Interfaces;
using GestaoFinanceira.Domain.Core.Logger.Interfaces;
using GestaoFinanceira.Infra.Data.Helpers;
using GestaoFinanceira.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoFinanceira.Infra.CrossCutting
{
    public static class ServiceIoC
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            #region Helper
            services.AddScoped<IRestFullHelper, RestFullHelper>();
            #endregion

            #region Repository
            services.AddScoped<IElmahRepository, ElmahRepository>();
            services.AddScoped<ILogRequestResponseRepository, LogRequestResponseRepository>();
            #endregion

            #region Handlers

            #endregion

            return services;
        }
    }
}
